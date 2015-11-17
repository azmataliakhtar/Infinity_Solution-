
Imports INF.Web.Data.BLL
Imports INF.Web.Data.DAL.SqlClient
Imports INF.Web.Data.Entities
Imports INF.Web.UI

Namespace Admin
    Partial Class Customers
        Inherits AdminPage

        Protected ReadOnly Property SelectedCustomerId() As Integer
            Get
                If String.IsNullOrEmpty(Request.Form("CustomerSelection")) Then
                    Return -1
                Else
                    Return CInt(Request.Form("CustomerSelection"))
                End If
            End Get
        End Property

        Protected Property CustomerName() As String
            Set(ByVal value As String)
                Session("Search_CustomerName") = value
            End Set
            Get
                If Not IsNothing(Session("Search_CustomerName")) Then
                    Return CStr(Session("Search_CustomerName"))
                End If
                Return String.Empty
            End Get
        End Property

        Protected Property CustomerEmail() As String
            Set(ByVal value As String)
                Session("Search_CustomerEmail") = value
            End Set
            Get
                If Not IsNothing(Session("Search_CustomerEmail")) Then
                    Return CStr(Session("Search_CustomerEmail"))
                End If
                Return String.Empty
            End Get
        End Property

        Protected Property CustomerTelephone() As String
            Set(ByVal value As String)
                Session("Search_CustomerTelephone") = value
            End Set
            Get
                If Not IsNothing(Session("Search_CustomerTelephone")) Then
                    Return CStr(Session("Search_CustomerTelephone"))
                End If
                Return String.Empty
            End Get
        End Property

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If RoleOfLoggedInUser() = UserRoles.Administrator Then
                UnBlockAllCustomerButton.Visible = True
            Else
                UnBlockAllCustomerButton.Visible = False
            End If

            If Not Page.IsPostBack Then
                ' Hide the left links for Settings Page
                Dim settingsContent As PlaceHolder = DirectCast(Page.Master.FindControl("SettingsPlaceHolder"), PlaceHolder)
                If settingsContent IsNot Nothing Then settingsContent.Visible = False

                LoadCustomers()

                TimePeriodOptions.Items.Add(New ListItem() With {.Value = "0", .Text = "- Anytime -"})
                TimePeriodOptions.Items.Add(New ListItem() With {.Value = "1", .Text = "Today"})
                TimePeriodOptions.Items.Add(New ListItem() With {.Value = "7", .Text = "In Week"})
                TimePeriodOptions.Items.Add(New ListItem() With {.Value = "30", .Text = "In Month"})
                TimePeriodOptions.Items.Add(New ListItem() With {.Value = "60", .Text = "2 Months"})
                TimePeriodOptions.Items.Add(New ListItem() With {.Value = "90", .Text = "3 Months"})
            End If
        End Sub

        Private Sub LoadCustomers()
            Dim business As New CustomerBusinessLogic()
            Dim customers As IEnumerable(Of CsCustomer) = business.GetAllCustomers()

            Dim name As String = CustomerNameTextBox.Text.Trim().ToLower()
            Dim email As String = EmailTextBox.Text.Trim().ToLower()
            Dim telephone As String = TelephoneTextBox.Text.Trim()

            Dim recentlyDays As Integer = 0
            If String.IsNullOrWhiteSpace(TimePeriodOptions.SelectedValue) Then
                recentlyDays = 0
            Else
                recentlyDays = CInt(TimePeriodOptions.SelectedValue)
            End If

            Dim filteredCustomers As IEnumerable(Of CsCustomer) = Nothing
            filteredCustomers = (From cs In customers
                Where
                    (String.IsNullOrWhiteSpace(name) OrElse (Not String.IsNullOrWhiteSpace(name) And (cs.FirstName.ToLower().StartsWith(name) OrElse cs.LastName.ToLower().StartsWith(name)))) And _
                    (String.IsNullOrWhiteSpace(email) OrElse (Not String.IsNullOrWhiteSpace(email) And cs.Email.ToLower() = email)) And _
                    (String.IsNullOrWhiteSpace(telephone) OrElse (Not String.IsNullOrWhiteSpace(telephone) And (cs.Telephone = telephone OrElse cs.Mobile = telephone)))
                Select cs).ToList()

            Dim result As New List(Of CsCustomer)
            If recentlyDays > 0 Then
                Dim shoppingBiz As New ShoppingBusinessLogic()
                Dim recentlyOrders As List(Of Order) = shoppingBiz.GetOrdersRecentlyDays(recentlyDays)
                result.AddRange(From csCustomer In filteredCustomers Where recentlyOrders.Any(Function(order) csCustomer.ID = order.CustomerID))
            Else
                result.AddRange(filteredCustomers)
            End If

            If Not IsNothing(result) Then
                ltrNumberOfCustomers.Text = result.Count().ToString()
                CustomerDataGrid.DataSource = result
                CustomerDataGrid.DataBind()
            Else
                ltrNumberOfCustomers.Text = "0"
            End If

        End Sub

        Protected Sub CustomerDataGrid_ItemCreated(sender As Object, e As DataGridItemEventArgs) Handles CustomerDataGrid.ItemCreated
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim markup As Literal = DirectCast(e.Item.FindControl("ltrRadioButtonMarkup"), Literal)
                Dim customer As CsCustomer = DirectCast(e.Item.DataItem, CsCustomer)
                If Not IsNothing(markup) AndAlso Not IsNothing(customer) Then
                    markup.Text = String.Format("<input type=""radio"" name=""CustomerSelection"" id=""RowSelector_{0}"" value=""{1}""></input>", e.Item.ItemIndex, customer.ID)
                End If
            End If
        End Sub

        Protected Sub CustomerDataGrid_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles CustomerDataGrid.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim customer As CsCustomer = DirectCast(e.Item.DataItem, CsCustomer)
                If Not IsNothing(customer) Then
                    If Not customer.IsActive Then
                        e.Item.Style.Add("background-color", "gainsboro")
                    Else
                        e.Item.Style.Add("background-color", "white")
                    End If
                End If
            End If
        End Sub

        Protected Sub CustomerDataGrid_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles CustomerDataGrid.PageIndexChanged
            If e.NewPageIndex < CustomerDataGrid.PageCount AndAlso e.NewPageIndex >= 0 Then
                CustomerDataGrid.CurrentPageIndex = e.NewPageIndex
            End If
            LoadCustomers()
        End Sub

        Protected Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
            LoadCustomers()
        End Sub

        Protected Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
            CustomerNameTextBox.Text = String.Empty
            EmailTextBox.Text = String.Empty
            TelephoneTextBox.Text = String.Empty
            TimePeriodOptions.SelectedIndex = 0
            LoadCustomers()
        End Sub

        Protected Sub UnBlockAllCustomerButton_Click(sender As Object, e As EventArgs) Handles UnBlockAllCustomerButton.Click
            Dim business As New CustomerBusinessLogic()
            business.UnBlockAllCustomer()
            LoadCustomers()
        End Sub
    End Class
End Namespace