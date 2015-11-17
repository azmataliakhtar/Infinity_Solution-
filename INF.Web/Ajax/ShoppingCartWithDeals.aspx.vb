Imports INF.Web.Data.BLL
Imports log4net
Imports INF.Web.UI.Shopping
Imports INF.Web.UI.Logging.Log4Net

Namespace Ajax
    Public Class ShoppingCartWithDeals
        Inherits System.Web.UI.Page

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

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
            If BxShoppingCart.GetShoppingCart().Items.Count = 0 Then
                mvShoppingCart.ActiveViewIndex = 0
            Else
                BindData()
                mvShoppingCart.ActiveViewIndex = 1
            End If
            _log.Info("END")
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
                Dim tempItemList = New List(Of BxCartItem)

                For Each item As BxCartItem In itemList
                    Dim subItem As SubCartItem = item.Items(0)

                    If (item.DealID < 1) Then
                        item.tempName2 = subItem.Name
                        item.isWhat = "isItem"
                    Else
                        item.tempName2 = item.Description
                        item.isWhat = "isDeal"
                    End If

                    tempItemList.Add(item)

                Next

                'rptCartItems.DataSource = BxShoppingCart.GetShoppingCart().Items
                rptCartItems.DataSource = tempItemList


                ' rptCartItems.DataSource = BxShoppingCart.GetShoppingCart().Items
                rptCartItems.DataBind()

                'Logging
                '_log.Debug("CollectionSpecialDiscountOrderValue = " + BxShoppingCart.GetShoppingCart().CollectionSpecialDiscountOrderValue)
                '_log.Debug("CollectionSpecialDiscount = " + BxShoppingCart.GetShoppingCart().CollectionSpecialDiscount)
                '_log.Debug("DeliverySpecialDiscountOrderValue = " + BxShoppingCart.GetShoppingCart().DeliverySpecialDiscountOrderValue)
                '_log.Debug("DeliverySpecialDiscount = " + BxShoppingCart.GetShoppingCart().DeliverySpecialDiscount)
                '_log.Debug("OnlineDiscount = " + BxShoppingCart.GetShoppingCart().DiscountInPercent)
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
                    Dim cartItem = TryCast(e.Item.DataItem, BxCartItem)
                    If cartItem Is Nothing Then
                        Return
                    End If

                    Dim subItemRepeater = DirectCast(e.Item.FindControl("rptSubCartItems"), Repeater)
                    If subItemRepeater Is Nothing Then
                        Return
                    End If

                    AddHandler subItemRepeater.ItemDataBound, AddressOf SubItemRepeater_ItemDataBound
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

        Private Sub SubItemRepeater_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
            _log.Info("BEGIN")
            Try
                If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

                    Dim subItem = TryCast(e.Item.DataItem, SubCartItem)
                    If subItem Is Nothing Then
                        Return
                    End If

                    Dim rptGrandSubCartItems As Repeater = DirectCast(e.Item.FindControl("rptGrandSubCartItems"), Repeater)
                    If Not IsNothing(rptGrandSubCartItems) Then
                        rptGrandSubCartItems.DataSource = subItem.Items
                        rptGrandSubCartItems.DataBind()
                    End If
                End If
            Catch ex As Exception
                _log.Error(ex)
                Response.Write("Error")
            End Try
            _log.Info("END")
        End Sub
    End Class
End Namespace