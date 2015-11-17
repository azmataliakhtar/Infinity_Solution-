Imports INF.Web.Data.Entities
Imports INF.Web.Data.BLL
Imports System.IO
Imports System.Reflection
Imports INF.Web.UI.Settings
Imports INF.Web.UI.SagePay
Imports INF.Web.UI
Imports INF.Web.Data.DAL.SqlClient
Imports INF.Web.UI.Shopping
Imports INF.Web.UI.Logging.Log4Net
Imports SagePay.IntegrationKit
Imports SagePay.IntegrationKit.Messages
Imports INF.Web.UI.Utils
Imports AjaxControlToolkit.HTMLEditor.ToolbarButton
Imports ShoppingCart = INF.Web.Ajax.ShoppingCart

Partial Class OrderWaitProcess
    Inherits EPAPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Protected Property CurrentOrderID As Decimal
        Get
            Dim vOrder As Decimal = 0
            If Session("CurrentOrderID") IsNot Nothing AndAlso IsNumeric(Session("CurrentOrderID")) Then
                vOrder = CDec(Session("CurrentOrderID"))
            End If
            Return vOrder
        End Get
        Set(ByVal value As Decimal)
            Session("CurrentOrderID") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        _log.Info("BEGIN")

        Try
            UpdateProgressPlaceHolder.Visible = False
            If Not Page.IsPostBack Then
                ' Makes sure the customer has logged in
                If Not IsAuthenticated Then
                    Response.RedirectTo("Login.aspx")
                    _log.Info("END")
                    Exit Sub
                End If

                If Session(SSN_PAYMENT_MODE) = CNS_PAYMENT_CARD Then
                    If (Not IsNothing(Request.QueryString("crypt")) AndAlso Not String.IsNullOrEmpty(Request.QueryString("crypt"))) Then
                        Dim sagePayFormIntegration As SagePayFormIntegration = New SagePayFormIntegration()
                        Dim paymentStatusResult As IFormPaymentResult = sagePayFormIntegration.ProcessResult(Request.QueryString("crypt"))

                        If Not paymentStatusResult.Status = ResponseStatus.OK Then
                            Response.RedirectTo("PaymentError.aspx?crypt=" & Request.QueryString("crypt"))
                            _log.Info("END")
                            Exit Sub
                        Else
                            If paymentStatusResult.Status = ResponseStatus.OK AndAlso Not String.IsNullOrEmpty(paymentStatusResult.BankAuthCode) Then
                                If GetAndSaveOrderDetail(paymentStatusResult) Then

                                    CleanShoppingCart()
                                    UpdateProgressPlaceHolder.Visible = True
                                End If

                                'Order Saved successfully, make page referrel null, so that refreshing thankyou page can't save order again
                                ViewState(SSN_REFERRER_URL) = Nothing
                            Else
                                Response.RedirectTo("PaymentError.aspx?crypt=" & Request.QueryString("crypt"))
                                _log.Info("END")
                                Exit Sub
                            End If
                        End If
                    Else
                        Response.RedirectTo("PaymentError.aspx?crypt=" & "")
                        _log.Info("END")
                        Exit Sub
                    End If
                Else
                    If GetAndSaveOrderDetail() Then

                        CleanShoppingCart()
                        UpdateProgressPlaceHolder.Visible = True
                    End If

                    'Order Saved successfully, make page referrel null, so that refreshing thankyou page can't save order again
                    ViewState(SSN_REFERRER_URL) = Nothing
                End If
            End If
        Catch ex As Exception
            _log.Error(ex)
        End Try

        _log.Info("END")
        Response.Redirect("WaitingOrderConfirmation.aspx")
    End Sub

    Protected Sub UpdateTimer_Tick(sender As Object, e As System.EventArgs) Handles UpdateTimer.Tick
        _log.Info("BEGIN")

        UpdateProgressPlaceHolder.Visible = False
        UpdateTimer.Enabled = False



        Dim vBusiness As New ShoppingBusinessLogic()
        Try
            Dim vOrder As Order = vBusiness.GetOrderByID(CurrentOrderID)
            If (vOrder Is Nothing) Then
                Return
            End If

            If ("CONFIRMED").Equals(vOrder.OrderStatus, StringComparison.CurrentCultureIgnoreCase) Then
                MessagePlaceHolder.Visible = True
                ErrorMessagePlaceHolder.Visible = False

                SendOrderEmail3Customer(vOrder)

                If vOrder.ProcessingTime > 0 Then
                    ' ltrProcessingTimeMessage.Text = vOrder.ProcessingTime.ToString()
                Else
                    'ltrProcessingTimeMessage.Text = "45"
                End If

                If UCase(vOrder.OrderType) = "DELIVERY" Then

                    If vOrder.ProcessingTime > 0 Then
                        ltrDeliveryTime.Text = vOrder.ProcessingTime.ToString() + " min"
                        ltrDeliveryTime.Visible = True
                    Else
                        ltrDeliveryTime.Text = vOrder.ExpectedTime
                        ltrDeliveryTime.Visible = True
                    End If

                Else
                    ltrDeliveryTime.Text = ""
                    ltrDeliveryTime.Visible = False
                End If

                'Dim vTempCustomer As CsCustomer = Nothing
                'vTempCustomer = GetLoggedInCustomer()
                'Dim vOrderTemp = vBusiness.GetLastOrderByCustomer(vTempCustomer.ID)
                'SendOrderEmail2Customer(vTempCustomer, vOrderTemp)
            Else
                MessagePlaceHolder.Visible = False
                ErrorMessagePlaceHolder.Visible = True
            End If
        Catch ex As Exception
            _log.Error(ex)
        End Try

        _log.Info("END")
    End Sub

    Private _BasketID As Integer = 0

    Private Function GetAndSaveOrderDetail(Optional paymentStatusResult As IFormPaymentResult = Nothing) As Boolean
        If Page.IsPostBack Then

        End If
        _log.Info("BEGIN")

        Dim vCustomer As CsCustomer = Nothing
        Dim basket As CsBasketTemp = Nothing
        Try
            ' Check if customer logged in
            vCustomer = GetLoggedInCustomer()
            If vCustomer Is Nothing Then
                _log.Debug("Could not find the customer!")
                Response.RedirectTo(WebConstants.ERROR_PAGE)
                Return False
            End If
            _log.Debug("Customer: " & vCustomer.LastName & " " & vCustomer.FirstName & " is placing order.")

            ' Retrieve the temporary saved basket from the database
            ' ID of temporary order information is stored in session with key as [CustomerEmail]_[SavedOrder.ID]
            ' If could not get the key from Session then it would be error.
            Dim tmpOrderInfoKey As String = Session(WebConstants.PLACING_ORDER_TEMP_BASKET_ID)
            If String.IsNullOrWhiteSpace(tmpOrderInfoKey) Then

                ' Read basket temp key from cookies
                Dim ck As HttpCookie = Request.Cookies("AspNet_infinitysol")
                If ck IsNot Nothing Then
                    Dim objKey = ck.Values(WebConstants.PLACING_ORDER_TEMP_BASKET_ID)
                    If objKey IsNot Nothing AndAlso Not String.IsNullOrEmpty(objKey.ToString()) Then
                        tmpOrderInfoKey = objKey.ToString()
                    End If
                End If
                If String.IsNullOrEmpty(tmpOrderInfoKey) Then
                    _log.Error("Could not get Session Key of temporary order stored when performing payment.")
                    Response.RedirectTo(WebConstants.ERROR_PAGE)
                    Return False
                End If
            End If

            ' Extract ID of order information in BasketTemp from the Session Key
            Dim tokens() As String = tmpOrderInfoKey.Split(New Char() {"_"}, StringSplitOptions.RemoveEmptyEntries)
            Dim tmpBasketId As Integer = 0
            If tokens.Length > 1 AndAlso IsNumeric(tokens(tokens.Length - 1)) Then
                tmpBasketId = CInt(tokens(tokens.Length - 1))
            End If

            Dim basketBusiness As New BasketTempBusinessLogic()
            basket = basketBusiness.GetBasketTempByID(tmpBasketId)

            If IsNothing(basket) Then
                _log.Error("The temporary saved basket for customer [" & vCustomer.Email & "] has been lost!")
                Response.RedirectTo(WebConstants.ERROR_PAGE)
                Return False
            End If

            ' Clean up temporary order information and session
            'CleanUpTemporaryStorage(tmpBasketId)

            UpdateTemporaryStorage(tmpBasketId, paymentStatusResult)
            Session(WebConstants.PLACING_ORDER_TEMP_BASKET_ID) = Nothing
            Request.Cookies.Remove("AspNet_infinitysol")

        Catch ex As Exception
            _log.Error(ex)
        End Try

        Me._BasketID = basket.ID
        Dim vIsSuccessful As Boolean = False
        If Not IsNothing(basket) AndAlso (basket.Items.Count > 0) Then
            Dim basketItems = basket.Items
            Dim vOrder As New Order()

            Try

                Dim vOrderItems As String = ""
                Dim vOrderItemsQty As String = ""
                Dim vOrderItemsPrice As String = ""

                With vOrder
                    .CustomerID = basket.CustomerID
                    .OrderType = basket.OrderType
                    .OrderStatus = basket.OrderStatus
                    .TotalAmount = basket.TotalAmount
                    .AmountReceived = basket.AmountReceived
                    .AmountDue = basket.AmountDue

                    If basket.PaymentType = CNS_PAYMENT_CASH Then
                        .PayStatus = "NOT PAID"
                    Else
                        .PayStatus = "PAID"
                    End If
                    '.PayStatus = basket.PayStatus

                    .VoucherCode = basket.VoucherCode
                    .DiscountType = basket.DiscountType
                    .Discount = basket.Discount
                    .DeliveryCharges = basket.DeliveryCharge
                    .PaymentCharges = basket.PaymentCharge
                    .PaymentType = basket.PaymentType
                    .SpecialInstructions = basket.SpecialInstructions
                    .AddressId = basket.AddressID
                    .OrderDate = basket.OrderDate

                    .ExpectedTime = basket.ExpectedTime
                End With

                _log.Debug("The order as following:")
                _log.Debug("- OrderType: " & vOrder.OrderType)
                _log.Debug("- TotalAmount: " & vOrder.TotalAmount)
                _log.Debug("- AmountReceived: " & vOrder.AmountReceived)
                _log.Debug("- AmountDue: " & vOrder.AmountDue)
                _log.Debug("- PayStatus: " & vOrder.PayStatus)
                _log.Debug("- Discount: " & vOrder.Discount)
                _log.Debug("- DeliveryCharges: " & vOrder.DeliveryCharges)
                _log.Debug("- PaymentCharges: " & vOrder.PaymentCharges)
                _log.Debug("- SpecialInstructions: " & vOrder.SpecialInstructions)
                _log.Debug("- OrderDate: " & vOrder.OrderDate.ToString("yyyy-MM-dd HH:mm:ss"))
                _log.Debug("- Items: " & basketItems.Count)

                For vIndex As Integer = 0 To basketItems.Count - 1
                    Dim vOrderDetail As New CsOrderDetail()

                    With vOrderDetail
                        .OrderID = vOrder.ID

                        .Price = CDec(basketItems(vIndex).Quantity * basketItems(vIndex).UnitPrice)
                        .Quantity = basketItems(vIndex).Quantity
                        .SpecialRequest = basketItems(vIndex).SpecialRequest
                        .MenuItemName = basketItems(vIndex).ItemName
                        .Status = basketItems(vIndex).Status
                        .Dressing = basketItems(vIndex).Dressing
                    End With

                    vOrderItems = vOrderItems & basketItems(vIndex).ItemName & ","
                    vOrderItemsQty = vOrderItemsQty & CStr(basketItems(vIndex).Quantity) & ","
                    vOrderItemsPrice = vOrderItemsPrice & CStr(basketItems(vIndex).Quantity * basketItems(vIndex).UnitPrice) & ","

                    vOrder.OrderDetails.Add(vOrderDetail)
                Next

            Catch ex As Exception
                _log.Error(ex)
            End Try

            Dim vBusiness As New ShoppingBusinessLogic()
            Try

                ' Add extra information
                If paymentStatusResult IsNot Nothing Then
                    With vOrder
                        .TxAuthNo = paymentStatusResult.TxAuthNo
                        .VpsTxId = paymentStatusResult.VpsTxId
                        .BankAuthCode = paymentStatusResult.BankAuthCode
                        .Last4Digits = paymentStatusResult.Last4Digits
                    End With
                End If

                Dim vSavedOrder As Order = vBusiness.SaveCustomerOrder(vOrder)
                If vSavedOrder Is Nothing Then
                    vIsSuccessful = False
                Else
                    CurrentOrderID = vSavedOrder.ID
                    vIsSuccessful = True
                End If
            Catch vException As Exception
                vIsSuccessful = False
                _log.Error(vException)
            End Try

            Session(SSN_ADDRESS_ID) = Nothing

            Try
                ' Send emails
                If vIsSuccessful Then
                    SendOrderEmail2Restaurant(vCustomer, basketItems, vOrder)

                Else
                    Response.RedirectTo("Error.aspx")
                End If
            Catch ex As Exception
                _log.Error(ex)
            End Try

        End If

        _log.Info("END")
        Return vIsSuccessful
    End Function

    Private Sub SendOrderEmail2Restaurant(vCustomer As CsCustomer, vCartItems As List(Of CsBasketItemTemp), vOrder As Order)
        _log.Info("BEGIN")
        Try
            Dim templateContent As String = ReadContentFile("~/Templates/OrderNotification.htm")
            If String.IsNullOrWhiteSpace(templateContent) Then Return
            If IsNothing(vCustomer) Then Return
            If IsNothing(vCartItems) OrElse vCartItems.Count = 0 Then Return

            Dim body As String = templateContent
            body = body.Replace("@customer_name@", HttpUtility.HtmlEncode(vCustomer.LastName + " " + vCustomer.FirstName))
            body = body.Replace("@customer_email@", HttpUtility.HtmlEncode(vCustomer.Email))
            body = body.Replace("@customer_phone@", HttpUtility.HtmlEncode(vCustomer.Telephone + "/" + vCustomer.Mobile))

            Dim bx As New CustomerBusinessLogic
            Dim cusAddrs As List(Of CsCustomerAddress) = bx.GetCustomerAddresses(vCustomer.ID).OrderBy(Function(a) a.ID).ToList()
            If cusAddrs.Count = 0 Then
                body = body.Replace("@customer_address@", HttpUtility.HtmlEncode("Unknown"))
            Else
                'Dim oldestAddr As CsCustomerAddress = cusAddrs.SingleOrDefault()
                Dim oldestAddr As CsCustomerAddress = cusAddrs.FirstOrDefault()
                If Not IsNothing(oldestAddr) Then
                    body = body.Replace("@customer_address@", HttpUtility.HtmlEncode(oldestAddr.PostCode & " / " & oldestAddr.Address & " / City: " & oldestAddr.City))
                Else
                    body = body.Replace("@customer_address@", HttpUtility.HtmlEncode("Unknown"))
                End If
            End If

            Dim customerAddr As CsCustomerAddress = bx.GetCustomerAddressByID(vOrder.AddressId)
            If Not IsNothing(customerAddr) Then
                body = body.Replace("@delivert_to_address@", HttpUtility.HtmlEncode(customerAddr.PostCode & " / " & customerAddr.Address & " / City: " & customerAddr.City))
            Else
                body = body.Replace("@delivert_to_address@", HttpUtility.HtmlEncode("Unknown"))
            End If

            body = body.Replace("@subtotal@", HttpUtility.HtmlEncode(vOrder.TotalAmount))
            body = body.Replace("@order_type@", HttpUtility.HtmlEncode(vOrder.OrderType))
            body = body.Replace("@payment_status@", HttpUtility.HtmlEncode(vOrder.PayStatus))
            body = body.Replace("@placed_at@", HttpUtility.HtmlEncode(vOrder.OrderDate.ToString("yyyy-MM-dd HH:mm:ss")))

            Dim sbOrderDetails As New StringBuilder()
            For vIndex As Integer = 0 To vCartItems.Count - 1
                sbOrderDetails.AppendLine("<tr>")

                'Quantity
                sbOrderDetails.Append("<td>").Append(vCartItems(vIndex).Quantity).AppendLine("</td>")
                'Item name with options
                'Dim itemWithOptions As String = vCartItems(vIndex).Product.Items.Aggregate(String.Empty, Function(current, vItem) current + ("+" + vItem.Name))
                'If String.IsNullOrWhiteSpace(itemWithOptions) Then
                '    sbOrderDetails.Append("<td>").Append(vCartItems(vIndex).Product.Name).AppendLine("</td>")
                'Else
                '    sbOrderDetails.Append("<td>").Append(vCartItems(vIndex).Product.Name + itemWithOptions).AppendLine("</td>")
                'End If
                Dim tempString As String
                tempString = vCartItems(vIndex).ItemName
                tempString.Replace("[hot]", "")
                tempString.Replace("[spicy]", "")
                tempString.Replace("[mild]", "")
                tempString.Replace("[vegan]", "")


                sbOrderDetails.Append("<td>").Append(tempString).AppendLine("</td>")

                'Unit price
                sbOrderDetails.Append("<td>").Append(vCartItems(vIndex).UnitPrice).AppendLine("</td>")
                'Price
                sbOrderDetails.Append("<td>").Append(vCartItems(vIndex).UnitPrice * vCartItems(vIndex).Quantity).AppendLine("</td>")

                sbOrderDetails.AppendLine("</tr>")
            Next

            body = body.Replace("@items@", sbOrderDetails.ToString())

            Dim emailsDic As Dictionary(Of String, String) = GetEmailsToSendOrderNotification()
            Dim msg As New MailMsg() With {
                    .Subject = EPATheme.Current.Themes.WebsiteName & " - Order Notification",
                    .Body = body
                }

            SendMail(msg, emailsDic)


        Catch ex As Exception
            _log.Error(ex)
        Finally
            'Response.Redirect("~/Default.aspx", True)
        End Try
        _log.Info("END")
    End Sub

    Private Sub SendOrderEmail2Customer(vCustomer As CsCustomer, vOrder As Order)
        _log.Info("BEGIN")
        Try
            Dim templateContent As String = ReadContentFile("~/Templates/OrderNotificationCustomer.htm")
            If String.IsNullOrWhiteSpace(templateContent) Then Return
            If IsNothing(vCustomer) Then Return

            Dim vCartItems = vOrder.OrderDetails
            If IsNothing(vCartItems) OrElse vCartItems.Count = 0 Then Return

            Dim body As String = templateContent
            body = body.Replace("@customer_name@", HttpUtility.HtmlEncode(vCustomer.LastName + " " + vCustomer.FirstName))
            body = body.Replace("@customer_email@", HttpUtility.HtmlEncode(vCustomer.Email))
            body = body.Replace("@customer_phone@", HttpUtility.HtmlEncode(vCustomer.Telephone + "/" + vCustomer.Mobile))

            Dim bx As New CustomerBusinessLogic
            Dim cusAddrs As List(Of CsCustomerAddress) = bx.GetCustomerAddresses(vCustomer.ID).OrderBy(Function(a) a.ID).ToList()
            If cusAddrs.Count = 0 Then
                body = body.Replace("@customer_address@", HttpUtility.HtmlEncode("Unknown"))
            Else
                'Dim oldestAddr As CsCustomerAddress = cusAddrs.SingleOrDefault()
                Dim oldestAddr As CsCustomerAddress = cusAddrs.FirstOrDefault()
                If Not IsNothing(oldestAddr) Then
                    body = body.Replace("@customer_address@", HttpUtility.HtmlEncode(oldestAddr.PostCode & " / " & oldestAddr.Address & " / City: " & oldestAddr.City))
                Else
                    body = body.Replace("@customer_address@", HttpUtility.HtmlEncode("Unknown"))
                End If
            End If

            Dim customerAddr As CsCustomerAddress = bx.GetCustomerAddressByID(vOrder.AddressId)
            If Not IsNothing(customerAddr) Then
                body = body.Replace("@delivert_to_address@", HttpUtility.HtmlEncode(customerAddr.PostCode & " / " & customerAddr.Address & " / City: " & customerAddr.City))
            Else
                body = body.Replace("@delivert_to_address@", HttpUtility.HtmlEncode("Unknown"))
            End If

            body = body.Replace("@subtotal@", HttpUtility.HtmlEncode(vOrder.TotalAmount))
            body = body.Replace("@order_type@", HttpUtility.HtmlEncode(vOrder.OrderType))
            body = body.Replace("@payment_status@", HttpUtility.HtmlEncode(vOrder.PayStatus))
            body = body.Replace("@placed_at@", HttpUtility.HtmlEncode(vOrder.OrderDate.ToString("yyyy-MM-dd HH:mm:ss")))

            Dim sbOrderDetails As New StringBuilder()
            For vIndex As Integer = 0 To vCartItems.Count - 1
                sbOrderDetails.AppendLine("<tr>")

                'Quantity
                sbOrderDetails.Append("<td>").Append(vCartItems(vIndex).Quantity).AppendLine("</td>")
                'Item name with options
                'Dim itemWithOptions As String = vCartItems(vIndex).Product.Items.Aggregate(String.Empty, Function(current, vItem) current + ("+" + vItem.Name))
                'If String.IsNullOrWhiteSpace(itemWithOptions) Then
                '    sbOrderDetails.Append("<td>").Append(vCartItems(vIndex).Product.Name).AppendLine("</td>")
                'Else
                '    sbOrderDetails.Append("<td>").Append(vCartItems(vIndex).Product.Name + itemWithOptions).AppendLine("</td>")
                'End If

                sbOrderDetails.Append("<td>").Append(vCartItems(vIndex).MenuItemName).AppendLine("</td>")

                'Unit price

                sbOrderDetails.Append("<td>").Append(CDec(vCartItems(vIndex).Price / vCartItems(vIndex).Quantity)).AppendLine("</td>")
                'Price
                sbOrderDetails.Append("<td>").Append(vCartItems(vIndex).Price).AppendLine("</td>")

                sbOrderDetails.AppendLine("</tr>")
            Next

            body = body.Replace("@items@", sbOrderDetails.ToString())

            Dim emailsCust As String = vCustomer.Email

            Dim msgCust As New MailMsg() With {
                    .ToAddress = emailsCust,
                    .ToDisplayName = vCustomer.LastName + " " + vCustomer.FirstName + " - Order Notification qq",
                    .Subject = EPATheme.Current.Themes.WebsiteName & " - Order Notification qqq",
            .Body = body
                }

            Dim emailsCust2 As String = "anekeen@yahoo.com"
            Dim msgCust2 As New MailMsg() With {
            .ToAddress = emailsCust2,
                    .ToDisplayName = vCustomer.LastName + " " + vCustomer.FirstName + " - Order Notification xx",
                    .Subject = EPATheme.Current.Themes.WebsiteName & " - Order Notification xxx",
            .Body = body
            }

            SendMail(msgCust)
            SendMail(msgCust2)



        Catch ex As Exception
            _log.Error(ex)
        Finally
            'Response.Redirect("~/Default.aspx", True)
        End Try
        _log.Info("END")
    End Sub

    Private Sub SendOrderEmail3Customer(vOrder As Order)
        _log.Info("BEGIN")
        Try
            Dim templateContent As String = ReadContentFile("~/Templates/OrderNotificationCustomer.htm")
            If String.IsNullOrWhiteSpace(templateContent) Then Return

            Dim vCustomer As CsCustomer = Nothing
            vCustomer = GetLoggedInCustomer()
            If IsNothing(vCustomer) Then Return

            Dim vCartItems = vOrder.OrderDetails
            If IsNothing(vCartItems) OrElse vCartItems.Count = 0 Then Return

            Dim body As String = templateContent
            body = body.Replace("@customer_name@", HttpUtility.HtmlEncode(vCustomer.LastName + " " + vCustomer.FirstName))
            body = body.Replace("@customer_email@", HttpUtility.HtmlEncode(vCustomer.Email))
            body = body.Replace("@customer_phone@", HttpUtility.HtmlEncode(vCustomer.Telephone + " / " + vCustomer.Mobile))

            Dim bx As New CustomerBusinessLogic
            Dim cusAddrs As List(Of CsCustomerAddress) = bx.GetCustomerAddresses(vCustomer.ID).OrderBy(Function(a) a.ID).ToList()
            If cusAddrs.Count = 0 Then
                body = body.Replace("@customer_address@", HttpUtility.HtmlEncode("Unknown"))
            Else
                'Dim oldestAddr As CsCustomerAddress = cusAddrs.SingleOrDefault()
                Dim oldestAddr As CsCustomerAddress = cusAddrs.FirstOrDefault()
                If Not IsNothing(oldestAddr) Then
                    body = body.Replace("@customer_address@", HttpUtility.HtmlEncode(oldestAddr.PostCode & " / " & oldestAddr.Address & " / City: " & oldestAddr.City))
                Else
                    body = body.Replace("@customer_address@", HttpUtility.HtmlEncode("Unknown"))
                End If
            End If

            Dim customerAddr As CsCustomerAddress = bx.GetCustomerAddressByID(vOrder.AddressId)
            If Not IsNothing(customerAddr) Then
                body = body.Replace("@delivert_to_address@", HttpUtility.HtmlEncode(customerAddr.PostCode & " / " & customerAddr.Address & " / City: " & customerAddr.City))
            Else
                body = body.Replace("@delivert_to_address@", HttpUtility.HtmlEncode("Unknown"))
            End If

            body = body.Replace("@subtotal@", HttpUtility.HtmlEncode(vOrder.TotalAmount))
            body = body.Replace("@order_type@", HttpUtility.HtmlEncode(vOrder.OrderType))
            body = body.Replace("@payment_status@", HttpUtility.HtmlEncode(vOrder.PayStatus))
            body = body.Replace("@placed_at@", HttpUtility.HtmlEncode(vOrder.OrderDate.ToString("yyyy-MM-dd HH:mm:ss")))

            Dim sbOrderDetails As New StringBuilder()
            For vIndex As Integer = 0 To vCartItems.Count - 1
                sbOrderDetails.AppendLine("<tr>")

                'Quantity
                sbOrderDetails.Append("<td>").Append(vCartItems(vIndex).Quantity).AppendLine("</td>")
                'Item name with options
                'Dim itemWithOptions As String = vCartItems(vIndex).Product.Items.Aggregate(String.Empty, Function(current, vItem) current + ("+" + vItem.Name))
                'If String.IsNullOrWhiteSpace(itemWithOptions) Then
                '    sbOrderDetails.Append("<td>").Append(vCartItems(vIndex).Product.Name).AppendLine("</td>")
                'Else
                '    sbOrderDetails.Append("<td>").Append(vCartItems(vIndex).Product.Name + itemWithOptions).AppendLine("</td>")
                'End If
                Dim tempString As String
                tempString = vCartItems(vIndex).MenuItemName


                If (ConfigurationManager.AppSettings("label1") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("label1")))) Then
                    tempString.Replace(ConfigurationManager.AppSettings("label1"), "")
                End If

                If (ConfigurationManager.AppSettings("label2") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("label2")))) Then
                    tempString.Replace(ConfigurationManager.AppSettings("label2"), "")
                End If

                If (ConfigurationManager.AppSettings("label3") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("label3")))) Then
                    tempString.Replace(ConfigurationManager.AppSettings("label3"), "")
                End If

                If (ConfigurationManager.AppSettings("label4") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("label4")))) Then
                    tempString.Replace(ConfigurationManager.AppSettings("label4"), "")
                End If

                If (ConfigurationManager.AppSettings("label5") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("label5")))) Then
                    tempString.Replace(ConfigurationManager.AppSettings("label5"), "")
                End If

                If (ConfigurationManager.AppSettings("label6") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("label6")))) Then
                    tempString.Replace(ConfigurationManager.AppSettings("label6"), "")
                End If

                If (ConfigurationManager.AppSettings("label7") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("label7")))) Then
                    tempString.Replace(ConfigurationManager.AppSettings("label7"), "")
                End If

                If (ConfigurationManager.AppSettings("label8") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("label8")))) Then
                    tempString.Replace(ConfigurationManager.AppSettings("label8"), "")
                End If

                If (ConfigurationManager.AppSettings("label9") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("label9")))) Then
                    tempString.Replace(ConfigurationManager.AppSettings("label9"), "")
                End If

                If (ConfigurationManager.AppSettings("label10") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("label9")))) Then
                    tempString.Replace(ConfigurationManager.AppSettings("label10"), "")
                End If
               

                sbOrderDetails.Append("<td>").Append(tempString).AppendLine("</td>")

                'Unit price

                sbOrderDetails.Append("<td>").Append(CDec(vCartItems(vIndex).Price / vCartItems(vIndex).Quantity)).AppendLine("</td>")
                'Price
                sbOrderDetails.Append("<td>").Append(vCartItems(vIndex).Price).AppendLine("</td>")

                sbOrderDetails.AppendLine("</tr>")
            Next

            body = body.Replace("@items@", sbOrderDetails.ToString())

            Dim websiteName As String = Nothing
            If (ConfigurationManager.AppSettings("orderwebsiteName") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("orderwebsiteName")))) Then
                websiteName = ConfigurationManager.AppSettings("orderwebsiteName")
            Else
                websiteName = ConfigurationManager.AppSettings("Your Order Details")
            End If

            body = body.Replace("@website_team@", HttpUtility.HtmlEncode(websiteName))

            Dim websiteEml As String = Nothing
            If (ConfigurationManager.AppSettings("orderwebsiteEmail") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("orderwebsiteEmail")))) Then
                websiteEml = ConfigurationManager.AppSettings("orderwebsiteEmail")
            Else
                websiteEml = ConfigurationManager.AppSettings("orders@restaurant.com")
            End If

            Dim websiteSubj As String = Nothing
            If (ConfigurationManager.AppSettings("orderwebsiteSubj") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("orderwebsiteSubj")))) Then
                websiteSubj = ConfigurationManager.AppSettings("orderwebsiteSubj")
            Else
                websiteSubj = ConfigurationManager.AppSettings("Your Order Details")
            End If

            Dim emailsCust As String = vCustomer.Email
            Dim customerName As String = HttpUtility.HtmlEncode(vCustomer.LastName + " " + vCustomer.FirstName)

            ' Dim emailsCust2 As String = "anekeen@yahoo.com"
            Dim msgCust2 As New MailMsg() With {
                    .ToAddress = vCustomer.Email,
                    .ToDisplayName = vCustomer.LastName + " " + vCustomer.FirstName,
                    .Subject = EPATheme.Current.Themes.WebsiteName,
            .Body = body
            }

            SendMailOrder(msgCust2, vCustomer.Email, customerName, websiteEml, websiteName, websiteSubj)

        Catch ex As Exception
            _log.Error(ex)
        Finally
            'Response.Redirect("~/Default.aspx", True)
        End Try
        _log.Info("END")
    End Sub

    Protected Function GetEmailsToSendOrderNotification() As Dictionary(Of String, String)
        _log.Info("BEGIN")

        Try
            Dim biz As New UserBusinessLogic()
            Dim allUsers As IEnumerable(Of CsUser) = biz.GetAllUsers(True)

            _log.Info("END")
            Return allUsers.ToDictionary(Function(csUser) csUser.Email, Function(csUser) csUser.LastName + " " + csUser.FirstName)

        Catch ex As Exception
            _log.Error(ex)
            _log.Info("END")
            Return Nothing
        End Try
    End Function

    Protected Function ReadContentFile(vFileName As String) As String
        _log.Info("BEGIN")

        Dim filePath As String = String.Empty
        Try
            filePath = Server.MapPath(vFileName)
            'Check if the templates exist
            If Not File.Exists(filePath) Then
                _log.Error(vFileName & " does not exist. Please check it!")
                Return String.Empty
            End If

            Dim strContent As String = String.Empty
            Using stream As New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using reader As New StreamReader(stream)
                    strContent = reader.ReadToEnd()
                End Using
            End Using
            Return strContent
        Catch ex As Exception
            _log.Error(ex)
            Return String.Empty
        End Try
        _log.Info("END")
    End Function

    Private Sub CleanUpTemporaryStorage()
        _log.Info("BEGIN")

        If Me._BasketID > 0 Then
            Dim basketBusiness As New BasketTempBusinessLogic()
            Try
                basketBusiness.DeleteBasketTemp(Me._BasketID)
            Catch ex As Exception
                _log.Error(ex)
            End Try
        End If
        _log.Info("END")
    End Sub

    Private Sub CleanUpTemporaryStorage(vBasketOrderId As Integer)
        _log.Info("BEGIN")

        If vBasketOrderId > 0 Then
            Dim basketBusiness As New BasketTempBusinessLogic()
            Try
                basketBusiness.DeleteBasketTemp(vBasketOrderId)
            Catch ex As Exception
                _log.Error(ex)
            End Try
        End If
        _log.Info("END")
    End Sub

    Private Sub UpdateTemporaryStorage(vBasketOrderId As Integer, Optional paymentStatusResult As IFormPaymentResult = Nothing)
        _log.Info("BEGIN")

        If vBasketOrderId > 0 Then
            Dim basketBusiness As New BasketTempBusinessLogic()
            Try
                Dim temp As CsBasketTemp = basketBusiness.GetBasketTempByID(vBasketOrderId)
                If temp IsNot Nothing Then

                    With temp
                        .ChangedOn = DateTime.Now
                        .ChangedBy = temp.CreatedBy

                        If paymentStatusResult IsNot Nothing Then
                            .TxAuthNo = paymentStatusResult.TxAuthNo
                            .BankAuthCode = paymentStatusResult.BankAuthCode
                            .Last4Digits = paymentStatusResult.Last4Digits
                            .VpsTxId = paymentStatusResult.VpsTxId
                            .SagePayStatus = paymentStatusResult.Status.ToString()
                        End If
                    End With

                    basketBusiness.UpdateBasketTemp(temp)
                End If
            Catch ex As Exception
                _log.Error(ex)
            End Try
        End If
        _log.Info("END")
    End Sub
End Class