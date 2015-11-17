
Imports INF.Web.UI
Imports INF.Web.UI.Settings
Imports INF.Web.UI.SagePay
Imports System.Security.Policy
Imports System.Reflection
Imports INF.Web.UI.Logging.Log4Net
Imports SagePay.IntegrationKit
Imports INF.Web.UI.Utils

Partial Class OrderConfirmation
    Inherits EPAPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                'UpdateTimer.Enabled = True
                ' Save the order to database then send email to restaurant and the customer
                If Not Request.UrlReferrer Is Nothing Then
                    Dim urlToCheck As String = String.Empty
                    If Not IsNothing(Request.UrlReferrer) Then
                        urlToCheck = Request.UrlReferrer.ToString().ToLower()
                    End If

                    If Not String.IsNullOrWhiteSpace(urlToCheck) AndAlso urlToCheck.Contains("www.") Then
                        urlToCheck = urlToCheck.Replace("www.", "")
                    End If

                    Dim orderReviewUrl As String = WebsiteConfig.Instance.OrderReviewReferrerUrl.ToLower()
                    If Not String.IsNullOrEmpty(orderReviewUrl) AndAlso orderReviewUrl.Contains("www.") Then
                        orderReviewUrl = orderReviewUrl.Replace("www.", "")
                    End If

                    _log.Debug(MethodBase.GetCurrentMethod().Name & ":" & "UrlReferrer=" & urlToCheck)
                    _log.Debug(MethodBase.GetCurrentMethod().Name & ":" & "OrderReviewReferrerUrl=" & orderReviewUrl)

                    Dim referrerPage As String = urlToCheck.AspxPage()
                    Dim orderReviewPage As String = orderReviewUrl.AspxPage()

                    _log.Debug(MethodBase.GetCurrentMethod().Name & ":" & "ReferrerPage=" & referrerPage)
                    _log.Debug(MethodBase.GetCurrentMethod().Name & ":" & "OrderReviewPage=" & orderReviewPage)

                    ViewState(SSN_REFERRER_URL) = urlToCheck
                    'Dim referrerUrl As String = WebsiteConfig.Instance.OrderReviewReferrerUrl
                    'If (LCase(ViewState(SSN_REFERRER_URL)) = LCase(referrerUrl)) Then
                    If referrerPage = orderReviewPage Then

                        ' Makes sure the customer has logged in
                        If Not IsAuthenticated Then
                            Response.RedirectTo("Login.aspx")
                            Exit Sub
                        End If

                        VPSProtocol.Value = SagePaySettings.ProtocolVersion.VersionString()
                        TxType.Value = SagePaySettings.DefaultTransactionType.ToString()
                        Vendor.Value = SagePaySettings.VendorName
                        frmSagePay.Action = SagePaySettings.FormPaymentUrl

                        If Not IsNothing(Session("POST_STR")) Then
                            Crypt.Value = Session("POST_STR").ToString()
                        End If

                        'Order Saved successfully, make page referrel null, so that refreshing thankyou page can't save order again
                        ViewState(SSN_REFERRER_URL) = Nothing
                        Session(SSN_ORDER_CONFIRMATION_PAGE) = Request.Url.AbsoluteUri.ToString()
                    Else
                        _log.Debug(MethodBase.GetCurrentMethod().Name & ":" & "The confirmation is failure!")
                        Response.RedirectTo("Menu.aspx")
                        Exit Sub
                    End If
                Else
                    _log.Debug(MethodBase.GetCurrentMethod().Name & ":" & "The confirmation is failure!")
                    _log.Debug(MethodBase.GetCurrentMethod().Name & ":" & "UrlReferrer=" & "UNKNOWN UrlReferrer!")
                    Response.RedirectTo("Menu.aspx")
                    Exit Sub
                End If

            End If
        Catch ex As Exception
            _log.Debug(MethodBase.GetCurrentMethod().Name & ":" & "The confirmation is failure!")
            _log.Error(ex)
        End Try
    End Sub
End Class
