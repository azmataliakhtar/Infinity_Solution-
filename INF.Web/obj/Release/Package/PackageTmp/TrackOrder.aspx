<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false" Inherits="INF.Web.TrackOrder" CodeBehind="TrackOrder.aspx.vb" %>

<%@ Import Namespace="INF.Web.UI.Settings" %>


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
        If (ConfigurationManager.AppSettings("linkCanonicalTracking") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("linkCanonicalTracking")))) Then
        %>                                
            <link rel="canonical" href="<%=ConfigurationManager.AppSettings("linkCanonicalTracking")%>" />
        <%
        End If
    %>   


     <%
         If (ConfigurationManager.AppSettings("descTracking") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("descTracking")))) Then
        %>                                
            <meta name="description" content="<%=ConfigurationManager.AppSettings("descTracking")%>" />
        <%
        End If
    %>   

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="Server">
   
     <%
         If (ConfigurationManager.AppSettings("titleTracking") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titleTracking")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titleTracking")%>
        <%
        Else
            %> 
                <%= EPATheme.Current.Themes.WebsiteName%> - Track Your Orders 
            <%
        End If
    %>   

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadExtra" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="cstmsection">
       
        <h3 class="page-header page-title"><i class="fa fa-truck"></i>Track Your Orders</h3>
            
       
            <p class="subheaderInfo">Here are your orders made on our website.</p>

            <asp:Repeater runat="server" ID="OrdersRepeater">
                <ItemTemplate>
                    <div class="trackOrderItem">

                        <div class="track-order-header">
                            <div class="ordTitl">
                                 <%# Eval("OrderDate")%>
                            </div>
                            
                            <div class="ordType">
                                <span class="left">Order Type:</span>
                                <span class="right"><%#Eval("OrderType")%></span>
                            </div>

                            <div class="ordStatus">
                                <span class="left">Status:</span>
                                <span class="right"><%# Eval("OrderStatus")%></span>
                            </div>
                        </div>

                        <div class="ordTimes" style="<%# IIf(Eval("OrderType") = "DELIVERY" , "display: block;", "display: none;")%>">
                            <ul>
                                
                                <li>Delivery Time: <%# IIf(Eval("ProcessingTime") > 0 , Eval("ProcessingTime"), Eval("ExpectedTime")) %></li>

                            </ul>
                        </div>

                        <div class="track-order-detail">

                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <th>qty</th>
                                    <th style="text-align:left;padding-left:10px;">item</th>
                                    <th>price (£)</th>
                                </tr>
                                <asp:Repeater runat="server" ID="OrderDetailsRepeater">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="width: 40px; text-align: center;"><%#Eval("Quantity")%></td>
                                            <td style="padding-left:10px;"><%#Eval("MenuItemName")%></td>
                                            <td style="width: 60px;"><%#Eval("Price")%></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <tr class="footerTrackTr">
                                    <td style="" colspan="2">Sub Total</td>
                                    <td style="">
                                        <asp:Literal runat="server" ID="OrderSubTotalPrice"></asp:Literal></td>
                                </tr>
                                <tr class="footerTrackTr">
                                    <td colspan="2">Online Discount</td>
                                    <td ><%#FormatNumber(Eval("Discount"), 2)%></td>
                                </tr>
                                <tr class="footerTrackTr">
                                    <td colspan="2">Delivery Charges</td>
                                    <td style=""><%# FormatNumber(Eval("DeliveryCharges"), 2)%></td>
                                </tr>
                                <tr class="footerTrackTr totaltrprice">
                                    <td style="" colspan="2">Total Price</td>
                                    <td style=""><%#FormatNumber(Eval("TotalAmount"), 2)%></td>
                                </tr>
                            </table>

                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
      
    </section>
</asp:Content>

