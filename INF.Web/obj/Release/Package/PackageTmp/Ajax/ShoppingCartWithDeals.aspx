<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ShoppingCartWithDeals.aspx.vb" Inherits="INF.Web.Ajax.ShoppingCartWithDeals" %>
<%@ Import Namespace="INF.Web.UI.Settings" %>
<%@ Import Namespace="INF.Web.UI.Shopping" %>



<script type="text/javascript">
  $(document).ready(function()
    {
        $('.shopping-cart-products').find('.nmPrd').each(function()
        {
            removeExtraLabels($(this));
            //console.log('Prepare to Remove extra label');
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
                                    <td class="nmPrd"><%# Eval("tempName2")%></td>
                                    <td class="ttlPrice"><%# FormatCurrency(Eval("TotalPrice"),2)%></td>
                                    <td class="delPrd">
                                       <a href="<%# "javascript:IncreaseQuantityOfItemWithDeals('" + Eval("CartID") + "');"%>">                                            
                                            <i class="fa fa-plus-square"></i>
                                        </a>

                                        <a href="<%# "javascript:DecreaseQuantityOfItemWithDeals('" + Eval("CartID") + "');"%>">
                                            <i class="fa fa-minus-square"></i>
                                        </a>
                                    </td>                                   
                                </tr>
                                
                                <asp:Repeater runat="server" ID="rptSubCartItems">

                                    <ItemTemplate>

                                    <tr class="<%# Eval("isWhatSI")%>">
                                        
                                        <td></td>
                                        <td colspan="2" class="dealThreeCols"><%# Eval("Name")%></td>
                                       
                                         <td colspan="1" class="dealOneCols">
                                            <%# IIf(CDec(Eval("UnitPrice")) = 0, "", FormatNumber(Eval("UnitPrice"), 2))%>
                                        </td>
                                        
                                    </tr>

                                    <asp:Repeater runat="server" ID="rptGrandSubCartItems">

                                        <ItemTemplate>
                                            <tr class="dealRowItemDetail <%# Eval("isWhatSI")%> ">    
                                                <td></td>                                            
                                                <td colspan="2" class="dealGrandName"><%#Eval("Name")%></td>
                                                <td class="dealGrandPrice">
                                                    
                                                        <%# IIf(CDec(Eval("UnitPrice")) = 0, "", FormatCurrency(Eval("UnitPrice"), 2))%>
                                                    
                                                </td>                                              
                                            </tr>
                                        </ItemTemplate>

                                    </asp:Repeater>
                                </ItemTemplate>

                                </asp:Repeater>
                        </ItemTemplate>

                        <FooterTemplate>
                           <tr class="darkBckg">
                                    <td colspan="2" class="total-price-caption">Subtotal Price</td>
                                    <td colspan="1" class="total-price-value"><asp:Literal runat="server" ID="lblSubTotal" Text=""></asp:Literal> </td>
                                    <td style="border-left:1px solid transparent"></td>
                                </tr>

                                <asp:PlaceHolder runat="server" ID="OnlineDiscountPlaceHolder" Visible="False">
                                    <tr class="darkBckg" >
                                        <td colspan="2" class="postcode-price-caption">
                                           <asp:Literal runat="server" ID="OnlineDiscountLabel" Text="Online Discount"></asp:Literal>
                                        </td>
                                        <td colspan="1" class="postcode-price-value">
                                            <span><asp:Literal runat="server" ID="lblOnlineDiscount" Text=""></asp:Literal></span>
                                        </td>
                                        <td style="border-left:1px solid transparent"></td>
                                    </tr>
                                </asp:PlaceHolder>

                                <% If (Not IsNothing(Session(BxShoppingCart.SS_ORDER_TYPE)) AndAlso Session(BxShoppingCart.SS_ORDER_TYPE).ToString() = BxShoppingCart.ORDER_TYPE_DELIVERY) Then%>
                                <tr class="darkBckg">
                                    <td colspan="2" class="postcode-price-caption">Delivery Charge</td>
                                    <td colspan="1" class="postcode-price-value"><asp:Literal runat="server" ID="lblPostCodePrice" Text=""></asp:Literal> </td>
                                    <td style="border-left:1px solid transparent"></td>
                                </tr>       
                                <%End If%>
                                <tr class="darkBckg trfinPrice">
                                    <td colspan="2" class="total-price-caption" >Total Price</td>
                                    <td colspan="1" class="total-price-value" ><asp:Literal runat="server" ID="lblTotalPrice" Text=""></asp:Literal> </td>
                                    <td style="border-left:1px solid transparent"></td>
                                </tr>
                                
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:View>
    </asp:multiview>
</div>