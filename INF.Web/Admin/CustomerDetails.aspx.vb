Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI

Namespace Admin
    Partial Class CustomerDetails
        Inherits AdminPage

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

            Dim role As UserRoles = RoleOfLoggedInUser()
            If role = UserRoles.Manager OrElse role = UserRoles.Administrator Then
                BlockCustomerButton.Visible = True
                UnBlockCustomerButton.Visible = True
            Else
                BlockCustomerButton.Visible = False
                UnBlockCustomerButton.Visible = False
            End If

            If Not Page.IsPostBack Then
                LoadCustomerDetails(Me.CustomerId)
                LoadCustomerAddresses(Me.CustomerId)
            End If
        End Sub

        Private Sub LoadCustomerAddresses(ByVal vCustomerId As Integer)
            If vCustomerId <= 0 Then Return

            Dim business As New CustomerBusinessLogic()
            Dim addresses As IEnumerable(Of CsCustomerAddress) = business.GetCustomerAddresses(vCustomerId)
            If Not IsNothing(addresses) Then
                CustomerAddressDataGrid.DataSource = addresses
                CustomerAddressDataGrid.DataBind()
            End If
        End Sub

        Private Sub LoadCustomerDetails(ByVal vCustomerId As Integer)
            If vCustomerId <= 0 Then Return

            Dim business As New CustomerBusinessLogic()
            Dim customer As CsCustomer = business.GetCustomerByID(vCustomerId)
            If Not IsNothing(customer) Then
                EmailTextBox.Text = customer.Email
                TelephoneTextBox.Text = customer.Telephone
                MobileTextBox.Text = customer.Mobile
                LastNameTextBox.Text = customer.LastName
                FirstNameTextBox.Text = customer.FirstName
                MemberSinceTextBox.Text = customer.MemberSince.ToLongDateString()
                If customer.IsActive Then
                    IsActiveTextBox.Text = "YES"
                Else
                    IsActiveTextBox.Text = "NO"
                End If
                lblCustomerName.Text = customer.FirstName & " " & customer.LastName
            End If
        End Sub

        Protected Sub GoBack_Click(sender As Object, e As EventArgs) Handles GoBack.Click
            Response.RedirectTo("Customers.aspx")
        End Sub

        Protected Sub EditButton_Click(sender As Object, e As EventArgs) Handles EditButton.Click
            'EmailTextBox.Enabled = True
            TelephoneTextBox.Enabled = True
            MobileTextBox.Enabled = True
            LastNameTextBox.Enabled = True
            FirstNameTextBox.Enabled = True

            TelephoneTextBox.Focus()
        End Sub

        Protected Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
            If Page.IsValid Then
                If Me.CustomerId <= 0 Then Return

                Dim business As New CustomerBusinessLogic()
                Dim customer As CsCustomer = business.GetCustomerByID(Me.CustomerId)
                If Not IsNothing(customer) Then
                    customer.Email = EmailTextBox.Text.Trim()
                    customer.Telephone = TelephoneTextBox.Text.Trim()
                    customer.Mobile = MobileTextBox.Text.Trim()
                    customer.FirstName = FirstNameTextBox.Text.Trim()
                    customer.LastName = LastNameTextBox.Text.Trim()
                    business.SaveCustomer(customer)
                    MessageLabel.Text = "Customer Information has been saved successfully!"

                    LoadCustomerDetails(Me.CustomerId)
                    LoadCustomerAddresses(Me.CustomerId)
                End If
            End If
        End Sub

        Protected Sub BlockCustomerButton_Click(sender As Object, e As EventArgs) Handles BlockCustomerButton.Click
            If Me.CustomerId <= 0 Then Return

            Dim business As New CustomerBusinessLogic()
            Dim customer As CsCustomer = business.GetCustomerByID(Me.CustomerId)
            If Not IsNothing(customer) Then
                customer.IsActive = False
                business.SaveCustomer(customer)
                MessageLabel.Text = "This Customer has been blocked!"

                LoadCustomerDetails(Me.CustomerId)
                LoadCustomerAddresses(Me.CustomerId)
            End If
        End Sub

        Protected Sub UnBlockCustomerButton_Click(sender As Object, e As EventArgs) Handles UnBlockCustomerButton.Click
            If Me.CustomerId <= 0 Then Return

            Dim business As New CustomerBusinessLogic()
            Dim customer As CsCustomer = business.GetCustomerByID(Me.CustomerId)
            If Not IsNothing(customer) Then
                customer.IsActive = True
                business.SaveCustomer(customer)
                MessageLabel.Text = "This Customer has been granted again!"

                LoadCustomerDetails(Me.CustomerId)
                LoadCustomerAddresses(Me.CustomerId)
            End If
        End Sub
    End Class
End Namespace