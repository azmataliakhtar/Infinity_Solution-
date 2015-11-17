Imports INF.Web.Data.BLL
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net
Imports SagePay.IntegrationKit.Messages
Imports INF.Web.UI.Shopping

Public Class OrderConfirm
    Inherits EPAPage
    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Private _crypt As String
    Public Property Crypt() As String
        Get
            Return _crypt
        End Get
        Set(value As String)
            _crypt = value
        End Set
    End Property

    Protected ReadOnly Property IsDeliveryOrder() As Boolean
        Get
            Dim blnDeliveryOrder As Boolean = (Session(BxShoppingCart.SS_ORDER_TYPE) IsNot Nothing) _
                                              AndAlso Convert.ToString(Session(BxShoppingCart.SS_ORDER_TYPE)) = BxShoppingCart.ORDER_TYPE_DELIVERY
            Return blnDeliveryOrder
        End Get
    End Property

    Protected Property OnlineDiscount() As Decimal
        Get
            If Session("Online_Discount") IsNot Nothing AndAlso Session("Online_Discount").ToString().Length > 0 Then
                Return Convert.ToDecimal(Session("Online_Discount"))
            End If
            Return 0
        End Get
        Set(value As Decimal)
            Session("Online_Discount") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _log.Info("BEGIN")
        ' Makes sure the customer has logged in
        If Not IsAuthenticated Then
            _log.Error("The Customer has not logged in yet.")
            Response.RedirectTo("Login.aspx")
            Exit Sub
        End If

        If Not IsPostBack Then
            Page.Form.Action = INF.Web.UI.SagePay.SagePaySettings.FormPaymentUrl

            If Not IsNothing(Session("POST_STR")) AndAlso CStr(Session("POST_STR")).Length > 0 Then
                Crypt = CStr(Session("POST_STR"))
                Session("POST_STR") = Nothing

                BindData()

                phSubmitPayment.Visible = True

                ' _log.Error("Bogdan debugging:")
                '_log.Error(Session("SS_CARD_PAYMENT_FORM_REQUEST"))



                If Not IsNothing(Session("SS_CARD_PAYMENT_FORM_REQUEST")) Then
                    Dim formPayment As IFormPayment = DirectCast(Session("SS_CARD_PAYMENT_FORM_REQUEST"), IFormPayment)
                    If Not IsNothing(formPayment) Then
                        With formPayment
                            'ltrBillingName.Text = .BillingFirstnames & " " & .BillingSurname
                            billingNamePlaceID.Text = .BillingFirstnames

                            ltrBillingName.Text = .BillingFirstnames & " " & .BillingSurname
                            ltrBillingAddress.Text = .BillingAddress1 & vbCrLf & .BillingPostCode & vbCrLf & .BillingCity & .BillingCountry
                            ltrBillingPhoneNo.Text = .BillingPhone
                            ltrBillingEmail.Text = .CustomerEmail

                            ltrDeliveryName.Text = .DeliveryFirstnames & " " & .DeliverySurname
                            ltrDeliveryAddress.Text = .DeliveryAddress1 & vbCrLf & .DeliveryPostCode & vbCrLf & .DeliveryCity & .DeliveryCountry
                            ltrDeliveryPhoneNo.Text = .DeliveryPhone
                            ltrDeliveryEmail.Text = .CustomerEmail

                            ' ltrDeliverTiming = "ASAP"

                            ltrDeliverTiming.Text = ConfigurationManager.AppSettings("deliveryTimeMin")
                            'ltrDeliverTiming.Text = Session("DeliveryTiming")

                        End With
                    End If

                    'formPayment.BillingCity = "London"
                    'formPayment.DeliveryFirstnames = "Jhonnyx"
                    'formPayment.BillingAddress1 = "AnaOne"
                    'formPayment.BillingAddress2 = "AnaTwo"
                    'ltrBillingAddress.Text = "Ion Mihalache, Coventry"
                End If
                _log.Debug("Here is ready for Proceed the card payment.")
            Else
                _log.Error("There is not FormPayment is setup!")
                phSubmitPayment.Visible = False
                Response.RedirectTo("Menu.aspx")
                Exit Sub
            End If
        Else
            '' Clean the shopping cart
            CleanShoppingCart()
        End If

        _log.Info("BEGIN")
    End Sub

    Private Sub BindData()
        _log.Info("BEGIN")

        Try
            Dim resBll = New RestaurantBusinessLogic()

            BxShoppingCart.GetShoppingCart().IsDeliveryOrder = IsDeliveryOrder
            Dim onlineDis = resBll.GetOnlineDiscount(IsDeliveryOrder, CInt(DateTime.Today.DayOfWeek))
            BxShoppingCart.GetShoppingCart().CollectionSpecialDiscountOrderValue = resBll.GetSpecialDiscountOrderValue()
            BxShoppingCart.GetShoppingCart().CollectionSpecialDiscount = resBll.GetSpecialDiscount()
            BxShoppingCart.GetShoppingCart().DeliverySpecialDiscountOrderValue = resBll.GetDeliverySpecialDiscountOrderValue()
            BxShoppingCart.GetShoppingCart().DeliverySpecialDiscount = resBll.GetDeliverySpecialDiscount()

            'OnlineDiscount = CDec(onlineDis)
            BxShoppingCart.GetShoppingCart().DiscountInPercent = CDec(onlineDis)
            OnlineDiscount = BxShoppingCart.GetShoppingCart().DiscountInPercent

            ' Let's give the data to the GridView and let it work!
            ' The GridView will take our cart items one by one and use the properties
            ' that we declared as column names (DataFields)
            Dim itemList As List(Of BxCartItem) = BxShoppingCart.GetShoppingCart().Items
            Dim listOfItemsWithoutDeals = New List(Of SubCartItem)
            For Each item As BxCartItem In itemList
                Dim subItem As SubCartItem = item.Items(0)
                subItem.Quantity = item.Quantity
                subItem.UnitPrice = item.TotalPrice
                listOfItemsWithoutDeals.Add(subItem)
            Next
            rptCartItems.DataSource = listOfItemsWithoutDeals
            rptCartItems.DataBind()

        Catch ex As Exception
            _log.[Error](ex)
            Response.Write("Error")
        End Try

        _log.Info("END")
    End Sub

    Protected Sub rptCartItems_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        _log.Info("BEGIN")

        Try
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                'Dim cartItem = TryCast(e.Item.DataItem, BxCartItem)
                Dim cartItem = TryCast(e.Item.DataItem, SubCartItem)
                If cartItem Is Nothing Then
                    Return
                End If

                Dim subItemRepeater = DirectCast(e.Item.FindControl("rptSubCartItems"), Repeater)
                If subItemRepeater Is Nothing Then
                    Return
                End If

                subItemRepeater.DataSource = cartItem.Items
                subItemRepeater.DataBind()
            End If

            ' If we are binding the footer row, let's add in our total
            If e.Item.ItemType = ListItemType.Footer Then
                Dim lblPostCodePrice = TryCast(e.Item.FindControl("lblPostCodePrice"), Literal)
                Dim lblTotalPrice = TryCast(e.Item.FindControl("lblTotalPrice"), Literal)

                Dim lblSubTotalPrice = TryCast(e.Item.FindControl("lblSubTotal"), Literal)
                Dim lblOnlineDiscount = TryCast(e.Item.FindControl("lblOnlineDiscount"), Literal)

                If BxShoppingCart.GetShoppingCart().DiscountInPercent > 0 Then
                    Dim placeHolder = TryCast(e.Item.FindControl("OnlineDiscountPlaceHolder"), PlaceHolder)
                    If placeHolder IsNot Nothing Then
                        placeHolder.Visible = True
                    End If
                    Dim onlineDiscountLabel = TryCast(e.Item.FindControl("OnlineDiscountLabel"), Literal)
                    If onlineDiscountLabel IsNot Nothing Then
                        onlineDiscountLabel.Text = Convert.ToString(onlineDiscountLabel.Text) & " (" & BxShoppingCart.GetShoppingCart().DiscountInPercent.ToString("N2") & " %)"
                    End If
                End If

                Dim totalPrice As Decimal = 0
                If lblPostCodePrice IsNot Nothing Then
                    If HttpContext.Current.Session(BxShoppingCart.POST_CODE_CHARGE) IsNot Nothing Then
                        lblPostCodePrice.Text = "£" & Convert.ToDecimal(HttpContext.Current.Session(BxShoppingCart.POST_CODE_CHARGE)).ToString("N2")
                        totalPrice = totalPrice + Convert.ToDecimal(HttpContext.Current.Session(BxShoppingCart.POST_CODE_CHARGE))
                    End If
                End If

                If lblTotalPrice IsNot Nothing AndAlso lblSubTotalPrice IsNot Nothing Then
                    totalPrice = totalPrice + BxShoppingCart.GetShoppingCart().GetSubTotal()

                    If lblOnlineDiscount IsNot Nothing Then
                        lblOnlineDiscount.Text = "- £" & ((BxShoppingCart.GetShoppingCart().DiscountInPercent * BxShoppingCart.GetShoppingCart().GetSubTotalExclCategoriesDonotHaveOnlineDiscount()) / 100).ToString("N2")
                    End If
                    totalPrice = totalPrice - ((BxShoppingCart.GetShoppingCart().DiscountInPercent * BxShoppingCart.GetShoppingCart().GetSubTotalExclCategoriesDonotHaveOnlineDiscount()) / 100)

                    lblSubTotalPrice.Text = "£" & BxShoppingCart.GetShoppingCart().GetSubTotal().ToString("N2")

                    lblTotalPrice.Text = "£" & totalPrice.ToString("N2")
                End If
            End If
        Catch ex As Exception
            _log.Error(ex)
            Response.Write("Error")
        End Try

        _log.Info("END")
    End Sub
End Class