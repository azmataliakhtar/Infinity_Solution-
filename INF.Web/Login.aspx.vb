
Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI
Imports INF.Web.UI.Settings
Imports INF.Web.UI.Logging.Log4Net

Partial Class Login
    Inherits EPAPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _log.Info("BEGIN")

        Try
            If Not IsPostBack Then
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState(SSN_REFERRER_URL) = Request.UrlReferrer.ToString()
                Else
                    ViewState(SSN_REFERRER_URL) = "Menu.aspx"
                End If
            End If

        Catch ex As Exception
            _log.Error(ex)
        End Try

        _log.Info("END")
    End Sub

    Protected Sub CreateNewUserButton_Click(sender As Object, e As System.EventArgs) Handles CreateNewUserButton.Click
        _log.Info("BEGIN")

        If CreateNewUser() Then
            UserName.Text = txtEmailAddress.Text
            Password.Text = txtPassword.Text
            Call DoLogIn()
        End If

        _log.Info("END")
    End Sub

    Private Function CreateNewUser() As Boolean
        _log.Info("BEGIN")

        ' Force to validate input values
        If Page.IsValid Then
            Dim errors As New List(Of String)

            If Not CheckDuplicateEmail() Then
                errors.Add("The email is already registered.")
                BuildErrorMessagesControl(ErrorMessages, errors)

                _log.Info("END")
                Return False
            End If

            If Not CheckDuplicateMobile() Then
                errors.Add("The mobile is already registered.")
                BuildErrorMessagesControl(ErrorMessages, errors)

                _log.Info("END")
                Return False
            End If

            If Not CheckDuplicateTelephone() Then
                errors.Add("The telephone is already registered.")
                BuildErrorMessagesControl(ErrorMessages, errors)

                _log.Info("END")
                Return False
            End If

            Try
                Dim bll As New CustomerBusinessLogic()
                Dim vCustomer = New CsCustomer()
                With vCustomer
                    .Email = txtEmailAddress.Text.Trim()
                    .Password = txtPassword.Text.Trim()
                    .FirstName = txtFirstName.Text.Trim()
                    .LastName = txtLastName.Text.Trim()
                    .Telephone = txtPhoneNumber.Text.Trim()
                    .Mobile = txtMobilePhone.Text.Trim()

                    .MemberSince = DateTime.Now

                    Dim messages As New List(Of String)
                    Dim vSavedCustomer As CsCustomer = bll.SaveCustomer(vCustomer)

                    If vSavedCustomer IsNot Nothing AndAlso vSavedCustomer.ID > 0 Then

                        ' Save customer address
                        Dim cAddress As New CsCustomerAddress()
                        With cAddress
                            .CustomerID = vSavedCustomer.ID
                            .PostCode = txtPostcode.Text.Trim()
                            .Address = txtAddressLine1.Text.Trim()
                            .AddressNotes = txtAddressLine2.Text.Trim()
                            .City = txtCity.Text.Trim()
                            .Country = "UK"
                            .Distance = 0
                        End With

                        bll.SaveCustomerAddress(cAddress)

                        BuildMessagesControl(ErrorMessages, "An account for the customer have been created successfully")

                        _log.Info("END")
                        Return True
                    Else
                        messages.Add("Could not create new account")
                        BuildErrorMessagesControl(ErrorMessages, messages)
                    End If

                End With
            Catch ex As Exception
                _log.Error(ex)
            End Try
        End If

        _log.Info("END")
        Return False
    End Function

    Private Function CheckDuplicateTelephone() As Boolean
        _log.Info("BEGIN")

        Try
            If Not String.IsNullOrWhiteSpace(txtPhoneNumber.Text) Then
                Dim bll As New CustomerBusinessLogic()
                Return bll.CheckDuplicateTelephone(txtPhoneNumber.Text.Trim())
            End If

            _log.Info("END")
            Return True
        Catch ex As Exception
            _log.Error(ex)
            _log.Info("END")
            Return True
        End Try
    End Function

    Private Function CheckDuplicateMobile() As Boolean
        _log.Info("BEGIN")

        Try
            If Not String.IsNullOrWhiteSpace(txtMobilePhone.Text) Then
                Dim bll As New CustomerBusinessLogic()
                Return bll.CheckDuplicateMobile(txtMobilePhone.Text.Trim())
            End If

            _log.Info("END")
            Return True
        Catch ex As Exception
            _log.Error(ex)

            _log.Info("END")
            Return True
        End Try
    End Function

    Private Function CheckDuplicateEmail() As Boolean
        _log.Info("BEGIN")
        Try
            If Not String.IsNullOrWhiteSpace(txtEmailAddress.Text) Then
                Dim bll As New CustomerBusinessLogic()

                _log.Info("END")
                Return bll.CheckDuplicateEmail(txtEmailAddress.Text.Trim())
            End If

            _log.Info("END")
            Return True
        Catch ex As Exception
            _log.Error(ex)

            _log.Info("END")
            Return True
        End Try
    End Function

    Protected Sub LoginButton_Click(sender As Object, e As System.EventArgs) Handles LoginButton.Click
        _log.Info("BEGIN")
        Call DoLogIn()
        _log.Info("END")
    End Sub

    Private Sub DoLogIn()
        _log.Info("BEGIN")
        If Page.IsValid Then
            Dim vLoggedInCustomer As CsCustomer = IsValidUser()
            If vLoggedInCustomer IsNot Nothing Then
                HttpContext.Current.Session(SSN_LOGGED_IN_USER) = vLoggedInCustomer

                FailureText.Visible = False
                Dim userData As String = vLoggedInCustomer.LastName & " " & vLoggedInCustomer.FirstName
                Dim ticket As New FormsAuthenticationTicket(1, UserName.Text.Trim(), DateTime.Now, DateTime.Now().AddMinutes(ConfigurationManager.AppSettings("CookieLoginMins")), True, userData)
                Dim cookieStr As String = FormsAuthentication.Encrypt(ticket)
                Dim cookie As New HttpCookie(FormsAuthentication.FormsCookieName, cookieStr)
                cookie.Expires = ticket.Expiration
                cookie.Path = FormsAuthentication.FormsCookiePath
                Response.Cookies.Add(cookie)

                Dim redirectStr As String = CStr(ViewState(SSN_REFERRER_URL))
                If String.IsNullOrWhiteSpace(redirectStr) Then
                    redirectStr = "Default.aspx"
                End If

                If redirectStr.Contains("Login.aspx") OrElse redirectStr.Contains("login.aspx") Then
                    redirectStr = "Default.aspx"
                End If

                Response.RedirectTo(redirectStr)
                _log.Info("END")
                Exit Sub
            Else
                FailureText.Visible = True
            End If
        End If
        _log.Info("END")
    End Sub

    Private Function IsValidUser() As CsCustomer
        _log.Info("BEGIN")
        Dim vBusiness As New CustomerBusinessLogic()
        Dim vLoggedInCustomer As CsCustomer = vBusiness.ValidateCustomer(UserName.Text.Trim(), Password.Text.Trim())

        _log.Info("END")
        Return vLoggedInCustomer
    End Function

End Class
