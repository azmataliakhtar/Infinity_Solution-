<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SmallShoppingCartWithoutDeals.aspx.vb" Inherits="INF.Web.Ajax.SmallShoppingCartWithoutDeals" %>
<%@ Import Namespace="INF.Web.UI.Settings" %>
<%@ Import Namespace="INF.Web.UI.Shopping" %>

<script type="text/javascript">
    setTimeout(function () {
        $('.box-info-message').slideUp();
    }, 3000);

     $(document).ready(function()
    {
        $('.shopping-cart-products').find('.nmPrd').each(function()
        {
            removeExtraLabels($(this));
            console.log('Prepare to Remove extra label');
        });
    });


</script>

<div class="shopping-cart-wrapper">

    <div class="shopping-cart-header">
        <asp:label runat="server" id="lblShopCartHeader" text="Your Basket" cssclass="shopping-cart-header-title"></asp:label>
        <i class="fa fa-shopping-cart"></i>        
    </div>

    <asp:multiview runat="server" id="mvShoppingCart">

        <asp:View runat="server" ID="vEmptyCart">
            <div class="shopping-cart-non-products">
               There are no products in your Basket!
            </div>
        </asp:View>

            <asp:View runat="server" ID="vNonEmptyCart">

                <div class="shopping-cart-products">

                    <asp:PlaceHolder runat="server" ID="phMessageBox" Visible="False"></asp:PlaceHolder>
                    
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        
                        <asp:Repeater runat="server" ID="rptCartItems" OnItemDataBound="rptCartItems_ItemDataBound">
                            
                            <HeaderTemplate>
                                <tr>
                                    <th class="shopping-cart-products-header">qty</th>
                                    <th class="shopping-cart-products-header">item</th>
                                    <th class="shopping-cart-products-header">price</th>
                                    <th class="shopping-cart-products-header">&nbsp;</th>
                                </tr>
                            </HeaderTemplate>

                            <ItemTemplate>

                                <tr>
                                    <td class="qtyPrd"><%# Eval("Quantity") %></td>
                                    <td class="nmPrd"><%# Eval("Name")%></td>
                                    <td class="ttlPrice"><%# IIf(CDec(Eval("UnitPrice")) = 0, "", FormatCurrency(Eval("UnitPrice"), 2))%></td>
                                    <td class="delPrd">
                                        <p>CART ID: <%#Eval("CartID")%> <br/> DEAL ID <%#Eval("DealID")%></p>
                                        <a href="<%# "javascript:RemoveMenuItemFromShopCart('" + Eval("CartID") + "');"%>">
                                            <i class="fa fa-times"></i>
                                        </a>
                                    </td>
                                </tr>

                                <asp:Repeater runat="server" ID="rptSubCartItems">
                                    <ItemTemplate>

                                        <tr>
                                            <td class="sbprdID">&nbsp;</td>
                                            <td class="sbprdName"><%#Eval("Name")%></td>
                                            <td class="sbprdPrice">
                                                
                                                  <%# IIf(CDec(Eval("UnitPrice")) = 0, "", FormatCurrency(Eval("UnitPrice"), 2))%>
                                                
                                            </td>
                                            <td class="sbrpdnbsp">&nbsp;</td>
                                        </tr>

                                    </ItemTemplate>
                                </asp:Repeater>

                            </ItemTemplate>

                            <FooterTemplate>

                                <tr class="darkBckg">
                                    <td colspan="2" class="total-price-caption">Subtotal Price</td>
                                    <td colspan="2" class="total-price-value"><asp:Literal runat="server" ID="lblSubTotal" Text=""></asp:Literal> </td>
                                </tr>

                              
                                 <asp:PlaceHolder runat="server" ID="OnlineDiscountPlaceHolder" Visible="False">
                                    <tr class="darkBckg" >
                                        <td colspan="2" class="postcode-price-caption">
                                           <asp:Literal runat="server" ID="OnlineDiscountLabel" Text="Online Discount"></asp:Literal>
                                        </td>
                                        <td colspan="2" class="postcode-price-value">
                                            <span><asp:Literal runat="server" ID="lblOnlineDiscount" Text=""></asp:Literal></span>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>

                                <% If (Not IsNothing(Session(BxShoppingCart.SS_ORDER_TYPE)) AndAlso Session(BxShoppingCart.SS_ORDER_TYPE).ToString() = BxShoppingCart.ORDER_TYPE_DELIVERY) Then%>
                                <tr class="darkBckg">
                                    <td colspan="2" class="postcode-price-caption">Delivery Charge</td>
                                    <td colspan="2" class="postcode-price-value"><asp:Literal runat="server" ID="lblPostCodePrice" Text=""></asp:Literal> </td>
                                </tr>       
                                <%End If%>

                                <tr class="darkBckg trfinPrice">
                                    <td colspan="2" class="total-price-caption" >Total Price</td>
                                    <td colspan="2" class="total-price-value" ><asp:Literal runat="server" ID="lblTotalPrice" Text=""></asp:Literal> </td>
                                </tr>

                                 <tr class="additInfo">
                                    <td colspan="4">
                                        <label class="additInfoLabel">Additional Instructions</label>
                                        <br/>
                                        <textarea id="txtAdditionalInstruction" style="width: 95%; height: 60px;" rows="3" class="form-control"></textarea>
                                    </td>
                                </tr>

                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                
              <asp:PlaceHolder runat="server" ID="phCheckOutButton">

              <asp:Literal runat="server" ID="TotalPrice" Text="" Visible="False"></asp:Literal>
                  <div id="DivCheckOut" style="text-align: center;">
                      <% If (Not IsNothing(Session(BxShoppingCart.SS_ORDER_TYPE)) AndAlso Session(BxShoppingCart.SS_ORDER_TYPE).ToString() = BxShoppingCart.ORDER_TYPE_DELIVERY) Then%>
                        <a href='javascript:Refresh_MinOrderPrice(<%= TotalPrice.Text %>)' class="aImgBtn">
                                     <i class="fa fa-shopping-cart"></i> Go To Checkout
                        </a>
                        <% Else%>
                             <a href='javascript:Refresh_WithoutMinOrderPrice()' class="aImgBtn">
                                <i class="fa fa-shopping-cart"></i> Go To Checkout
                        </a>
                    <% End If%>

                  </div>
              </asp:PlaceHolder>
            </asp:View>
    </asp:multiview>
</div>
