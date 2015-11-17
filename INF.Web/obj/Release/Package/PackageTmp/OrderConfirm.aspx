<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.master" CodeBehind="OrderConfirm.aspx.vb" Inherits="INF.Web.OrderConfirm" %>

<%@ Import Namespace="INF.Web.UI.Settings" %>
<%@ Import Namespace="INF.Web.UI.SagePay" %>
<%@ Import Namespace="SagePay.IntegrationKit" %>


<asp:Content ID="GoogleContent" ContentPlaceHolderID="GooglePlaceHolder" runat="Server">
        
      <%
          If (ConfigurationManager.AppSettings("google-site-verification") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("google-plus-link")))) Then
        %>                                
             <meta name="google-site-verification" content="<%=ConfigurationManager.AppSettings("google-site-verification")%>"/>
        <%
        End If
    %>    
    
     <%
         If (ConfigurationManager.AppSettings("google-plus-link") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("google-plus-link")))) Then
        %>                                
             <link href="<%=ConfigurationManager.AppSettings("google-plus-link")%>" rel="publisher"/>
        <%
        End If
    %>   
   
     <%
         If (ConfigurationManager.AppSettings("geo-region") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("geo-region")))) Then
        %>                                
             <meta name="geo.region" content="<%=ConfigurationManager.AppSettings("geo-region")%>" />
        <%
        End If
    %> 

    <%
        If (ConfigurationManager.AppSettings("geo-placename") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("geo-placename")))) Then
        %>                                
             <meta name="geo.placename" content="<%=ConfigurationManager.AppSettings("geo-placename")%>" />
        <%
        End If
    %> 


     <%
         If (ConfigurationManager.AppSettings("geo-position") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("geo-position")))) Then
        %>                                
             <meta name="geo.position" content="<%=ConfigurationManager.AppSettings("geo-position")%>" />
        <%
        End If
    %> 

     <%
         If (ConfigurationManager.AppSettings("google-icbm") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("google-icbm")))) Then
        %>                                
             <meta name="ICBM" content="<%=ConfigurationManager.AppSettings("google-icbm")%>" />
        <%
        End If
    %> 

</asp:Content>

<asp:Content runat="server" ID="LinkCanonicalContent" ContentPlaceHolderID="LinkCanonical">

    <%
        If (ConfigurationManager.AppSettings("linkCanonicalOrderConfirm") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("linkCanonicalOrderConfirm")))) Then
        %>                                
            <link rel="canonical" href="<%=ConfigurationManager.AppSettings("linkCanonicalOrderConfirm")%>" />
        <%
        End If
    %>   


</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
     <%
         If (ConfigurationManager.AppSettings("titleOrderConfirmation") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titleOrderConfirmation")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titleOrderConfirmation")%>
        <%
        Else
            %> 
                <%= EPATheme.Current.Themes.WebsiteName%> - Order Confirm  
            <%
        End If
    %>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadExtra" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <section class="cstmsection orderconfirm">
        
            <h3>Your Order Details</h3>
      
        <div class="shopping-cart-products">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <asp:Repeater runat="server" ID="rptCartItems" OnItemDataBound="rptCartItems_ItemDataBound">
                    <HeaderTemplate>
                        <tr>
                            <th class="shopping-cart-products-header">Qty
                            </th>
                            <th class="shopping-cart-products-header">Item
                            </th>
                            <th class="shopping-cart-products-header">Price
                            </th>
                            <%--<th class="shopping-cart-products-header">&nbsp;
                            </th>--%>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="width: 25px; text-align: center; font-weight: bold;">
                                <%# Eval("Quantity") %>
                            </td>
                            <td style="text-align: left;">
                                <%# Eval("Name")%>
                            </td>
                            <td style="width: 40px; text-align: right; padding-right: 5px;">
                                <%# Eval("UnitPrice")%>
                            </td>
                            <%--<td style="width: 68px; padding: 0; text-align: center;">
                                <a href="<%# "javascript:IncreaseQuantityOfItem('" + Eval("CartID") + "');"%>">
                                    <img alt="" src="/images/plus_24.png" style="margin-top: 2px; border-width: 0px; text-decoration: none;" /></a>&nbsp;
                                                    <a href="<%# "javascript:DecreaseQuantityOfItem('" + Eval("CartID") + "');"%>">
                                                        <img alt="" src="/images/minus_24.png" style="margin-top: -2px; border-width: 0px; text-decoration: none;" /></a>
                            </td>--%>
                        </tr>
                        <asp:Repeater runat="server" ID="rptSubCartItems">
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 25px; text-align: center;">&nbsp;
                                    </td>
                                    <td style="text-align: left; padding-left: 10px;">
                                        <%# "→ " + Eval("Name")%>
                                    </td>
                                    <td style="width: 40px; text-align: right;">
                                        <%# IIf(CDec(Eval("UnitPrice")) = 0, "", FormatNumber(Eval("UnitPrice"), 2))%>
                                    </td>
                                    <%--<td style="width: 68px;">&nbsp;
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr style="background-color: #ffffff;">
                            <td colspan="2" class="total-price-caption">Sub Total Price
                            </td>
                            <td colspan="1" class="total-price-value">
                                <asp:Literal runat="server" ID="lblSubTotal" Text=""></asp:Literal>
                            </td>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="OnlineDiscountPlaceHolder" Visible="False">
                            <tr style="background-color: #ffffff;">
                                <td colspan="2" class="postcode-price-caption">
                                    <span style="color: red;">
                                        <asp:Literal runat="server" ID="OnlineDiscountLabel" Text="Online Discount"></asp:Literal></span>
                                </td>
                                <td colspan="1" class="postcode-price-value">
                                    <span style="color: red;">
                                        <asp:Literal runat="server" ID="lblOnlineDiscount" Text=""></asp:Literal></span>
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <tr style="background-color: #ffffff;">
                            <td colspan="2" class="postcode-price-caption">Delivery Charge
                            </td>
                            <td colspan="1" class="postcode-price-value">
                                <asp:Literal runat="server" ID="lblPostCodePrice" Text=""></asp:Literal>
                            </td>
                        </tr>
                        <tr style="background-color: #ffffff;">
                            <td colspan="2" class="total-price-caption" style="font-weight: bold; color: red;">Total Price
                            </td>
                            <td colspan="1" class="total-price-value" style="font-weight: bold; color: red;">
                                <asp:Literal runat="server" ID="lblTotalPrice" Text=""></asp:Literal>
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </section>
    <section class="orderconfirmSecond">
        <%-- <section class="col-md-12 col-xs-12">--%>
        <div class="yourBillingAddr">
        <div>
            <h3>Your Billing Details</h3>
        </div>
        <div>
            <div class="row form-group">
                <div class="col-md-3" style="padding-left: 25px;">
                    Your Name:
                </div>
                <div class="col-md-9">
                    <asp:Literal runat="server" ID="ltrBillingName"></asp:Literal>
                </div>
            </div>
            <div class="row form-group">
                <div class="col-md-3" style="padding-left: 25px;">
                    Address:
                </div>
                <div class="col-md-9">
                    <asp:Literal runat="server" ID="ltrBillingAddress"></asp:Literal>
                </div>
            </div>
            <div class="row form-group">
                <div class="col-md-3" style="padding-left: 25px;">
                    Phone Number:
                </div>
                <div class="col-md-9">
                    <asp:Literal runat="server" ID="ltrBillingPhoneNo"></asp:Literal>
                </div>
            </div>
            <div class="row form-group">
                <div class="col-md-3" style="padding-left: 25px;">
                    E-Mail:
                </div>
                <div class="col-md-9">
                    <asp:Literal runat="server" ID="ltrBillingEmail"></asp:Literal>
                </div>
            </div>
        </div>
        </div>
        <%--</section>--%>
        <%--<section class="col-md-12 col-xs-12">--%>
        <div class="yourDeliveryAddr">
         <div>
            <h3>Your Delivery Details</h3>
        </div>
        <div>
            <div class="row form-group">
                <div class="col-md-3" style="padding-left: 25px;">
                    Your Name:
                </div>
                <div class="col-md-9">
                    

                    <!-- <asp:TextBox ID="billingNamePlaceID" runat="server" CssClass="form-control login-input"  placeholder="billing name"></asp:TextBox> -->
                    
                    <asp:Literal runat="server" ID="ltrDeliveryName"></asp:Literal>
                </div>
            </div>
            <div class="row form-group">
                <div class="col-md-3" style="padding-left: 25px;">
                    Address:
                </div>
                <div class="col-md-9">
                    <asp:Literal runat="server" ID="ltrDeliveryAddress"></asp:Literal>
                </div>
            </div>
            <div class="row form-group">
                <div class="col-md-3" style="padding-left: 25px;">
                    Phone Number:
                </div>
                <div class="col-md-9">
                    <asp:Literal runat="server" ID="ltrDeliveryPhoneNo"></asp:Literal>
                </div>
            </div>
            <div class="row form-group">
                <div class="col-md-3" style="padding-left: 25px;">
                    E-Mail:
                </div>
                <div class="col-md-9">
                    <asp:Literal runat="server" ID="ltrDeliveryEmail"></asp:Literal>
                </div>
            </div>
        </div>
        </div>
        <%-- </section>--%>
        

        <div class="yourDeliveryTiming">
            <h3>Delivery Timing: <asp:Literal runat="server" ID="ltrDeliverTiming"></asp:Literal></h3>
        </div>
      
    </section>
    <section class="orderconfirmThird">
        <div class="text-center">
            <%--<asp:Button runat="server" ID="btnBack" Text="Back" class="btn btn-sm btn-default" Style="width: 110px;" />--%>
            <a href="OrderReview.aspx" class="btn goBackBtn">Back</a>
            <%--<input type="submit" name="processPayment" value="Proceed" class="btn btn-sm btn-danger" style="width: 110px;" />--%>
            <!-- ************************************************************************************* -->
            <!-- This form is all that is required to submit the payment information to the system -->
            <asp:PlaceHolder ID="phSubmitPayment" runat="server">
                <%--<form action="<%= SagePaySettings.FormPaymentUrl %>" method="post" id="SagePayForm"
                    name="SagePayForm">--%>
                <input type="hidden" name="VPSProtocol" value="<%= SagePaySettings.ProtocolVersion.VersionString() %>" />
                <input type="hidden" name="TxType" value="<%= SagePaySettings.DefaultTransactionType.ToString()%>" />
                <input type="hidden" name="Vendor" value="<%= SagePaySettings.VendorName %>" />
                <input type="hidden" name="Crypt" value="<%= Crypt %>" />
                <input type="submit" name="processPayment" value="Proceed to Payment" class="btn prcToPaymentBtn"  />
                
                <%--</form>--%>
            </asp:PlaceHolder>
            <!-- ************************************************************************************* -->
        </div>
    </section>
</asp:Content>
