
Imports System.Data.SqlClient
Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net

Partial Class Profile
    Inherits EPAPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Dim SqlCon As SqlConnection
    Protected strWelcomeMessage As String = ""
    Protected seconds As Double
    Dim sDate, EDate As Date
    Dim ShopOpenAt, ShopCloseAt As String
    Protected OpeningTime As String = "12:00 AM"

    Protected StatusMessage As String
    Protected ChangePasswordStatusMessage As String
    Protected AddressStatusMessage As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsAuthenticated Then
                Response.RedirectTo("Login.aspx")
                Exit Sub
            End If

            Dim vCustomer As CsCustomer = GetLoggedInCustomer()
            If Not IsNothing((vCustomer)) Then
                Session("CurrentUserId") = vCustomer.ID
            End If

            'GetOpeningTiming()
            'If WebUtil.IsWebsiteClosed() Then
            '    Me.seconds = 0
            'Else
            '    GetOpeningTiming()
            'End If

            Dim vRemainingTime As Double = RemainingTime
            Me.seconds = vRemainingTime

            If Not Page.IsPostBack Then
                LoadCustomerInformation()
                LoadCustomerAddresses()
            End If

        Catch ex As Exception
            _log.Error(ex)
        End Try
    End Sub

#Region "Private Methods"

    Private Sub LoadCustomerInformation()
        Dim businessLogic As New CustomerBusinessLogic()
        Try
            Dim customer As CsCustomer = businessLogic.GetCustomerByID(CInt(Session("CurrentUserId")))
            If Not IsNothing(customer) Then
                txtFirstName.Text = customer.FirstName
                txtLastName.Text = customer.LastName
                txtTelephone.Text = customer.Telephone
                txtEmailAddress.Text = customer.Email
                txtMobile.Text = customer.Mobile
            End If
        Catch ex As Exception
            _log.Error(ex)
        End Try
    End Sub

    Private Sub LoadCustomerAddresses()
        Dim businessLogic As New CustomerBusinessLogic()
        Try
            Dim addresses As List(Of CsCustomerAddress) = businessLogic.GetCustomerAddresses(CDec(Session("CurrentUserId"))).ToList()
            If Not IsNothing(addresses) Then
                DlAddresses.DataSource = addresses
                DlAddresses.DataBind()
            End If
        Catch ex As Exception
            _log.Error(ex)
        End Try
    End Sub

#End Region

    'Private Sub GetOpeningTiming()
    '    Try

    '        Dim ClosingTime As String = "12:30 PM"


    '        Dim sqlcmd As New SqlCommand
    '        Dim sqlread As SqlDataReader

    '        If SqlCon Is Nothing Then
    '            SqlCon = New SqlConnection(ConfigurationManager.ConnectionStrings("PizzaWebConnectionString").ConnectionString)
    '        End If

    '        sqlcmd.CommandType = CommandType.StoredProcedure
    '        sqlcmd.CommandText = "GetOpeningTiming"
    '        sqlcmd.Parameters.AddWithValue("@DayNumber", Now.DayOfWeek)

    '        If Not SqlCon Is Nothing Then
    '            If SqlCon.State <> ConnectionState.Open Then
    '                SqlCon.Open()
    '            End If
    '            If SqlCon.State = ConnectionState.Open Then
    '                sqlcmd.Connection = SqlCon
    '                sqlread = sqlcmd.ExecuteReader
    '                If sqlread.HasRows Then
    '                    sqlread.Read()
    '                    If Not IsDBNull(sqlread.GetValue(0)) Then
    '                        OpeningTime = sqlread.GetValue(0)
    '                        If OpeningTime.Length > 5 Then
    '                            OpeningTime = OpeningTime.Substring(0, 5)
    '                            ShopOpenAt = OpeningTime
    '                        End If
    '                    End If
    '                    If Not IsDBNull(sqlread.GetValue(1)) Then
    '                        ClosingTime = sqlread.GetValue(1)
    '                        If ClosingTime.Length > 5 Then
    '                            ClosingTime = ClosingTime.Substring(0, 5)
    '                            ShopCloseAt = ClosingTime
    '                        End If
    '                    End If
    '                End If
    '                sqlread.Close()
    '            End If
    '        End If

    '        'Calculate the Remaining time here

    '        Dim CurrentTime As DateTime = Convert.ToDateTime(DateTime.Now.ToShortTimeString)
    '        Dim TodayCloseTime As DateTime = Convert.ToDateTime(ClosingTime)

    '        TodayCloseTime = Convert.ToDateTime(ClosingTime)
    '        TodayCloseTime = Convert.ToDateTime(TodayCloseTime.ToShortTimeString)

    '        seconds = (TodayCloseTime - CurrentTime).TotalSeconds



    '    Catch ex As Exception
    '        m_Logger.Error("Message: " & ex.Message & vbNewLine & ex.StackTrace)
    '    End Try
    'End Sub

    Protected Sub UserProfile_RecordUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewUpdatedEventArgs)
        Try
            If Not ViewState("ReferrerUrl") Is Nothing Then
                Response.RedirectTo(ViewState("ReferrerUrl"))
            Else
                Response.RedirectTo("Profile.aspx")
            End If
        Catch ex As Exception
            _log.Error(ex)
        End Try
    End Sub

    Protected Sub UserProfile_AddressUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewUpdatedEventArgs)
        Try
            If Not ViewState("ReferrerUrl") Is Nothing Then
                Response.RedirectTo(ViewState("ReferrerUrl"))
            Else
                Response.RedirectTo("Profile.aspx")
            End If
        Catch ex As Exception
            _log.Error(ex)
        End Try
    End Sub

    Private Sub SetSelectedAddress()
        'Try
        '    Dim i As Integer = 0
        '    Dim lbl As HiddenField

        '    Dim rdcontrol As RadioButton
        '    For i = 0 To DLAddresses.Items.Count - 1
        '        rdcontrol = DLAddresses.Items(i).FindControl("rdoSelected")
        '        If rdcontrol.Checked Then
        '            Exit For
        '        End If
        '    Next
        '    If DLAddresses.Items.Count > 0 Then

        '        DLAddresses.Visible = True
        '        DVNewAddress.Visible = True

        '        lbl = DLAddresses.Items(i).FindControl("HField")
        '        If Not lbl Is Nothing Then
        '            Session("AddressId") = lbl.Value
        '        End If
        '    Else
        '        DLAddresses.Visible = False
        '        DVNewAddress.Visible = False
        '    End If

        'Catch ex As Exception
        '    m_Logger.Error("Message: " & ex.Message & vbNewLine & ex.StackTrace)
        'End Try
    End Sub

    'Protected Sub DLAddresses_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DLAddresses.ItemDataBound
    'Try
    '    If e.Item.ItemIndex = 1 Then
    '        Dim rdodefault As RadioButton
    '        rdodefault = DirectCast(DLAddresses.Items(0).Controls(1), RadioButton)
    '        rdodefault.Checked = True
    '        SetSelectedAddress()
    '    Else

    '    End If

    '    If e.Item.ItemType <> ListItemType.Item AndAlso e.Item.ItemType <> ListItemType.AlternatingItem Then
    '        Return
    '    End If

    '    Dim rdo As RadioButton = DirectCast(e.Item.FindControl("rdoSelected"), RadioButton)
    '    Dim script As String = "SetUniqueRadioButton('DLAddresses.*grpAddress',this)"
    '    rdo.Attributes.Add("onclick", script)

    'Catch ex As Exception
    '    m_Logger.Error("Message: " & ex.Message & vbNewLine & ex.StackTrace)
    'End Try
    'End Sub


    Protected Sub OnUpdateInformation(ByVal sender As Object, ByVal e As EventArgs)
        If Not Page.IsValid Then Return
        Me.StatusMessage = String.Empty
        Dim businessLogic As New CustomerBusinessLogic()
        Try
            Dim customer As CsCustomer = businessLogic.GetCustomerByID(CInt(Session("CurrentUserId")))
            If Not IsNothing(customer) Then
                customer.FirstName = txtFirstName.Text
                customer.LastName = txtLastName.Text
                customer.Telephone = txtTelephone.Text
                customer.Mobile = txtMobile.Text
                businessLogic.SaveCustomer(customer)
                Me.StatusMessage = "The changes for information has been saved successfully."
            End If
        Catch ex As Exception
            _log.Error(ex)
        End Try
    End Sub

    Protected Sub OnChangePassword(ByVal sender As Object, ByVal e As EventArgs)
        If Not Page.IsValid Then Return
        Me.ChangePasswordStatusMessage = String.Empty
        Dim businessLogic As New CustomerBusinessLogic()
        Try
            Dim customer As CsCustomer = businessLogic.GetCustomerByID(CInt(Session("CurrentUserId")))
            If Not IsNothing(customer) Then
                If customer.Password = txtOldPassword.Text.Trim() AndAlso txtNewPassword.Text.Trim() = txtConfirmPassword.Text.Trim() Then
                    customer.Password = txtNewPassword.Text.Trim()
                    businessLogic.SaveCustomer(customer)
                    Me.ChangePasswordStatusMessage = "New Password has been saved successfully."
                Else
                    Me.ChangePasswordStatusMessage = "The Old Password is not correct!"
                End If
            End If
        Catch ex As Exception
            _log.Error(ex)
        End Try
    End Sub

    Protected Sub DlAddresses_OnItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs)

        If String.IsNullOrWhiteSpace(e.CommandName) Then Return

        Dim hfAddressId As HiddenField = DirectCast(e.Item.FindControl("hfAddressID"), HiddenField)
        If IsNothing(hfAddressId) OrElse String.IsNullOrWhiteSpace(hfAddressId.Value) OrElse Not IsNumeric(hfAddressId.Value) Then Return

        Dim addressId As Decimal = CDec(hfAddressId.Value)
        If addressId = 0 Then Return

        Dim txtAddressLine1 As TextBox = DirectCast(e.Item.FindControl("txtAddressLine1"), TextBox)
        If IsNothing(txtAddressLine1) Then Return

        Dim txtAddressLine2 As TextBox = DirectCast(e.Item.FindControl("txtAddressLine2"), TextBox)
        If IsNothing(txtAddressLine2) Then Return

        Dim txtCity As TextBox = DirectCast(e.Item.FindControl("txtCity"), TextBox)
        If IsNothing(txtAddressLine1) Then Return

        Dim txtPostcode As TextBox = DirectCast(e.Item.FindControl("txtPostcode"), TextBox)
        If IsNothing(txtAddressLine1) Then Return

        Dim businessLogic As New CustomerBusinessLogic()
        If e.CommandName = "UpdateAddress" Then
            Dim address As CsCustomerAddress = businessLogic.GetCustomerAddressByID(addressId)
            If Not IsNothing(address) Then
                address.Address = txtAddressLine1.Text.Trim()
                address.AddressNotes = txtAddressLine2.Text.Trim()
                address.City = txtCity.Text.Trim()
                address.PostCode = txtPostcode.Text.Trim()
                businessLogic.SaveCustomerAddress(address)
                AddressStatusMessage = "The changes for address have been saved successfully."
            End If

        ElseIf e.CommandName = "DeleteAddress" Then
            Dim address As CsCustomerAddress = businessLogic.GetCustomerAddressByID(addressId)
            If Not IsNothing(address) Then
                businessLogic.DeleteCustomerAddress(address.ID)
                AddressStatusMessage = "The address have been deleted successfully."
            End If
        Else
            Return
        End If
        Dim addresses As List(Of CsCustomerAddress) = businessLogic.GetCustomerAddresses(CDec(Session("CurrentUserId"))).ToList()
        If Not IsNothing(addresses) Then
            DlAddresses.DataSource = addresses
            DlAddresses.DataBind()
        End If
    End Sub
End Class
