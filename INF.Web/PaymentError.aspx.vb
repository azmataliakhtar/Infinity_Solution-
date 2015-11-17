
Imports System.Reflection
Imports INF.Web.Data.Entities
Imports INF.Web.Data.BLL
Imports INF.Web.UI
Imports INF.Web.UI.SagePay
Imports INF.Web.UI.Logging.Log4Net
Imports SagePay.IntegrationKit
Imports SagePay.IntegrationKit.Messages

Partial Class PaymentError
    Inherits EPAPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call UpdateTemporaryBasketWithPaymentStatus()
            ' Clear the shopping cart
            Call CleanShoppingCart()
        End If
    End Sub

    Private Sub UpdateTemporaryBasketWithPaymentStatus()
        _log.Debug(String.Format("{0}::{1}", MethodBase.GetCurrentMethod().Name, "BEGIN"))

        Try
            Dim vCustomer As CsCustomer = GetLoggedInCustomer()
            If vCustomer Is Nothing Then
                _log.Debug(String.Format("{0}::{1}", MethodBase.GetCurrentMethod().Name, "Could not find the customer!"))
                Exit Sub
            End If

            Dim paymentStatus As String = ""
            If (Not IsNothing(Request.QueryString("crypt")) AndAlso Not String.IsNullOrEmpty(Request.QueryString("crypt"))) Then
                'Dim paymentMessage As String = SagePayIntegration.DecodeAndDecrypt(Request.QueryString("crypt"))
                'paymentStatus = ExtractPaymentStatus(paymentMessage)

                Dim sagePayFormIntegration As SagePayFormIntegration = New SagePayFormIntegration()
                Dim paymentStatusResult As IFormPaymentResult = sagePayFormIntegration.ProcessResult(Request.QueryString("crypt"))

                If (paymentStatusResult.Status = ResponseStatus.NOTAUTHED) Then
                    paymentStatus = "NOTAUTHED"
                    ltrDetailErrors.Text = "You payment was declined by the bank.  This could be due to insufficient funds, or incorrect card details."
                ElseIf (paymentStatusResult.Status = ResponseStatus.ABORT) Then
                    paymentStatus = "ABORT"
                    ltrDetailErrors.Text = "You chose to Cancel your order on the payment pages."
                ElseIf (paymentStatusResult.Status = ResponseStatus.REJECTED) Then
                    paymentStatus = "REJECTED"
                    ltrDetailErrors.Text = "Your order did not meet our minimum fraud screening requirements."
                ElseIf (paymentStatusResult.Status = ResponseStatus.INVALID OrElse paymentStatusResult.Status = ResponseStatus.MALFORMED) Then
                    paymentStatus = "INVALID_MALFORMED"
                    ltrDetailErrors.Text = "We could not process your order because we have been unable to register your transaction with our Payment Gateway."
                ElseIf (paymentStatusResult.Status = ResponseStatus.ERROR) Then
                    paymentStatus = "ERROR"
                    ltrDetailErrors.Text = "We could not process your order because our Payment Gateway service was experiencing difficulties."
                Else
                    paymentStatus = "UNKNOWN"
                    ltrDetailErrors.Text = "The transaction process failed. Please contact us with the date and time of your order and we will investigate."
                End If

                If String.IsNullOrWhiteSpace(paymentStatus) Then
                    paymentStatus = "UNKNOWN"
                End If
            Else
                paymentStatus = "UNKNOWN"
            End If

            ' Retrieve the temporary saved basket from the database
            ' ID of temporary order information is stored in session with key as [CustomerEmail]_[SavedOrder.ID]
            ' If could not get the key from Session then it would be error.
            Dim tmpOrderInfoKey As String = Session(WebConstants.PLACING_ORDER_TEMP_BASKET_ID)
            If String.IsNullOrWhiteSpace(tmpOrderInfoKey) Then
                _log.Error("Could not get Session Key of temporary order stored when performing payment.")
                Response.RedirectTo(WebConstants.ERROR_PAGE)
                Exit Sub
            End If
            ' Extract ID of order information in BasketTemp from the Session Key
            Dim tokens() As String = tmpOrderInfoKey.Split(New Char() {"_"}, StringSplitOptions.RemoveEmptyEntries)
            Dim tmpBasketID As Integer = 0
            If tokens.Length > 1 AndAlso IsNumeric(tokens(tokens.Length - 1)) Then
                tmpBasketID = CInt(tokens(tokens.Length - 1))
            End If

            Dim basketBusiness As New BasketTempBusinessLogic()
            Dim basket As CsBasketTemp = basketBusiness.GetBasketTempByID(tmpBasketID)

            If IsNothing(basket) Then
                _log.Error("The temporary saved basket for customer [" & vCustomer.Email & "] has been lost!")
                Response.RedirectTo(WebConstants.ERROR_PAGE)
                Exit Sub
            End If

            ' Clean up temporary order information and session
            Session(WebConstants.PLACING_ORDER_TEMP_BASKET_ID) = Nothing

            ' Retrieve the temporary saved basket from the database
            basket.SagePayStatus = paymentStatus
            With basket
                .ChangedOn = DateTime.Now
                .ChangedBy = basket.CreatedBy
            End With
            basketBusiness.UpdateBasketTemp(basket, paymentStatus)
            'basketBusiness.UpdateSagePayStatus(vCustomer.ID & "_" & vCustomer.Email, paymentStatus)
        Catch ex As Exception
            _log.Error(ex)
        End Try
    End Sub
End Class
