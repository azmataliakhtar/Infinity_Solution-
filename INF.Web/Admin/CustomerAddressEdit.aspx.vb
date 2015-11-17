Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI

Namespace Admin
    Partial Class CustomerAddressEdit
        Inherits AdminPage
        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                hdnCustomerId.Value = Me.CustomerId.ToString()
                hdnAddressId.Value = Me.AddressId.ToString()

                LoadCustomerInfo(Me.CustomerId)
                LoadCustomerAddress(Me.AddressId)
            End If
        End Sub

        Private Sub LoadCustomerAddress(ByVal vAddressId As Integer)
            If vAddressId <= 0 Then Return
            Dim business As New CustomerBusinessLogic
            Dim address As CsCustomerAddress = business.GetCustomerAddressByID(vAddressId)
            If Not IsNothing(address) Then
                PostcodeTextBox.Text = address.PostCode
                CityTextBox.Text = address.City
                AddressTextBox.Text = address.Address
                AddressNoteTextBox.Text = address.AddressNotes
            End If
        End Sub

        Private Sub LoadCustomerInfo(ByVal vCustomerId As Integer)
            If vCustomerId <= 0 Then Return

            Dim business As New CustomerBusinessLogic
            Dim customer = business.GetCustomerByID(vCustomerId)
            If Not IsNothing(customer) Then
                ltrCustomer.Text = customer.FirstName & " " & customer.LastName
            End If
        End Sub

        Protected Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
            Response.RedirectTo("CustomerDetails.aspx?CustomerId=" & hdnCustomerId.Value)
        End Sub

        Protected Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
            If Page.IsValid Then
                If Me.AddressId <= 0 Then Return
                Dim business As New CustomerBusinessLogic
                Dim address As CsCustomerAddress = business.GetCustomerAddressByID(Me.AddressId)
                If Not IsNothing(address) Then
                    address.PostCode = PostcodeTextBox.Text.Trim().ToUpper()
                    address.City = CityTextBox.Text.Trim()
                    address.Address = AddressTextBox.Text.Trim()
                    address.AddressNotes = AddressNoteTextBox.Text.Trim()

                    business.SaveCustomerAddress(address)

                    Response.RedirectTo("CustomerDetails.aspx?CustomerId=" & hdnCustomerId.Value)
                End If
            End If
        End Sub
    End Class
End Namespace