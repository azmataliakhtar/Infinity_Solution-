
Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI
Imports INF.Web.Data.DAL.SqlClient

Namespace Admin
    Partial Class PendingOrders
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
                Session("SalesBoard_Search_CustomerName") = value
            End Set
            Get
                If Not IsNothing(Session("SalesBoard_Search_CustomerName")) Then
                    Return CStr(Session("SalesBoard_Search_CustomerName"))
                End If
                Return String.Empty
            End Get
        End Property

        Protected Property CustomerEmail() As String
            Set(ByVal value As String)
                Session("SalesBoard_Search_CustomerEmail") = value
            End Set
            Get
                If Not IsNothing(Session("SalesBoard_Search_CustomerEmail")) Then
                    Return CStr(Session("SalesBoard_Search_CustomerEmail"))
                End If
                Return String.Empty
            End Get
        End Property

        Protected Property CustomerTelephone() As String
            Set(ByVal value As String)
                Session("SalesBoard_Search_CustomerTelephone") = value
            End Set
            Get
                If Not IsNothing(Session("SalesBoard_Search_CustomerTelephone")) Then
                    Return CStr(Session("SalesBoard_Search_CustomerTelephone"))
                End If
                Return String.Empty
            End Get
        End Property

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                ' Hide the left links for Settings Page
                Dim settingsContent As PlaceHolder = DirectCast(Page.Master.FindControl("SettingsPlaceHolder"), PlaceHolder)
                If settingsContent IsNot Nothing Then settingsContent.Visible = False

                LoadPendingOrders()

                TimePeriodOptions.Items.Add(New ListItem() With {.Value = "0", .Text = "- Anytime -"})
                TimePeriodOptions.Items.Add(New ListItem() With {.Value = "1", .Text = "Today"})
                TimePeriodOptions.Items.Add(New ListItem() With {.Value = "7", .Text = "In Week"})
                TimePeriodOptions.Items.Add(New ListItem() With {.Value = "30", .Text = "In Month"})
                TimePeriodOptions.Items.Add(New ListItem() With {.Value = "60", .Text = "2 Months"})
                TimePeriodOptions.Items.Add(New ListItem() With {.Value = "90", .Text = "3 Months"})
            End If
        End Sub

        Private Sub LoadPendingOrders()
            Dim business As New BasketTempBusinessLogic()
            Dim pendingOrders As IEnumerable(Of CsBasketTemp) = business.FindCsBasketTemps(False)

            Dim result As List(Of CsBasketTemp) = pendingOrders.OrderByDescending(Function(x) x.OrderDate).ToList()

            Dim recentlyDays As Integer = 0
            If String.IsNullOrWhiteSpace(TimePeriodOptions.SelectedValue) Then
                recentlyDays = 0
            Else
                recentlyDays = CInt(TimePeriodOptions.SelectedValue)
            End If

            Dim recentlyOrders As New List(Of CsBasketTemp)
            If (recentlyDays > 0) Then
                For Each temp As CsBasketTemp In result
                    If temp.OrderDate.AddDays(recentlyDays).CompareTo(DateTime.Now) >= 0 Then
                        recentlyOrders.Add(temp)
                    End If
                Next
            Else
                recentlyOrders = result
            End If

            If Not IsNothing(recentlyOrders) Then
                ltrNumberOfPendingOrders.Text = recentlyOrders.Count.ToString()
                OrdersDataGrid.DataSource = recentlyOrders
                OrdersDataGrid.DataBind()
            Else
                ltrNumberOfPendingOrders.Text = "0"
            End If
        End Sub

        Protected Sub CustomerDataGrid_ItemCreated(sender As Object, e As DataGridItemEventArgs) Handles OrdersDataGrid.ItemCreated
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim markup As Literal = DirectCast(e.Item.FindControl("ltrRadioButtonMarkup"), Literal)
                Dim customer As CsBasketTemp = DirectCast(e.Item.DataItem, CsBasketTemp)
                If Not IsNothing(markup) AndAlso Not IsNothing(customer) Then
                    markup.Text = String.Format("<input type=""radio"" name=""CustomerSelection"" id=""RowSelector_{0}"" value=""{1}""></input>", e.Item.ItemIndex, customer.ID)
                End If
            End If
        End Sub

        Protected Sub CustomerDataGrid_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles OrdersDataGrid.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim customer As CsBasketTemp = DirectCast(e.Item.DataItem, CsBasketTemp)
                If Not IsNothing(customer) Then
                    'If Not customer.IsActive Then
                    '    e.Item.Style.Add("background-color", "gainsboro")
                    'Else
                    '    e.Item.Style.Add("background-color", "white")
                    'End If
                End If
            End If
        End Sub

        Protected Sub CustomerDataGrid_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles OrdersDataGrid.PageIndexChanged
            If e.NewPageIndex < OrdersDataGrid.PageCount AndAlso e.NewPageIndex >= 0 Then
                OrdersDataGrid.CurrentPageIndex = e.NewPageIndex
            End If
            LoadPendingOrders()
        End Sub

        Protected Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
            LoadPendingOrders()
        End Sub
    End Class
End Namespace