
Imports INF.Web.Data.BLL
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net

Namespace Admin
    Partial Class Login
        Inherits BasePage

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                ErrorsLabel.Visible = False
            End If
        End Sub

        Protected Sub LoginButton_Click(sender As Object, e As System.EventArgs) Handles LoginButton.Click
            If Page.IsValid Then
                If IsValidUser() Then
                    ErrorsLabel.Visible = False
                    Dim ticket As New FormsAuthenticationTicket(1, UserName.Text.Trim(), DateTime.Now, DateTime.Now.AddMinutes(30), True, ADMIN_ROLE)
                    Dim cookieStr As String = FormsAuthentication.Encrypt(ticket)
                    Dim cookie As New HttpCookie(FormsAuthentication.FormsCookieName, cookieStr)
                    cookie.Expires = ticket.Expiration
                    cookie.Path = FormsAuthentication.FormsCookiePath
                    Response.Cookies.Add(cookie)

                    Dim redirectStr As String = Request("ReturnUrl")
                    'If String.IsNullOrWhiteSpace(redirectStr) Then
                    redirectStr = "Dashboard.aspx"
                    'End If
                    Response.RedirectTo(redirectStr)
                Else
                    ErrorsLabel.Visible = True
                End If
            End If
        End Sub

        Private Function IsValidUser() As Boolean
            'If ("epos").Equals(UserName.Text.Trim()) Then
            '    Dim dynamicPassword As String = "ePos@" & DateTime.Now.ToString("dd")
            '    If dynamicPassword.Equals(Password.Text.Trim()) Then
            '        Return True
            '    End If
            'End If
            'Return False

            Dim userBiz As New UserBusinessLogic
            Try
                Return userBiz.ValidateUser(UserName.Text.Trim(), Password.Text.Trim())
            Catch ex As Exception
                _log.Error(ex)
                Return False
            End Try
        End Function
    End Class
End Namespace