
Imports System.Data.SqlClient
Imports System.Data
Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI
Imports INF.Web.UI.Settings
Imports INF.Web.UI.Utils
Imports INF.Web.UI.Logging.Log4Net


Partial Class PasswordRecovery
    Inherits EPAPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Structure EmailSettings
        Dim EmailSender As String
        Dim EmailServer As String
        Dim EmailAuthenticationUser As String
        Dim EmailAuthenticationPassword As String
        Dim ApplicationServerURL As String
        Dim FeedbackEmail As String
    End Structure

    Dim SqlCon As SqlConnection
    Dim EmilSet As EmailSettings


    Protected Sub RecoverPassword(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Try

            ' If page is not valid, return and this will display the error summary
            If Not Page.IsValid Then
                Return
            End If
            Dim pwd As String = ""

            Dim EmailAddress As String = Trim(txtEmail.Text)
            pwd = RecoverPassword(EmailAddress)

            If pwd = "" Then
                txtEmail.BackColor = Drawing.Color.Red
                txtEmail.Text = "Email does not exist"
                Exit Sub
            End If


            Dim MailBody As String = "Valued Customer, Here is your password as you requested.<br/><br/>"
            MailBody += "Your Email Address: " & EmailAddress & " <br/>"
            MailBody += "Your Password: " & pwd & " <br/>"

            Dim customerBusiness As New CustomerBusinessLogic
            Dim customer As CsCustomer = customerBusiness.GetCustomerByEmail(txtEmail.Text.Trim())
            If IsNothing(customer) Then
                'ltrMessage.Text = "This email is not registered in system yet!"
                'phMessages.Visible = True
                Return
            End If

            Dim newPassword As String = KeyGenerationUtil.GetUniqueKey(8)
            customer.Password = newPassword
            customerBusiness.SaveCustomer(customer)

            'Send mail to user
            Dim msg As New MailMsg() With {
                    .ToAddress = customer.Email,
                    .ToDisplayName = customer.LastName + " " + customer.FirstName,
                    .Subject = EPATheme.Current.Themes.WebsiteName + " - Reset your password",
                .Body = "Hi " + customer.LastName + " " + customer.FirstName + "<br/>" +
                "- Your username is: " + "<b>" + customer.Email + "</b>" + "<br/>" +
                "- Your new password is: " + "<b>" + newPassword + "</b>" + "<br/>" +
                "<br/>" + "Your Sincerely"
                }
            SendMail(msg)
            Response.RedirectTo("Login.aspx")
        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo(WebConstants.ERROR_PAGE)
        End Try
    End Sub

    Private Function RecoverPassword(ByVal Email As String) As String
        Try
            Dim txt As TextBox
            Dim pwd As String = ""


            Dim sqlcmd As New SqlCommand
            Dim sqlread As SqlDataReader

            SqlCon = New SqlConnection(ConfigurationManager.ConnectionStrings("PizzaWebConnectionString").ConnectionString)

            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.CommandText = "RecoverPassword"
            sqlcmd.Parameters.AddWithValue("@CustEmail", Email)

            If Not SqlCon Is Nothing Then
                SqlCon.Open()
                If SqlCon.State = ConnectionState.Open Then
                    sqlcmd.Connection = SqlCon
                    sqlread = sqlcmd.ExecuteReader

                    If sqlread.HasRows Then
                        sqlread.Read()
                        pwd = sqlread.GetValue(0).ToString
                    End If
                    sqlread.Close()
                End If
            End If


            Return pwd

        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo("~/Error.aspx")
        End Try
    End Function

    Private Function GetEmailSettings() As Boolean
        Try

            Dim sqlcmd As New SqlCommand
            Dim sqlread As SqlDataReader

            If SqlCon Is Nothing Then
                SqlCon = New SqlConnection(ConfigurationManager.ConnectionStrings("PizzaWebConnectionString").ConnectionString)
            End If

            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.CommandText = "GetEmailSettings"

            If Not SqlCon Is Nothing Then
                If SqlCon.State <> ConnectionState.Open Then
                    SqlCon.Open()
                End If

                If SqlCon.State = ConnectionState.Open Then
                    sqlcmd.Connection = SqlCon
                    sqlread = sqlcmd.ExecuteReader

                    If sqlread.HasRows Then
                        sqlread.Read()
                        If Not IsDBNull(sqlread.GetValue(1)) Then
                            EmilSet.EmailSender = sqlread.GetValue(1)
                        End If
                        If Not IsDBNull(sqlread.GetValue(2)) Then
                            EmilSet.EmailServer = sqlread.GetValue(2)
                        End If
                        If Not IsDBNull(sqlread.GetValue(3)) Then
                            EmilSet.EmailAuthenticationUser = sqlread.GetValue(3)
                        End If
                        If Not IsDBNull(sqlread.GetValue(4)) Then
                            EmilSet.EmailAuthenticationPassword = sqlread.GetValue(4)
                        End If
                        If Not IsDBNull(sqlread.GetValue(5)) Then
                            EmilSet.ApplicationServerURL = sqlread.GetValue(5)
                        End If
                        If Not IsDBNull(sqlread.GetValue(6)) Then
                            EmilSet.FeedbackEmail = sqlread.GetValue(6)
                        End If
                    Else
                        sqlread.Close()
                        EmilSet = Nothing
                        Return False
                    End If
                    sqlread.Close()
                End If
            End If
            Return True
        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo(WebConstants.ERROR_PAGE)
        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            mltFeedback.SetActiveView(viewFeedback)

        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo(WebConstants.ERROR_PAGE)
        End Try

    End Sub
End Class
