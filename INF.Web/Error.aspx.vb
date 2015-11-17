
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net

Partial Class ErrorHandler
    Inherits EPAPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        _log.Info("BEGIN")

        If Not Page.IsPostBack Then

            Dim errorCode As String = ""
            If Not IsNothing(Page.Request.QueryString("msg")) Then
                errorCode = Page.Request.QueryString("msg")
            End If

            If errorCode = "EPOS_ERROR_MESSAGE" Then
                ErrorMessage = "If you are in progress of Checkout Order, then kindly contact the website owener."
            End If

            If Not String.IsNullOrEmpty(ErrorMessage) Then
                ltrErrorMessage.Text = ErrorMessage
                ErrorMessage = String.Empty
            End If
        End If
        _log.Info("END")
    End Sub
End Class
