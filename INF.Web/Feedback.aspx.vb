
Imports System.Net.Mail
Imports System.Data.SqlClient
Imports System.Data
Imports System.Security.Principal
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net


Partial Class Feedback
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

    
    Protected Sub SubmitFeedback(ByVal sender As Object, ByVal e As EventArgs)
        Try

            ' If page is not valid, return and this will display the error summary
            If Not Page.IsValid Then
                Return
            End If

            Dim FoodQualityRatingValue As String = ratingFoodQuality.CurrentRating.ToString()
            Dim ProcessingTimeRatingValue As String = ratingProcessingTime.CurrentRating.ToString()
            Dim DeliveryTimeRatingValue As String = ratingDeliveryTime.CurrentRating.ToString()

            'string Email = this.txtEmail.Text.Trim();
            Dim comments As String = Me.txtComments.InnerText.Trim()

            Dim MailBody As [String] = "A new feedback has been received from " & Session("FirstName").ToString() & " " & Session("LastName").ToString() & ".<br/><br/>"
            MailBody += "Food Quality Rating: " & FoodQualityRatingValue & " <br/>"
            MailBody += "Processing Time Rating: " & ProcessingTimeRatingValue & " <br/>"
            MailBody += "Delivery Time Rating: " & DeliveryTimeRatingValue & " <br/><br/>"

            MailBody += "Email Address : " & Session("Email").ToString() & " <br/>"
            MailBody += "User Comments: " & comments & " <br/><br/>"

            If GetEmailSettings() Then

                Dim mMailMessage As New MailMessage()

                ' Set the sender address of the mail message
                mMailMessage.From = New MailAddress(EmilSet.EmailSender)
                ' Set the recepient address of the mail message
                mMailMessage.To.Add(New MailAddress(EmilSet.FeedbackEmail))
                mMailMessage.Bcc.Add(New MailAddress("imrancs_qau@yahoo.com"))

                mMailMessage.Subject = "User Feedback - Wrapidos Leicester"
                ' Set the body of the mail message
                mMailMessage.Body = MailBody
                ' Set the format of the mail message body as HTML
                mMailMessage.IsBodyHtml = True
                ' Set the priority of the mail message to normal
                mMailMessage.Priority = MailPriority.Normal

                ' Instantiate a new instance of SmtpClient
                Dim mSmtpClient As New SmtpClient()
                mSmtpClient.UseDefaultCredentials = False
                mSmtpClient.Credentials = New System.Net.NetworkCredential(EmilSet.EmailAuthenticationUser, EmilSet.EmailAuthenticationPassword)

                mSmtpClient.Host = EmilSet.EmailServer
                Try
                    ' Send the mail message
                    mSmtpClient.Send(mMailMessage)
                    'Response.Write(ex.ToString());
                    'Response.End();
                Catch ex As Exception
                    'm_Logger.Error("Message: " & ex.Message & vbNewLine & ex.StackTrace)
                End Try
                Me.mltFeedback.SetActiveView(viewFeedback)
            End If
        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo(WebConstants.ERROR_PAGE)
        End Try
    End Sub

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
            Response.Write(WebConstants.ERROR_PAGE)
        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'If Session("CurrentUserId") Is Nothing Then
            If Not IsAuthenticated Then
                Response.RedirectTo("Login.aspx")
            Else
                Dim vIdentity As IIdentity = HttpContext.Current.User.Identity
                mltFeedback.SetActiveView(viewFeedback)
            End If
        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo(WebConstants.ERROR_PAGE)
        End Try
    End Sub
End Class
