Imports INF.Web.UI.Shopping
Imports INF.Web.UI.Utils
Imports INF.Web.UI
Imports INF.Web.UI.Settings
Imports INF.Web.UI.Logging.Log4Net
Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities



Namespace Ajax
    Partial Class SendReservation
        Inherits BasePage

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            _log.Info("BEGIN")
            Try

                Dim emailClient As String = WebUtil.GetParameterValueAsString(Page.Request, "emailClient")
                Dim phoneNr As String = WebUtil.GetParameterValueAsString(Page.Request, "phone")
                Dim firstName As String = WebUtil.GetParameterValueAsString(Page.Request, "firstName")
                Dim lastName As String = WebUtil.GetParameterValueAsString(Page.Request, "lastName")
                Dim nrPeople As String = WebUtil.GetParameterValueAsInteger(Page.Request, "nrPeople")
                Dim dateReservation As String = WebUtil.GetParameterValueAsString(Page.Request, "dateReservation")
                Dim dateTime As String = WebUtil.GetParameterValueAsString(Page.Request, "dateTime")
                Dim customerComment As String = WebUtil.GetParameterValueAsString(Page.Request, "customerComment")


               

                If (ConfigurationManager.AppSettings("bookingEmails") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("bookingEmails")))) Then

                    Dim emails = ConfigurationManager.AppSettings("bookingEmails")
                    Dim msg As New MailMsg() With {
                             .ToAddress = emails,
                             .ToDisplayName = EPATheme.Current.Themes.WebsiteName + " Booking ",
                             .Subject = EPATheme.Current.Themes.WebsiteName + " - " + firstName + " " + lastName + " - " + "Booking",
                             .Body = "" +
                                     "<b>TABLE RESERVATION</b> <br/> Customer Name:  " + firstName + " " + lastName + "<br/>" +
                                      "phone: " + phoneNr + ", email: " + emailClient + "<br/>" +
                                       "Booking for: " + nrPeople + "<br/>" +
                                       "Date: " + dateReservation + "<br/>" +
                                       "Arrival: " + dateTime + "<br/>" +
                                       customerComment + ""
                         }

                    SendMail(msg)

                End If

                If (ConfigurationManager.AppSettings("bookingEmails2") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("bookingEmails2")))) Then

                    Dim emails = ConfigurationManager.AppSettings("bookingEmails2")
                    Dim msg As New MailMsg() With {
                             .ToAddress = emails,
                             .ToDisplayName = EPATheme.Current.Themes.WebsiteName + " Booking ",
                             .Subject = EPATheme.Current.Themes.WebsiteName + " - " + firstName + " " + lastName + " - " + "Booking",
                             .Body = "" +
                                     "<b>TABLE RESERVATION</b> <br/> Customer Name:  " + firstName + " " + lastName + "<br/>" +
                                      "phone: " + phoneNr + ", email: " + emailClient + "<br/>" +
                                       "Booking for: " + nrPeople + "<br/>" +
                                       "Date: " + dateReservation + "<br/>" +
                                       "Arrival: " + dateTime + "<br/>" +
                                       customerComment + ""
                         }

                    SendMail(msg)

                End If

                If (ConfigurationManager.AppSettings("bookingEmails3") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("bookingEmails3")))) Then

                    Dim emails = ConfigurationManager.AppSettings("bookingEmails3")
                    Dim msg As New MailMsg() With {
                             .ToAddress = emails,
                             .ToDisplayName = EPATheme.Current.Themes.WebsiteName + " Booking ",
                             .Subject = EPATheme.Current.Themes.WebsiteName + " - " + firstName + " " + lastName + " - " + "Booking",
                             .Body = "" +
                                     "<b>TABLE RESERVATION</b> <br/> Customer Name:  " + firstName + " " + lastName + "<br/>" +
                                      "phone: " + phoneNr + ", email: " + emailClient + "<br/>" +
                                       "Booking for: " + nrPeople + "<br/>" +
                                       "Date: " + dateReservation + "<br/>" +
                                       "Arrival: " + dateTime + "<br/>" +
                                       customerComment + ""
                         }

                    SendMail(msg)

                End If

                'Dim returnValue As String = emailClient + ", " + firstName + ", " + lastName + ", " + nrPeople + ", " + dateReservation + ", " + dateTime + ", " + customerComment

                Response.Write("Success")

            Catch ex As Exception
                _log.Error(ex)
                Response.Write("Error")
            End Try

            _log.Info("END")
        End Sub

    End Class
End Namespace