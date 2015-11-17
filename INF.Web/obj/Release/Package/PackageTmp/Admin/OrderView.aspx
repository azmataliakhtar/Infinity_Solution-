<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/DashboardMaster.master" CodeBehind="OrderView.aspx.vb" Inherits="INF.Web.OrderView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style type="text/css">
        .main-content-wrapper {
            display: block;
            height: auto !important;
        }

        .content-header {
            font-family: "Segoe UI","Segoe","Segoe WP","Arial","sans-serif";
            font-size: 18px;
            color: #349BDC;
            text-align: left;
        }

            .content-header h3 {
                margin-top: 5px;
                margin-bottom: 5px;
            }

        .content-body {
            margin: auto 10px;
        }

        .track-order-header {
            /*background-color: #800000;*/
            color: #349BDC;
            text-align: left;
            padding-left: 5px;
            /*border-bottom: 1px solid #800000;*/
        }

            .track-order-header h3 {
                margin-bottom: 5px;
            }

        .track-order-detail table {
            border-top: 1px solid #CCCCCC;
            border-right: 1px solid #CCCCCC;
        }

            .track-order-detail table th {
                background-color: #eeeeee;
            }

            .track-order-detail table td, .track-order-detail table th {
                text-align: left;
                padding: 2px 10px 2px 5px;
                border-bottom: 1px solid #CCCCCC;
                border-left: 1px solid #CCCCCC;
                color: #363636;
            }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ScriptManager runat="server" ID="smAdmin"></asp:ScriptManager>
    <div class="main-content-wrapper">
        <div class="content-header">
            <div id="functions_wrapper" style="padding-top: 5px; padding-bottom: 5px;">
                <div style="text-align: left; float: left; display: inline-block; width: 400px;">
                    <h3><b>
                        <asp:Literal runat="server" ID="ltrCustomer"></asp:Literal></b> - Order Details</h3>
                </div>
                <div style="float: right;">
                    <asp:Button runat="server" ID="GoBack" Text="Back" CssClass="flat-button" Width="110px" />
                </div>
                <div style="clear: both;"></div>
            </div>
        </div>
        <hr style="color: #d2691e; border: 1px solid #d2691e; margin: 0; padding: 0;" />
        <p></p>
        <div>
        </div>
        <p><asp:Literal runat="server" ID="ltrCustomerInfo"></asp:Literal></p>
        <div class="content-body">
            <asp:UpdatePanel runat="server" ID="upCustomerOrders">
                <ContentTemplate>
                    <div class="track-order-header">
                        <h3>
                            <img src="/Images/order_item.png" alt="" />&nbsp;Ordered At:&nbsp;
                            <asp:Literal runat="server" ID="ltrOrderAt"></asp:Literal>&nbsp;-&nbsp;Status:&nbsp;<span style="color: blue;"><asp:Literal runat="server" ID="ltrOrderStatus"></asp:Literal></span>&nbsp;-&nbsp;Order Type:&nbsp;<span style="color: green;"><asp:Literal runat="server" ID="ltrOrderType"></asp:Literal></span>&nbsp;-&nbsp;Payment Status:&nbsp;<span style="color: red;"><asp:Literal runat="server" ID="ltrPaymentStatus"></asp:Literal></span></h3>
                    </div>
                    <div class="track-order-detail">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <th style="width: 60px; text-align: center;">Quantity</th>
                                <th>Item</th>
                                <th style="width: 100px; text-align: right;">Unit Price (£)</th>
                            </tr>
                            <asp:Repeater runat="server" ID="OrderDetailsRepeater">
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 40px; text-align: center;"><%#Eval("Quantity")%></td>
                                        <td><%#Eval("MenuItemName")%></td>
                                        <td style="width: 80px; text-align: right;"><%#Eval("Price")%></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr style="background-color: #eeeeee;">
                                <td style="text-align: right; font-weight: bold;" colspan="2">Sub Total</td>
                                <td style="text-align: right; font-weight: bold;">
                                    <asp:Literal runat="server" ID="ltrSubTotal"></asp:Literal></td>
                            </tr>
                            <tr style="background-color: #eeeeee;">
                                <td style="text-align: right; font-weight: bold;" colspan="2">Online Discount</td>
                                <td style="text-align: right; font-weight: bold;">
                                    <%--<%#FormatNumber(Eval("Discount"), 2)%>--%>
                                    <asp:Literal runat="server" ID="ltrOnlineDiscount"></asp:Literal>
                                </td>
                            </tr>
                            <tr style="background-color: #eeeeee;">
                                <td style="text-align: right; font-weight: bold;" colspan="2">Delivery Charges</td>
                                <td style="text-align: right; font-weight: bold;">
                                    <%--<%# FormatNumber(Eval("DeliveryCharges"), 2)%>--%>
                                    <asp:Literal runat="server" ID="ltrDeliveryCharges"></asp:Literal>
                                </td>
                            </tr>
                            <tr style="background-color: #eeeeee;">
                                <td style="text-align: right; font-weight: bold; font-size: 18px;" colspan="2">Total Price</td>
                                <td style="text-align: right; font-weight: bold; font-size: 18px;">
                                    <%--<%#FormatNumber(Eval("TotalAmount"), 2)%>--%>
                                    <asp:Literal runat="server" ID="ltrTotalAmount"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
