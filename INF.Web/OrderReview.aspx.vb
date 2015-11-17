Imports INF.Web.Data.Entities
Imports INF.Web.Data.BLL
Imports System.Reflection
Imports INF.Web.UI.Settings
Imports INF.Web.UI.SagePay
Imports INF.Web.UI
Imports INF.Web.UI.Shopping
Imports System.IO
Imports INF.Web.UI.Logging.Log4Net
Imports System.Net
Imports SagePay.IntegrationKit.Messages
Imports Microsoft.SqlServer.Server
Imports ShoppingCart = INF.Web.Ajax.ShoppingCart

Partial Class OrderReview
    Inherits EPAPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Private Const VS_IS_NEW_ADDRESS As String = "IsNewAddress"
    Protected CardCharges As Double = 0

    Protected Property IsNewAddress() As Boolean
        Get
            Dim vIsNewAddress As Boolean = True
            If ViewState(VS_IS_NEW_ADDRESS) IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(ViewState(VS_IS_NEW_ADDRESS).ToString()) Then
                Boolean.TryParse(ViewState(VS_IS_NEW_ADDRESS).ToString(), vIsNewAddress)
            End If
            Return vIsNewAddress
        End Get
        Set(value As Boolean)
            ViewState(VS_IS_NEW_ADDRESS) = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _log.Info("BEGIN")

        Try
            ' CsCustomer has to login to take order review
            If Not IsAuthenticated Then
                Response.RedirectTo("Login.aspx")
                Exit Sub
            End If

            If IsDeliveryOrder Then
                chkDeliveryOrder.Checked = True
            Else
                chkCollectionOrder.Checked = True
            End If

            If Not IsPostBack Then


                'If File.Exists(Server.MapPath(EPATheme.Current.Themes.ConfirmOrderImageUrl)) Then
                'btnConfirmOrder.ImageUrl = EPATheme.Current.Themes.ConfirmOrderImageUrl
                'End If



                Call LoadShoppingCart()

                Call LoadCustomerAddresses()
                Call GetPaymentOptions()

                If IsItDeliveryTime() Then
                    chkDeliveryOrder.Enabled = True
                    chkDeliveryOrder.Checked = True
                    'lblDeliveryText.Text = "(Delivery order is now available)"
                Else

                    chkDeliveryOrder.Enabled = False
                    chkDeliveryOrder.Checked = True
                    'lblDeliveryText.Text = "(We are sorry, we cannot delivery any orders at the moment.)"
                End If

                UpdateCardPaymentCharges()

                If IsDeliveryOrder Then
                    radAsSoonAsPossible.Checked = True
                    InitializeDeliveryTiming(DateTime.Now.AddMinutes(45))
                End If
            Else
                ' Prevents form from being submitted multiple times in MOST cases
                ' Progamatic client-side calls to _doPostBack() call by pass this
                'Page.ClientScript.RegisterOnSubmitStatement(Me.GetType(), "ServerForm", "if (this.submitted) return false; this.submitted = true; return true;")
            End If



            
            Dim vFormIdentity As FormsIdentity = TryCast(HttpContext.Current.User.Identity, FormsIdentity)
            Dim vCustomerAddress As CsCustomerAddress = Nothing
            Dim vCustomer As CsCustomer = Nothing
            Dim customerAddresses As IEnumerable(Of CsCustomerAddress) = Nothing

            Dim vBusiness As New CustomerBusinessLogic()
            vCustomer = vBusiness.GetCustomerByEmail(vFormIdentity.Name)
            If Not IsNothing(vCustomer) Then
                customerAddresses = vBusiness.GetCustomerAddresses(vCustomer.ID)
                If Not IsNothing(customerAddresses) Then
                    vCustomerAddress = customerAddresses.AsQueryable().OrderBy(Function(c) c.ID).FirstOrDefault()
                End If
            End If

            'billFirstName.Text = vCustomer.FirstName
            'billLastName.Text = vCustomer.LastName
            'billAddress.Text = vCustomerAddress.Address
            'billCity.Text = vCustomerAddress.City
            'billPostode.Text = vCustomerAddress.PostCode

        Catch ex As Exception
            _log.Error(ex)
        End Try

        _log.Info("END")
    End Sub

    Private Sub InitializeDeliveryTiming(Optional ByVal vCurrentTime As DateTime = Nothing)
        Session("DeliveryTiming") = Nothing

        ddlAmOrPm.Items.Add(New ListItem("AM", "AM"))
        ddlAmOrPm.Items.Add(New ListItem("PM", "PM"))
        ddlAmOrPm.SelectedValue = "AM"

        For hour As Integer = 0 To 11
            ddlAtHours.Items.Add(New ListItem((hour + 1).ToString().PadLeft(2, "0"c), (hour + 1).ToString().PadLeft(2, "0"c)))
        Next

        For minute As Integer = 0 To 58
            ddlAtMinutes.Items.Add(New ListItem((minute + 1).ToString().PadLeft(2, "0"c), (minute + 1).ToString().PadLeft(2, "0"c)))
        Next

        If Not IsNothing(vCurrentTime) Then
            If vCurrentTime.Hour <= 12 Then
                ddlAmOrPm.SelectedIndex = 0
            Else
                ddlAmOrPm.SelectedIndex = 1
            End If

            ddlAtHours.SelectedValue = vCurrentTime.ToString("hh").PadLeft(2, "0"c)
            ddlAtMinutes.SelectedValue = vCurrentTime.ToString("mm").PadLeft(2, "0"c)
        End If
    End Sub

    Private Sub UpdateCardPaymentCharges()
        _log.Info("BEGIN")
        Try
            CardCharges = ConfigurationManager.AppSettings("CardFee")
            lblCardCharges.Text = String.Format("credit card fee £ {0}", FormatNumber(CardCharges, , vbTrue))
        Catch ex As Exception
            _log.Error(ex)
        End Try
        _log.Info("END")
    End Sub

    Private Function IsItDeliveryTime() As Boolean
        _log.Info("BEGIN")

        Dim vBusiness As New RestaurantBusinessLogic()
        Try
            Dim vIsDeliveryTime As Boolean = False
            Dim vStartTime, vEndTime As DateTime
            Dim vTotalSecondPassed, vTotalSecondsRemaining As Double

            Dim vDeliveryTiming As CsDeliveryTiming = vBusiness.GetDeliveryTiming(DateTime.Today.DayOfWeek)
            If vDeliveryTiming IsNot Nothing Then
                If (DateTime.TryParse(vDeliveryTiming.StartTime, vStartTime)) AndAlso (DateTime.TryParse(vDeliveryTiming.EndTime, vEndTime)) Then

                    ' Check if current time is in delivery start and end time range
                    Dim vAdjustStartTime As DateTime = vStartTime
                    If vStartTime.Hour > DateTime.Now.Hour OrElse (vStartTime.Hour = DateTime.Now.Hour AndAlso vStartTime.Minute > DateTime.Now.Minute) Then
                        vAdjustStartTime = vAdjustStartTime.AddDays(-1)
                    End If
                    Dim vTsStart As TimeSpan = DateTime.Now.Subtract(vAdjustStartTime)
                    vTotalSecondPassed = vTsStart.TotalSeconds

                    Dim vTsEnd As TimeSpan = vEndTime.Subtract(DateTime.Now)
                    vTotalSecondsRemaining = vTsEnd.TotalSeconds

                    If vTsEnd.TotalSeconds < 0 Then
                        ' Either time is passed for delivery or this time is after midnight 00:00.
                        If CInt(vDeliveryTiming.EndTime.Substring(0, 2)) < CInt(vDeliveryTiming.StartTime.Substring(0, 2)) Then

                            ' End time goes after 24 hour midnight, so add that time while checking delivery range
                            Dim vTempTime As DateTime = CType("00:00:00", DateTime)
                            vTotalSecondsRemaining = vStartTime.Subtract(vTempTime).TotalSeconds
                            vTotalSecondsRemaining = vTotalSecondsRemaining + vEndTime.Subtract(vTempTime).TotalSeconds
                        End If
                    End If

                    If vTotalSecondPassed > 0 And vTotalSecondsRemaining > 0 Then
                        vIsDeliveryTime = True
                    Else
                        vIsDeliveryTime = False
                    End If
                End If
            End If

            _log.Info("END")
            Return vIsDeliveryTime
        Catch ex As Exception
            _log.Error(ex)
            _log.Info("END")
            Return False
        End Try
    End Function

    Private Function SetSelectedAddress() As Boolean
        _log.Info("BEGIN")

        Try
            Dim vIndex As Integer = 0
            Dim vAddressIdHiddenField As HiddenField

            Dim cbAddressId As CheckBox
            For vIndex = 0 To dlCustomerAddress.Items.Count - 1
                cbAddressId = CType(dlCustomerAddress.Items(vIndex).FindControl("cbAddressId"), CheckBox)
                If Not IsNothing(cbAddressId) AndAlso cbAddressId.Checked Then
                    Exit For
                End If
            Next

            If vIndex >= dlCustomerAddress.Items.Count Then
                'not item is checked, it should be new address
                IsNewAddress = True
                Return False
            Else
                vAddressIdHiddenField = CType(dlCustomerAddress.Items(vIndex).FindControl("hfAddressId"), HiddenField)
                If Not vAddressIdHiddenField Is Nothing Then
                    Session(SSN_ADDRESS_ID) = vAddressIdHiddenField.Value
                End If
                IsNewAddress = False
            End If

            _log.Info("END")
            Return True

        Catch ex As Exception
            _log.Error(ex)
            _log.Info("END")
            Return False
        End Try
    End Function

    Private Sub LoadCustomerAddresses()
        _log.Info("BEGIN")

        Dim vBusiness As New CustomerBusinessLogic()
        Try
            Dim vCustomer As CsCustomer = GetLoggedInCustomer()
            If vCustomer Is Nothing Then
                _log.Info("END")
                Exit Sub
            End If

            HttpContext.Current.Session(SSN_CUSTOMER_ID) = vCustomer.ID
            txtFistName.Text = vCustomer.FirstName
            txtLastName.Text = vCustomer.LastName
            txtEmailAddress.Text = vCustomer.Email
            If Not String.IsNullOrWhiteSpace(vCustomer.Mobile) Then
                txtPhoneNumber.Text = vCustomer.Mobile
            Else
                txtPhoneNumber.Text = vCustomer.Telephone
            End If

            Dim vAddresses As IEnumerable(Of CsCustomerAddress) = vBusiness.GetCustomerAddresses(vCustomer.ID)
            Dim addressList As List(Of CsCustomerAddress) = vAddresses.OrderByDescending(Function(a) a.ID).ToList()

            dlCustomerAddress.DataSource = addressList
            dlCustomerAddress.DataBind()
        Catch ex As Exception
            _log.Error(ex)
        End Try

        _log.Info("END")
    End Sub

    Private Function AddNewCusomterAddress() As Boolean
        _log.Info("BEGIN")

        If Not Page.IsValid Then
            _log.Info("END")
            Return False
        End If

        Dim vBusiness As New CustomerBusinessLogic()
        Try
            Dim vCustomerAddress As New CsCustomerAddress()
            Dim vCustomer As CsCustomer = GetLoggedInCustomer()
            If vCustomer IsNot Nothing Then
                vCustomerAddress.CustomerID = vCustomer.ID
                HttpContext.Current.Session(SSN_CUSTOMER_ID) = vCustomer.ID
            Else
                Return False
            End If

            With vCustomerAddress
                .Address = txtAddressLine1.Text.Trim()
                .PostCode = txtPostcode.Text.Trim()
                .City = txtCity.Text.Trim()
                .AddressNotes = txtAddressLine2.Text.Trim()
            End With

            Dim savedAddress As CsCustomerAddress = vBusiness.SaveCustomerAddress(vCustomerAddress)
            If savedAddress IsNot Nothing Then
                HttpContext.Current.Session(SSN_ADDRESS_ID) = savedAddress.ID
                _log.Info("END")
                Return True
            End If
        Catch ex As Exception
            _log.Error(ex)
        End Try

        _log.Info("END")
        Return False
    End Function

    Private Function GetMinimumOrderValue() As Decimal
        _log.Info("BEGIN")
        Dim minOrderValue As Decimal = 0

        Try
            If Not IsNothing(Session(BxShoppingCart.SS_MINIMUM_ORDER_VALUE)) AndAlso IsNumeric(Session(BxShoppingCart.SS_MINIMUM_ORDER_VALUE)) Then
                minOrderValue = CDec(Session(BxShoppingCart.SS_MINIMUM_ORDER_VALUE))
            End If

            If minOrderValue = 0 Then
                minOrderValue = WebsiteConfig.Instance.MinOrderValue
            End If

            Session(BxShoppingCart.SS_MINIMUM_ORDER_VALUE) = minOrderValue
        Catch ex As Exception
            _log.Error(ex)
        End Try

        _log.Info("END")
        Return minOrderValue
    End Function

    Protected Sub ConfirmOrder()
        _log.Info("BEGIN")

        If IsNothing(BxShoppingCart.GetShoppingCart()) OrElse BxShoppingCart.GetShoppingCart().Items.Count = 0 Then
            Response.RedirectTo("Menu.aspx")
            _log.Info("END")
            Exit Sub
        End If

        If chkDeliveryOrder.Checked Then
            SetSelectedAddress()
        End If

        Try
            If Not IsPostBack Then
                _log.Info("END")
                Return
            End If

            If IsDeliveryOrder Then
                Dim minOrderValue As Decimal = GetMinimumOrderValue()
                If (BxShoppingCart.GetShoppingCart().GetSubTotal() < minOrderValue) Then
                    'Dim sb As New StringBuilder()
                    ' JQuery-UI
                    'sb.AppendLine("<script type = 'text/javascript'>")
                    'sb.AppendLine("    window.onload=function(){")
                    'sb.AppendLine("        $('#dialog-message-minimum-order-value').dialog({ ")
                    'sb.AppendLine("            modal: true, ")
                    'sb.AppendLine("            buttons: {")
                    'sb.AppendLine("                Ok: function () {")
                    'sb.AppendLine("                    $(this).dialog('close'); ")
                    'sb.AppendLine("                }")
                    'sb.AppendLine("            } ")
                    'sb.AppendLine("        });")
                    'sb.AppendLine("    };")
                    'sb.AppendLine("    alert('FINE');")
                    'sb.AppendLine("    window.location='Menu.aspx';")
                    'sb.AppendLine("</script>")

                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertAboutMinimumOrderValue", sb.ToString(), True)

                    'Response.RedirectTo("Menu.aspx")
                    Response.Write("<script language='javascript'>window.alert('Your Order Value must not be less than " & FormatCurrency(minOrderValue, 2) & ".');window.location='Menu.aspx';</script>")
                    _log.Info("END")
                    Exit Sub
                End If
            End If

            ' If customer has entered new address, please add it to database against that customer id
            If chkCollectionOrder.Checked Then
                ' This is collection order
                Session(SSN_ORDER_TYPE) = CNS_OT_COLLECTION
                Session(SSN_ADDRESS_ID) = -1
            Else
                If IsNothing(Session(SSN_ADDRESS_ID)) OrElse Not IsNumeric(Session(SSN_ADDRESS_ID)) Then

                    Dim sb As New StringBuilder()
                    ' JQuery-UI
                    sb.Append("<script type = 'text/javascript'>")
                    sb.Append("    window.onload=function(){")
                    sb.Append("        $('#dialog-message').dialog({ ")
                    sb.Append("            modal: true, ")
                    sb.Append("            buttons: {")
                    sb.Append("                Ok: function () {")
                    sb.Append("                    $(this).dialog('close'); ")
                    sb.Append("                }")
                    sb.Append("            } ")
                    sb.Append("        });")
                    sb.Append("    };")
                    sb.Append("</script>")

                    ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", sb.ToString())
                    Return
                End If

                Session(SSN_ORDER_TYPE) = CNS_OT_DELIVERY


                'Update timing for delivery
                Dim deliveryTimingStr As String = ""
                If radAsSoonAsPossible.Checked Then
                    deliveryTimingStr = "ASAP"
                Else
                    deliveryTimingStr = ddlAtHours.SelectedValue & ":" & ddlAtMinutes.SelectedValue & " " & ddlAmOrPm.SelectedValue
                End If
                Session("DeliveryTiming") = deliveryTimingStr
            End If

            Session(SSN_SPEC_INSTR) = txtAdditionalInstructions.Text
            Session(SSN_VOUCHER_CODE) = VoucherCodeTextBox.Text.Trim()
            Session(SSN_IS_CART_EMPTY) = False

            If chkPayByCash.Checked Then
                Session(SSN_PAYMENT_MODE) = CNS_PAYMENT_CASH
                Session(SSN_PAYMENT_CHARGES) = 0

                '****************************************************
                ' Temporary save the basket to database to avoid 
                ' losting order come back to our website.
                '****************************************************
                Call GetAndSaveOrderToBasketTemporary()
                ' Clean the shopping cart
                CleanShoppingCart()
                Response.RedirectTo("OrderWaitProcess.aspx")

                _log.Info("END")
                Exit Sub
            Else
                UpdateCardPaymentCharges()

                Session(SSN_PAYMENT_MODE) = CNS_PAYMENT_CARD
                Session(SSN_PAYMENT_CHARGES) = FormatNumber(CardCharges, 2)
                Session(SSN_TOTAL_ORDER_PRICE) = CDbl(Session(SSN_TOTAL_ORDER_PRICE)) + FormatNumber(CardCharges, 2)

                'Session("POST_STR") = Me.BuildPostString()
                Dim sagepayFormIntegrationStr As String = Me.BuildSagePayIntegration()
                If String.IsNullOrEmpty(sagepayFormIntegrationStr) Then
                    Response.RedirectTo("Error.aspx.aspx")

                    _log.Info("END")
                    Exit Sub
                Else
                    Session("POST_STR") = sagepayFormIntegrationStr
                End If

                '#If DEBUG Then
                '                    Response.Redirect("OrderWaitProcess.aspx", False)
                '#Else
                '                    Response.Redirect("OrderConfirmation.aspx", False)
                '#End If

                '****************************************************
                ' Temporary save the basket to database to avoid 
                ' losting order come back to our website.
                '****************************************************
                Call GetAndSaveOrderToBasketTemporary()

                'Response.RedirectTo("OrderConfirmation.aspx")
                Response.RedirectTo("OrderConfirm.aspx")

                _log.Info("END")
                Exit Sub
            End If

        Catch ex As Exception
            _log.Error(ex)
        End Try
        _log.Info("END")
    End Sub

    Private Sub GetPaymentOptions()
        _log.Info("BEGIN")

        Dim vBusiness As New RestaurantBusinessLogic()
        Try
            Dim vRestaurant As CsRestaurant = vBusiness.GetRestaurantInfo()
            If vRestaurant IsNot Nothing Then
                chkPayByCard.Enabled = vRestaurant.EnableNochex
                chkPayByCash.Enabled = vRestaurant.EnableCashPayments
            End If
        Catch ex As Exception
            _log.Error(ex)
        End Try
        _log.Info("END")
    End Sub

    'Private m_counter As Integer = 1

    Protected Sub DLAddresses_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataListCommandEventArgs) 'Handles DLAddresses.ItemCommand
        _log.Info("BEGIN")

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim hdnAddressId As HiddenField = DirectCast(e.Item.FindControl("HField"), HiddenField)
            If Not IsNothing(hdnAddressId) AndAlso IsNumeric(hdnAddressId.Value) Then
                Dim addressId As Decimal = CDec(hdnAddressId.Value)
                Dim business As New CustomerBusinessLogic()
                Try
                    business.DeleteCustomerAddress(addressId)
                    Call LoadCustomerAddresses()
                Catch ex As Exception
                    _log.Error(ex)
                End Try
            End If
        End If

        _log.Info("END")
    End Sub
    Protected Sub DLAddresses_ItemDataBound(ByVal sender As Object, ByVal e As DataListItemEventArgs) 'Handles DLAddresses.ItemDataBound
        _log.Info("BEGIN")

        Try
            If e.Item.ItemType <> ListItemType.Item AndAlso e.Item.ItemType <> ListItemType.AlternatingItem Then
                _log.Info("END")
                Return
            End If

            Dim removeAddressLink As LinkButton = DirectCast(e.Item.FindControl("lnkRemoveAddress"), LinkButton)
            If Not IsNothing(removeAddressLink) Then

            End If

            Dim rdo As RadioButton = DirectCast(e.Item.FindControl("rdoSelected"), RadioButton)
            Dim vScripts As String = "SetUniqueRadioButton('DLAddresses.*grpAddress',this)"
            rdo.Attributes.Add("onclick", vScripts)

        Catch ex As Exception
            _log.Error(ex)
        End Try
        _log.Info("END")
    End Sub

    Private Sub SetErrorMessage(ByRef txt As TextBox, ByVal msg As String, ByVal isError As Boolean)
        _log.Info("BEGIN")

        Try
            If isError Then
                txt.BackColor = Drawing.Color.LightYellow
            Else
                txt.BackColor = Drawing.Color.White
            End If

        Catch ex As Exception
            _log.Error(ex)
        End Try
        _log.Info("END")
    End Sub

    Protected Sub EditOrderButton2_Click(sender As Object, e As System.EventArgs) Handles EditOrderButton2.Click
        _log.Info("BEGIN")
        Response.RedirectTo("Menu.aspx")
        _log.Info("END")
    End Sub

#Region "Shopping Cart"

    Private Sub LoadShoppingCart()
        _log.Info("BEGIN")
        txtAdditionalInstructions.Text = BxShoppingCart.GetShoppingCart().AddtitionalInstruction
        _log.Info("END")
    End Sub

    Protected Sub RptCartItems_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        _log.Info("BEGIN")

        Try
            If (e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem) Then

                Dim cartItem As BxCartItem = DirectCast(e.Item.DataItem, BxCartItem)
                If (cartItem Is Nothing) Then
                    _log.Info("END")
                    Return
                End If

                Dim subItemRepeater As Repeater = DirectCast(e.Item.FindControl("rptSubCartItems"), Repeater)
                If (subItemRepeater Is Nothing) Then
                    _log.Info("END")
                    Return
                End If

                subItemRepeater.DataSource = cartItem.Items
                subItemRepeater.DataBind()
            End If

            'If we are binding the footer row, let's add in our total
            If (e.Item.ItemType = ListItemType.Footer) Then

                Dim lblPostCodePrice = DirectCast(e.Item.FindControl("lblPostCodePrice"), Literal)
                Dim lblTotalPrice = DirectCast(e.Item.FindControl("lblTotalPrice"), Literal)
                Dim lblSubTotalPrice = DirectCast(e.Item.FindControl("lblSubTotal"), Literal)
                Dim vTotalPrice As Decimal = 0

                If (lblPostCodePrice IsNot Nothing) Then
                    If (HttpContext.Current.Session(BxShoppingCart.POST_CODE_CHARGE) IsNot Nothing) Then

                        lblPostCodePrice.Text = "£" + Convert.ToDecimal(HttpContext.Current.Session(BxShoppingCart.POST_CODE_CHARGE)).ToString("N2")
                        vTotalPrice = vTotalPrice + Convert.ToDecimal(HttpContext.Current.Session(BxShoppingCart.POST_CODE_CHARGE))
                    End If
                End If

                If (lblTotalPrice IsNot Nothing AndAlso lblSubTotalPrice IsNot Nothing) Then
                    vTotalPrice = vTotalPrice + BxShoppingCart.GetShoppingCart().GetSubTotal()
                    lblSubTotalPrice.Text = "£" + BxShoppingCart.GetShoppingCart().GetSubTotal().ToString("N2")

                    lblTotalPrice.Text = "£" + vTotalPrice.ToString("N2")
                End If

            End If
        Catch ex As Exception
            _log.Error(ex)
        End Try

        _log.Info("END")
    End Sub

#End Region

    Private Function BuildSagePayIntegration() As String
        _log.Info("BEGIN")

        Try
            Dim sagePayFormIntegration As New SagePayFormIntegration()
            Dim fromRequest As IFormPayment = sagePayFormIntegration.FormPaymentRequest()
            SetSagePayAPIData(fromRequest)

            Dim errors As NameValueCollection = sagePayFormIntegration.Validation(fromRequest)
            If IsNothing(errors) OrElse errors.Count = 0 Then

                Session("SS_CARD_PAYMENT_FORM_REQUEST") = fromRequest
                sagePayFormIntegration.ProcessRequest(fromRequest)

                _log.Info("END")
                Return fromRequest.Crypt
            Else

                _log.Info("END")
                Return String.Empty
            End If
        Catch ex As Exception
            _log.Error(ex)
        End Try

        _log.Info("END")
        Return String.Empty
    End Function

    Private Sub SetSagePayAPIData(ByRef vRequest As IFormPayment)
        _log.Info("BEGIN")

        Try
            Dim isCollectRecipientDetails As Boolean = SagePaySettings.IsCollectRecipientDetails
            Dim cart As BxShoppingCart = BxShoppingCart.GetShoppingCart()

            vRequest.VpsProtocol = SagePaySettings.ProtocolVersion
            vRequest.TransactionType = SagePaySettings.DefaultTransactionType
            vRequest.Vendor = SagePaySettings.VendorName

            'Assign Vendor tx Code.
            vRequest.VendorTxCode = SagePayFormIntegration.GetNewVendorTxCode()

            Dim decTotal As Decimal = BxShoppingCart.GetShoppingCart().GetSubTotal()
            If chkDeliveryOrder.Checked Then
                If Not IsNothing(HttpContext.Current.Session(BxShoppingCart.POST_CODE_CHARGE)) Then
                    decTotal = decTotal + Convert.ToDecimal(HttpContext.Current.Session(BxShoppingCart.POST_CODE_CHARGE))
                End If
            End If
            decTotal = decTotal - ((OnlineDiscount * BxShoppingCart.GetShoppingCart().GetSubTotalExclCategoriesDonotHaveOnlineDiscount()) / 100)
            decTotal = decTotal + CDec(WebsiteConfig.Instance.PayByCardFee)

            vRequest.Amount = FormatNumber(decTotal, 2, -1, 0, 0)
            vRequest.Currency = SagePaySettings.Currency
            vRequest.Description = "" + SagePaySettings.VendorName
            vRequest.SuccessUrl = SagePaySettings.SiteFqdn + "OrderWaitProcess.aspx"
            vRequest.FailureUrl = SagePaySettings.SiteFqdn + "PaymentError.aspx"

            Dim vFormIdentity As FormsIdentity = TryCast(HttpContext.Current.User.Identity, FormsIdentity)
            Dim vFullName As String = String.Empty
            If (vFormIdentity IsNot Nothing) Then
                vFullName = vFormIdentity.Ticket.UserData
            End If

            Dim vCustomerAddress As CsCustomerAddress = Nothing
            Dim vCustomer As CsCustomer = Nothing
            Dim customerAddresses As IEnumerable(Of CsCustomerAddress) = Nothing

            Dim vBusiness As New CustomerBusinessLogic()
            vCustomer = vBusiness.GetCustomerByEmail(vFormIdentity.Name)
            If Not IsNothing(vCustomer) Then
                customerAddresses = vBusiness.GetCustomerAddresses(vCustomer.ID)
                If Not IsNothing(customerAddresses) Then
                    vCustomerAddress = customerAddresses.AsQueryable().OrderBy(Function(c) c.ID).FirstOrDefault()
                End If
            End If

            If Not IsNothing(vCustomerAddress) Then


                vRequest.BillingSurname = vCustomer.LastName
                vRequest.BillingFirstnames = vCustomer.FirstName
                vRequest.BillingAddress1 = vCustomerAddress.Address
                vRequest.BillingCity = vCustomerAddress.City
                vRequest.BillingPostCode = vCustomerAddress.PostCode
                vRequest.BillingCountry = "GB"

                If Not IsNothing(Session(SSN_ADDRESS_ID)) Then
                    Dim deliveryAddress As CsCustomerAddress = customerAddresses.AsQueryable().FirstOrDefault(Function(c) c.ID = CDec(Session(SSN_ADDRESS_ID)))
                    vRequest.DeliverySurname = vCustomer.LastName
                    vRequest.DeliveryFirstnames = vCustomer.FirstName

                    If Not IsNothing(deliveryAddress) Then
                        vRequest.DeliveryAddress1 = deliveryAddress.Address
                        vRequest.DeliveryCity = deliveryAddress.City
                        vRequest.DeliveryPostCode = deliveryAddress.PostCode
                        vRequest.DeliveryCountry = "GB"
                    Else
                        vRequest.DeliveryAddress1 = vRequest.BillingAddress1
                        vRequest.DeliveryCity = vRequest.BillingCity
                        vRequest.DeliveryPostCode = vRequest.BillingPostCode
                        vRequest.DeliveryCountry = "GB"
                    End If
                Else
                    vRequest.DeliveryAddress1 = vRequest.BillingAddress1
                    vRequest.DeliveryCity = vRequest.BillingCity
                    vRequest.DeliveryPostCode = vRequest.BillingPostCode
                    vRequest.DeliveryCountry = "GB"
                End If

                If ConfigurationManager.AppSettings("enableBilling") IsNot Nothing Then
                    vRequest.BillingSurname = billLastName.Text
                    vRequest.BillingFirstnames = billFirstName.Text
                    vRequest.BillingAddress1 = billAddress.Text
                    vRequest.BillingCity = billCity.Text
                    vRequest.BillingPostCode = billPostode.Text
                    vRequest.BillingCountry = "GB"
                End If
               
            Else
                Dim pos As Integer = vFullName.IndexOf(" ", System.StringComparison.Ordinal)

                If pos > 0 Then
                    vRequest.BillingSurname = vFullName.Substring(pos + 1)
                    vRequest.BillingFirstnames = vFullName.Substring(0, pos)
                Else
                    vRequest.BillingSurname = ""
                    vRequest.BillingFirstnames = vFullName
                End If

                vRequest.BillingAddress1 = "N/A"
                vRequest.BillingCity = "N/A"
                vRequest.BillingPostCode = ConfigurationManager.AppSettings("billingDefaultPostCode")
                vRequest.BillingCountry = "GB"

                vRequest.DeliverySurname = vRequest.BillingSurname
                vRequest.DeliveryFirstnames = vRequest.BillingFirstnames
                vRequest.DeliveryAddress1 = vRequest.BillingAddress1
                vRequest.DeliveryPostCode = vRequest.BillingPostCode
                vRequest.DeliveryCity = vRequest.BillingCity
                vRequest.DeliveryCountry = "GB"

                If ConfigurationManager.AppSettings("enableBilling") IsNot Nothing Then
                    vRequest.BillingSurname = billLastName.Text
                    vRequest.BillingFirstnames = billFirstName.Text
                    vRequest.BillingAddress1 = billAddress.Text
                    vRequest.BillingCity = billCity.Text
                    vRequest.BillingPostCode = billPostode.Text
                    vRequest.BillingCountry = "GB"
                End If

            End If

            'Optional
            vRequest.CustomerName = vFullName
            vRequest.CustomerEmail = vFormIdentity.Name
            vRequest.VendorEmail = SagePaySettings.VendorEmail
            vRequest.SendEmail = SagePaySettings.SendEmail
            vRequest.EmailMessage = SagePaySettings.EmailMessage

            'request.BillingAddress2 = cart.Billing.Address2
            'request.BillingPostCode = cart.Billing.PostCode
            'request.BillingState = cart.Billing.Region
            'request.BillingPhone = cart.Billing.Phone
            'request.DeliveryAddress2 = cart.Shipping.Address2
            'request.DeliveryPostCode = cart.Shipping.PostCode
            'request.DeliveryState = cart.Shipping.Region
            'request.DeliveryPhone = cart.Shipping.Phone

            vRequest.AllowGiftAid = SagePaySettings.AllowGiftAid
            vRequest.ApplyAvsCv2 = SagePaySettings.ApplyAvsCv2
            vRequest.Apply3dSecure = SagePaySettings.Apply3dSecure

            vRequest.BillingAgreement = ""
            vRequest.ReferrerId = SagePaySettings.ReferrerID
            'vRequest.BasketXml = cart.ToXml()

            Dim intBasketItems As Integer = 0
            Dim strBasket As String = String.Empty

            Dim vDeliveryCharge As Double = 0
            If Not IsNothing(HttpContext.Current.Session(BxShoppingCart.POST_CODE_CHARGE)) Then
                vDeliveryCharge = CDec(HttpContext.Current.Session(BxShoppingCart.POST_CODE_CHARGE))
            End If

            For Each item As BxCartItem In BxShoppingCart.GetShoppingCart().Items
                'strBasket = strBasket & ":" & item.Description & ":" & item.Quantity
                'strBasket = strBasket & ":" & FormatNumber(item.UnitPrice, 2, -1, 0, 0) '** Price ex-Vat **
                'strBasket = strBasket & ":" & FormatNumber(item.UnitPrice, 2, -1, 0, 0) '** VAT component **
                'strBasket = strBasket & ":" & FormatNumber(item.UnitPrice, 2, -1, 0, 0) '** Item price **
                'strBasket = strBasket & ":" & FormatNumber(item.UnitPrice * item.Quantity, 2, -1, 0, 0) '** Line total **			

                If (item.DealID > 0) Then
                    strBasket = strBasket & ":" & item.Description & ":" & item.Quantity
                    strBasket = strBasket & ":" & FormatNumber(item.UnitPrice, 2, -1, 0, 0) '** Price ex-Vat **
                    strBasket = strBasket & ":" & FormatNumber(item.UnitPrice, 2, -1, 0, 0) '** VAT component **
                    strBasket = strBasket & ":" & FormatNumber(item.UnitPrice, 2, -1, 0, 0) '** Item price **
                    strBasket = strBasket & ":" & FormatNumber(item.UnitPrice * item.Quantity, 2, -1, 0, 0) '** Line total **		
                Else
                    Dim menuItem As BxMenuItem = CType(item.Product.Items(0), BxMenuItem)

                    strBasket = strBasket & ":" & menuItem.Name & ":" & item.Quantity
                    strBasket = strBasket & ":" & FormatNumber(menuItem.UnitPrice, 2, -1, 0, 0) '** Price ex-Vat **
                    strBasket = strBasket & ":" & FormatNumber(menuItem.UnitPrice, 2, -1, 0, 0) '** VAT component **
                    strBasket = strBasket & ":" & FormatNumber(menuItem.UnitPrice, 2, -1, 0, 0) '** Item price **
                    strBasket = strBasket & ":" & FormatNumber(menuItem.UnitPrice * item.Quantity, 2, -1, 0, 0) '** Line total **		
                End If

                intBasketItems = intBasketItems + 1
            Next

            ' Only add this when it's delivery order
            If chkDeliveryOrder.Checked Then
                strBasket = intBasketItems + 1 & strBasket & ":Delivery:1:" & FormatNumber(vDeliveryCharge, 2) & ":-:" & FormatNumber(vDeliveryCharge, 2) & ":" & FormatNumber(vDeliveryCharge, 2) & ""
            Else
                strBasket = intBasketItems & strBasket
            End If
            vRequest.Basket = strBasket

            'set vendor data
            vRequest.VendorData = "" 'Use this to pass any data you wish to be displayed against the transaction in My Sage Pay.
        Catch ex As Exception
            _log.Error(ex)
        End Try
        _log.Info("END")
    End Sub

    Protected ReadOnly Property OnlineDiscount As Decimal
        Get
            If (Session("Online_Discount") IsNot Nothing AndAlso Session("Online_Discount").ToString().Length > 0) Then
                Return Convert.ToDecimal(Session("Online_Discount"))
            End If
            Return 0
        End Get
    End Property

    Protected Sub dlCustomerAddress_OnItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs)
        _log.Info("BEGIN")

        If e.CommandName = "DeleteAddress" Then
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim hdnAddressId As HiddenField = DirectCast(e.Item.FindControl("hfAddressId"), HiddenField)
                If Not IsNothing(hdnAddressId) AndAlso IsNumeric(hdnAddressId.Value) Then
                    Dim addressId As Decimal = CDec(hdnAddressId.Value)
                    Dim business As New CustomerBusinessLogic()
                    Try
                        business.DeleteCustomerAddress(addressId)
                        Call LoadCustomerAddresses()
                    Catch ex As Exception
                        _log.Error(ex)
                    End Try
                End If
            End If
        End If

        _log.Info("END")
    End Sub

    Protected Property IsSubmittingNewAddress() As Boolean
        Get
            Return CBool(Session("IsSubmittingNewAddress"))
        End Get
        Set(value As Boolean)
            Session("IsSubmittingNewAddress") = value
        End Set
    End Property

    Protected Sub OnNewAddress(ByVal sender As Object, ByVal e As EventArgs)
        _log.Info("BEGIN")

        txtAddressLine1.Text = String.Empty
        txtAddressLine2.Text = String.Empty
        txtCity.Text = String.Empty
        txtPostcode.Text = String.Empty

        IsSubmittingNewAddress = True
        mpeUpdateAddress.Show()

        'ClientScript.RegisterStartupScript(Me.GetType(), "openAddressPopup", "openAddressPopup();", True)

        _log.Info("END")
    End Sub

    Protected Sub OnSaveAddress(ByVal sender As Object, ByVal e As EventArgs)
        _log.Info("BEGIN")

        If Not Page.IsValid Then
            mpeUpdateAddress.Show()

            _log.Info("END")
            Return
        End If

        Dim businessLogic As New CustomerBusinessLogic()

        Try
            If IsSubmittingNewAddress Then
                IsSubmittingNewAddress = False

                Dim newAddress As New CsCustomerAddress() With
                    {
                        .Address = txtAddressLine1.Text.Trim(),
                        .AddressNotes = txtAddressLine2.Text.Trim(),
                        .City = txtCity.Text.Trim(),
                        .PostCode = txtPostcode.Text.Trim(),
                        .CustomerID = CDec(Session(SSN_CUSTOMER_ID))
                    }
                businessLogic.SaveCustomerAddress(newAddress)
                'HttpContext.Current.Session(SSN_ADDRESS_ID) = newAddress.ID

                txtAddressLine1.Text = String.Empty
                txtAddressLine2.Text = String.Empty
                txtCity.Text = String.Empty
                txtPostcode.Text = String.Empty
                mpeUpdateAddress.Hide()
            End If

            Dim addresses As List(Of CsCustomerAddress) = businessLogic.GetCustomerAddresses(CDec(Session(SSN_CUSTOMER_ID))).ToList()
            If Not IsNothing(addresses) Then
                dlCustomerAddress.DataSource = addresses
                dlCustomerAddress.DataBind()
            End If
        Catch ex As Exception
            _log.Error(ex)
        End Try
        _log.Info("END")
    End Sub

    Private Function GetAndSaveOrderToBasketTemporary() As Boolean
        _log.Info("BEGIN")

        Dim vIsSuccessful As Boolean = False
        Dim vCustomer As CsCustomer = Nothing

        ' Check if customer logged in
        Try
            vCustomer = GetLoggedInCustomer()
            If vCustomer Is Nothing Then
                _log.Error("Could not find the customer!")
                Response.RedirectTo(WebConstants.ERROR_PAGE)
                Return False
            End If
            _log.Debug("Customer: " & vCustomer.LastName & " " & vCustomer.FirstName & " is placing order.")

            BxShoppingCart.GetShoppingCart().DeliveryCharge = CDec(Session(BxShoppingCart.POST_CODE_CHARGE))
            _log.Debug("Delivery Charge = " & BxShoppingCart.GetShoppingCart().DeliveryCharge)
        Catch ex As Exception
            _log.Error("Error when checking customer information before performnin payment.", ex)
            Response.RedirectTo(WebConstants.ERROR_PAGE)
        End Try

        ' Gets order information and store it in BasketTemp
        If (BxShoppingCart.GetShoppingCart().Items IsNot Nothing) AndAlso (BxShoppingCart.GetShoppingCart().Items.Count > 0) Then

            Dim vCartItems = BxShoppingCart.GetShoppingCart().Items
            Dim vOrder As New CsBasketTemp()
            Try

                Dim vTotalOrderPrice As Double = BxShoppingCart.GetShoppingCart().GetTotal()

                Dim vOrderItems As String = ""
                Dim vOrderItemsQty As String = ""
                Dim vOrderItemsPrice As String = ""

                vOrder = New CsBasketTemp()
                With vOrder
                    .UserLogIn = vCustomer.ID & "_" & vCustomer.Email
                    .CustomerID = CType(vCustomer.ID, Integer)
                    .OrderType = CStr(Session(SSN_ORDER_TYPE))
                    .OrderStatus = "NEW"
                    .TotalAmount = vTotalOrderPrice

                    If Session(SSN_PAYMENT_MODE) = CNS_PAYMENT_CASH Then
                        .AmountReceived = 0
                        .AmountDue = vTotalOrderPrice
                        '.PayStatus = "NOT PAID" -> as always be NOT PAID for payment by CASH
                    Else
                        .AmountReceived = vTotalOrderPrice
                        .AmountDue = 0
                        '.PayStatus = "PAID" -> this will be updated when getting response from SagePay
                    End If
                    .PayStatus = "NOT PAID"

                    If Session(SSN_VOUCHER_CODE) IsNot Nothing Then
                        .VoucherCode = CStr(Session(SSN_VOUCHER_CODE))
                    End If

                    .DiscountType = "ONLINE DISCOUNT"
                    .Discount = (BxShoppingCart.GetShoppingCart().DiscountInPercent * BxShoppingCart.GetShoppingCart().GetSubTotalExclCategoriesDonotHaveOnlineDiscount()) / 100

                    .DeliveryCharge = CDbl(Session(BxShoppingCart.POST_CODE_CHARGE))
                    .PaymentCharge = CDbl(Session(SSN_PAYMENT_CHARGES))
                    .PaymentType = CStr(Session(SSN_PAYMENT_MODE))

                    .SpecialInstructions = CStr(Session(SSN_SPEC_INSTR))
                    .AddressID = CInt(Session(SSN_ADDRESS_ID))
                    .OrderDate = DateTime.Now

                    .ExpectedTime = Session("DeliveryTiming")
                End With

                _log.Debug("The order as following:")
                _log.Debug("- OrderType: " & vOrder.OrderType)
                _log.Debug("- TotalAmount: " & vOrder.TotalAmount)
                _log.Debug("- AmountReceived: " & vOrder.AmountReceived)
                _log.Debug("- AmountDue: " & vOrder.AmountDue)
                _log.Debug("- PayStatus: " & vOrder.PayStatus)
                _log.Debug("- Discount: " & vOrder.Discount)
                _log.Debug("- DeliveryCharges: " & vOrder.DeliveryCharge)
                _log.Debug("- PaymentCharges: " & vOrder.PaymentCharge)
                _log.Debug("- SpecialInstructions: " & vOrder.SpecialInstructions)
                _log.Debug("- OrderDate: " & vOrder.OrderDate.ToString("yyyy-MM-dd HH:mm:ss"))
                _log.Debug("- Items: " & vCartItems.Count)

                vOrder.Items = New List(Of CsBasketItemTemp)

                For vIndex As Integer = 0 To vCartItems.Count - 1
                    Dim vOrderDetail As New CsBasketItemTemp()

                    With vOrderDetail
                        .BasketID = vOrder.ID
                        .Quantity = vCartItems(vIndex).Quantity
                        .SpecialRequest = ""

                        ' Is Deal
                        If vCartItems(vIndex).DealID > 0 Then

                            .UnitPrice = vCartItems(vIndex).UnitPrice
                            Dim dealWithItems As String = String.Empty

                            For Each item As GenericItem In vCartItems(vIndex).Product.Items
                                Dim menuWithItems As String = String.Empty
                                If TypeOf (item) Is BxMenuItem Then
                                    Dim menuItem As BxMenuItem = CType(item, BxMenuItem)
                                    Dim itemWithOptions As String = menuItem.Items.Aggregate(String.Empty, Function(current, subItem) current + ("+" + subItem.Name))
                                    If String.IsNullOrWhiteSpace(itemWithOptions) Then
                                        menuWithItems = item.Name
                                    Else
                                        menuWithItems = item.Name & itemWithOptions
                                    End If

                                    dealWithItems = dealWithItems & "@" & menuWithItems
                                End If
                            Next

                            If String.IsNullOrWhiteSpace(dealWithItems) Then
                                .ItemName = vCartItems(vIndex).Product.Name
                            Else
                                .ItemName = vCartItems(vIndex).Product.Name + dealWithItems
                            End If

                            .Status = True

                            Dim vDressings As String = ""
                            'For Each vItem As GenericItem In vCartItems(vIndex).Product.Items
                            '    If vItem.ItemType = ItemTypes.MenuDressing Then
                            '        vDressings = vDressings & vItem.Name & ","
                            '    End If
                            'Next
                            'If vDressings.Length > 1 Then
                            '    vDressings = vDressings.Substring(0, vDressings.Length - 1)
                            'End If
                            .Dressing = vDressings
                        Else
                            Dim menuItem As BxMenuItem = CType(vCartItems(vIndex).Product.Items(0), BxMenuItem)
                            .UnitPrice = menuItem.UnitPrice
                            ' UnitPrice should include its dressing,topping or option prices
                            If Not IsNothing(menuItem.Items) AndAlso menuItem.Items.Count > 0 Then
                                For Each subItem As GenericItem In menuItem.Items
                                    If subItem.ItemType <> ItemTypes.MenuDressing AndAlso subItem.ItemType <> ItemTypes.MenuTopping AndAlso subItem.ItemType <> ItemTypes.MenuOption Then
                                        Continue For
                                    End If

                                    If subItem.UnitPrice > 0 Then
                                        .UnitPrice = .UnitPrice + subItem.UnitPrice
                                    End If
                                Next
                            End If

                            Dim itemWithOptions As String = menuItem.Items.Aggregate(String.Empty, Function(current, vItem) current + ("+" + vItem.Name))

                            If String.IsNullOrWhiteSpace(itemWithOptions) Then
                                .ItemName = menuItem.Name
                            Else
                                .ItemName = menuItem.Name + itemWithOptions
                            End If

                            .Status = True

                            Dim vDressings As String = ""
                            For Each vItem As GenericItem In menuItem.Items
                                If vItem.ItemType = ItemTypes.MenuDressing Then
                                    vDressings = vDressings & vItem.Name & ","
                                End If
                            Next
                            If vDressings.Length > 1 Then
                                vDressings = vDressings.Substring(0, vDressings.Length - 1)
                            End If
                            .Dressing = vDressings
                        End If
                    End With

                    vOrderItems = vOrderItems & vCartItems(vIndex).Product.Name & ","
                    vOrderItemsQty = vOrderItemsQty & CStr(vCartItems(vIndex).Quantity) & ","
                    vOrderItemsPrice = vOrderItemsPrice & CStr(vCartItems(vIndex).TotalPrice) & ","

                    vOrder.Items.Add(vOrderDetail)
                Next
            Catch ex As Exception
                _log.Error("Error when getting order information to store before performing payment.", ex)
                Response.RedirectTo(WebConstants.ERROR_PAGE)
            End Try

            Dim vBusiness As New BasketTempBusinessLogic()
            Try

                ' Extra information
                With vOrder
                    .CreatedOn = DateTime.Now
                    .CreatedBy = vCustomer.Email
                End With

                Dim vSavedOrder As CsBasketTemp = vBusiness.SaveBasketTemp(vOrder)
                If vSavedOrder Is Nothing Then
                    vIsSuccessful = False
                Else
                    vIsSuccessful = True

                    ' Keep the ID of the temp order information in session for checking at payment later on
                    ' Please keep in mind to clean it up when payment done
                    Session(WebConstants.PLACING_ORDER_TEMP_BASKET_ID) = String.Format("{0}_{1}", vCustomer.Email, vSavedOrder.ID)

                    ' Try to keep the id in cookies
                    Dim ck As HttpCookie = New HttpCookie("AspNet_infinitysol")
                    ck.Values.Add(WebConstants.PLACING_ORDER_TEMP_BASKET_ID, String.Format("{0}_{1}", vCustomer.Email, vSavedOrder.ID))
                    ck.Expires = DateTime.Now.AddMinutes(60)
                    Response.Cookies.Add(ck)
                End If

            Catch vException As Exception
                vIsSuccessful = False
                _log.Error("Error when saving order information to BasketTemp.", vException)
            End Try

            Session(SSN_ADDRESS_ID) = Nothing

            ' Send emails
            If Not vIsSuccessful Then
                Response.RedirectTo(WebConstants.ERROR_PAGE)
            End If
        End If

        _log.Info("END")
        Return vIsSuccessful
    End Function
End Class