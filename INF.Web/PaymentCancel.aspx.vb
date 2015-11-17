
Imports System.Reflection
Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI
Imports INF.Web.UI.SagePay
Imports INF.Web.UI.Logging.Log4Net

Partial Class PaymentCancel
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

        'Try
        '    Dim vCustomer As CsCustomer = GetLoggedInCustomer()
        '    If vCustomer Is Nothing Then
        '        _log.Debug(String.Format("{0}::{1}", MethodBase.GetCurrentMethod().Name, "Could not find the customer!"))
        '        Exit Sub
        '    End If

        '    Dim paymentStatus As String = ""
        '    If (Not IsNothing(Request.QueryString("crypt")) AndAlso Not String.IsNullOrEmpty(Request.QueryString("crypt"))) Then
        '        Dim paymentMessage As String = SagePayIntegration.DecodeAndDecrypt(Request.QueryString("crypt"))
        '        paymentStatus = ExtractPaymentStatus(paymentMessage)
        '        If String.IsNullOrWhiteSpace(paymentStatus) Then
        '            paymentStatus = "UnDefined"
        '        End If
        '    End If

        '    ' Retrieve the temporary saved basket from the database
        '    Dim basketBusiness As New BasketTempBusinessLogic()
        '    basketBusiness.UpdateSagePayStatus(vCustomer.ID & "_" & vCustomer.Email, paymentStatus)
        'Catch ex As Exception
        '    _log.Error(ex)
        'End Try
    End Sub
End Class
