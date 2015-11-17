Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI
Imports INF.Web.UI.Settings
Imports INF.Web.UI.Utils
Imports INF.Web.UI.Logging.Log4Net

Namespace Account
    Public Class ForgotPassword
        Inherits BasePage

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        End Sub

        Protected Sub btnSendEmail_Click(sender As Object, e As EventArgs) Handles btnSendEmail.Click
            Try
                If Page.IsValid Then
                    Dim userBusiness As New UserBusinessLogic
                    Dim usr As CsUser = userBusiness.GetUserByEmail(txtEmail.Text.Trim())
                    If IsNothing(usr) Then
                        ltrMessage.Text = "This email is not registered in system yet!"
                        phMessages.Visible = True
                        Return
                    End If

                    Dim newPassword As String = KeyGenerationUtil.GetUniqueKey(8)
                    userBusiness.SetNewPassword(usr.UserName, newPassword)

                    'Send mail to user
                    Dim msg As New MailMsg() With {
                            .ToAddress = usr.Email,
                            .ToDisplayName = usr.LastName + " " + usr.FirstName,
                            .Subject = EPATheme.Current.Themes.WebsiteName + " - Reset your password",
                            .Body = "Hi " + usr.LastName + " " + usr.FirstName + "<br/>" +
                                    "- Your username is: " + "<b>" + usr.UserName + "</b>" + "<br/>" +
                                    "- Your new password is: " + "<b>" + newPassword + "</b>" + "<br/>" +
                                    "<br/>" + "Your Sincerely"
                            }
                    SendMail(msg)
                    Response.RedirectTo("/Admin/LogIn.aspx")
                End If

                'TODO: Testing
                'Throw New Exception("This is exception for testing purpose!")
            Catch ex As Exception
                _log.Error(ex)
                Response.RedirectTo("~/Error.apsx")
            End Try
        End Sub

    End Class
End Namespace