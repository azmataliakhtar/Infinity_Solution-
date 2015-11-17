Imports INF.Web.Data.BLL
Imports INF.Web.UI.Shopping
Imports log4net
Imports INF.Web.UI.Logging.Log4Net

Namespace Ajax
    Public Class SmallShoppingCart
        Inherits System.Web.UI.Page

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected ReadOnly Property IsDeliveryOrder() As Boolean
            Get
                Dim blnDeliveryOrder As Boolean = (Session(BxShoppingCart.SS_ORDER_TYPE) IsNot Nothing) AndAlso Convert.ToString(Session(BxShoppingCart.SS_ORDER_TYPE)) = BxShoppingCart.ORDER_TYPE_DELIVERY
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

        Private Sub SmallShoppingCart_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            _log.Info("BEGIN")
            Try
                If BxShoppingCart.GetShoppingCart().Items.Count = 0 Then
                    mvShoppingCart.ActiveViewIndex = 0
                Else
                    BindData()
                    phMessageBox.Controls.Clear()
                    If Not String.IsNullOrWhiteSpace(BxShoppingCart.GetShoppingCart().Message) Then
                        Dim div = New HtmlGenericControl("div")
                        Dim lblMessage = New Label() With {
                            .Text = BxShoppingCart.GetShoppingCart().Message
                        }

                        'Dim img = New Image() With {
                        '    .Width = 20,
                        '.ImageUrl = If(BxShoppingCart.GetShoppingCart().IsErrorMessage, "/Images/info_red_20.png", "/Images/info_blue_20.png")
                        '}

                        div.Attributes.Add("class", If(BxShoppingCart.GetShoppingCart().IsErrorMessage, "box-error-message", "box-info-message"))

                        'div.Controls.Add(img)
                        div.Controls.Add(lblMessage)
                        phMessageBox.Controls.Add(div)
                        phMessageBox.Visible = True
                    Else
                        phMessageBox.Visible = False
                    End If

                    mvShoppingCart.ActiveViewIndex = 1
                End If

                If Request.UrlReferrer IsNot Nothing Then
                    phCheckOutButton.Visible = Not Request.UrlReferrer.ToString().ToLower().Contains("orderreview.aspx")
                End If
            Catch ex As Exception
                _log.Error(ex)
                Response.Write("Error")
            End Try
            _log.Info("END")
        End Sub

        Private Sub BindData()
            _log.Info("BEGIN")
            Try
                Dim resBll = New RestaurantBusinessLogic()

                Dim onlineDis = resBll.GetOnlineDiscount(IsDeliveryOrder, CInt(DateTime.Today.DayOfWeek))

                BxShoppingCart.GetShoppingCart().IsDeliveryOrder = IsDeliveryOrder
                BxShoppingCart.GetShoppingCart().CollectionSpecialDiscountOrderValue = resBll.GetSpecialDiscountOrderValue()
                BxShoppingCart.GetShoppingCart().CollectionSpecialDiscount = resBll.GetSpecialDiscount()
                BxShoppingCart.GetShoppingCart().DeliverySpecialDiscountOrderValue = resBll.GetDeliverySpecialDiscountOrderValue()
                BxShoppingCart.GetShoppingCart().DeliverySpecialDiscount = resBll.GetDeliverySpecialDiscount()

                BxShoppingCart.GetShoppingCart().DiscountInPercent = CDec(onlineDis)
                OnlineDiscount = CDec(onlineDis)

                ' Let's give the data to the GridView and let it work!
                ' The GridView will take our cart items one by one and use the properties
                ' that we declared as column names (DataFields)
                Dim itemList As List(Of BxCartItem) = BxShoppingCart.GetShoppingCart().Items
                Dim tempItemList = New List(Of BxCartItem)

                For Each item As BxCartItem In itemList
                    Dim subItem As SubCartItem = item.Items(0)
                    ' subItem.Quantity = item.Quantity
                    ' subItem.UnitPrice = item.TotalPrice
                    ' subItem.DealID = item.DealID
                    'subItem.CartID = item.CartId

                    'item.Description = subItem.Name



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
                rptCartItems.DataBind()

            Catch ex As Exception
                _log.Error(ex)
                Response.Write("Error")
            End Try
            _log.Info("END")
        End Sub

        Protected Property CurrentCartItem() As BxCartItem
            Get
                Return TryCast(ViewState("CurrentBxCartItem"), BxCartItem)
            End Get
            Set(value As BxCartItem)
                ViewState("CurrentCartItem") = value
            End Set
        End Property

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

                    CurrentCartItem = cartItem
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

                    Dim totalPrice__1 As Decimal = 0
                    If lblPostCodePrice IsNot Nothing Then
                        If HttpContext.Current.Session(BxShoppingCart.POST_CODE_CHARGE) IsNot Nothing Then
                            lblPostCodePrice.Text = "£" & Convert.ToDecimal(HttpContext.Current.Session(BxShoppingCart.POST_CODE_CHARGE)).ToString("N2")
                            totalPrice__1 = totalPrice__1 + Convert.ToDecimal(HttpContext.Current.Session(BxShoppingCart.POST_CODE_CHARGE))
                        End If
                    End If

                    If lblTotalPrice IsNot Nothing AndAlso lblSubTotalPrice IsNot Nothing Then
                        totalPrice__1 = totalPrice__1 + BxShoppingCart.GetShoppingCart().GetSubTotal()

                        If lblOnlineDiscount IsNot Nothing Then
                            lblOnlineDiscount.Text = "- £" & ((BxShoppingCart.GetShoppingCart().DiscountInPercent * BxShoppingCart.GetShoppingCart().GetSubTotalExclCategoriesDonotHaveOnlineDiscount()) / 100).ToString("N2")
                        End If
                        totalPrice__1 = totalPrice__1 - ((BxShoppingCart.GetShoppingCart().DiscountInPercent * BxShoppingCart.GetShoppingCart().GetSubTotalExclCategoriesDonotHaveOnlineDiscount()) / 100)

                        lblSubTotalPrice.Text = "£" & BxShoppingCart.GetShoppingCart().GetSubTotal().ToString("N2")

                        lblTotalPrice.Text = "£" & totalPrice__1.ToString("N2")
                        TotalPrice.Text = totalPrice__1.ToString("N2")
                    End If
                End If
            Catch ex As Exception
                _log.Error(ex)
                Response.Write("Error")
            End Try
            _log.Info("END")
        End Sub

        Private Sub SubItemRepeater_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
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

                    Dim link = TryCast(e.Item.FindControl("LinkToRemoveSubItem"), HyperLink)
                    If link Is Nothing Then
                        Return
                    End If
                    If CurrentCartItem Is Nothing Then
                        Return
                    End If

                    'Dim href = "javascript:RemoveOptionsFromBasket('" & Convert.ToString(CurrentCartItem.MenuItemID) & "','" & Convert.ToString(CurrentCartItem.SubMenuItemID) & "','" & Convert.ToString(subItem.ID) & "','" & Convert.ToInt32(subItem.ItemType) & "')"
                    'link.Attributes.Add("href", href)

                End If
            Catch ex As Exception
                _log.Error(ex)
                Response.Write("Error")
            End Try
            _log.Info("END")
        End Sub
    End Class
End Namespace