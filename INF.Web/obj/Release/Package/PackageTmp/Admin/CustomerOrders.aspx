<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="false" Inherits="INF.Web.Admin.CustomerOrders" CodeBehind="CustomerOrders.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="Server">
    <asp:ScriptManager runat="server" ID="smAdmin"></asp:ScriptManager>
    <article class="page-header" style="margin-top: 10px;">
        <h3>Customer - <b>
            <asp:Literal runat="server" ID="ltrCustomer"></asp:Literal></b></h3>
    </article>
    <article>
        <div style="float: right;">
            <asp:Button runat="server" ID="GoBack" Text="Back" CssClass="btn btn-danger" Width="110px" />
        </div>
        <div style="clear: both;"></div>
    </article>
    <div style="line-height: 10px;">&nbsp;</div>
    <article>
        <div>
            <div class="col-sm-2"><label class="text-right">Recently placed orders:</label></div>
            <div class="col-sm-10">
                <asp:DropDownList runat="server" ID="TimePeriodOptions" style="max-width: 300px;" CssClass="form-control" AutoPostBack="True"></asp:DropDownList></div>
        </div>
    </article>
    <div style="line-height: 10px;">&nbsp;</div>
    <article>
        <asp:UpdatePanel runat="server" ID="upCustomerOrders">
            <ContentTemplate>
                <div class="col-sm-12">
                    <p style="padding: 0px; margin: 0;">Number of orders placed:&nbsp;<b><asp:Literal runat="server" ID="ltrNumberOfOrders"></asp:Literal></b></p>
                </div>
                <asp:Repeater runat="server" ID="OrdersRepeater">
                    <ItemTemplate>
                        <div class="col-sm-12">
                            <h4>
                                <img src="/Images/order_item.png" alt="" />&nbsp;Ordered At:&nbsp; <%# Eval("OrderDate")%>&nbsp;-&nbsp;Status:&nbsp;<span style="color: blue;"><%# Eval("OrderStatus")%></span>&nbsp;-&nbsp;Order Type:&nbsp;<span style="color: green;"><%#Eval("OrderType")%></span>&nbsp;-&nbsp;Payment Status:&nbsp;<span style="color: red;"><%# Eval("PayStatus") %></span></h4>
                        </div>
                        <div class="col-sm-12">
                            <table class="table table-striped table-responsive table-bordered">
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
                                        <asp:Literal runat="server" ID="OrderSubTotalPrice"></asp:Literal></td>
                                </tr>
                                <tr style="background-color: #eeeeee;">
                                    <td style="text-align: right; font-weight: bold;" colspan="2">Online Discount</td>
                                    <td style="text-align: right; font-weight: bold;"><%#FormatNumber(Eval("Discount"), 2)%></td>
                                </tr>
                                <tr style="background-color: #eeeeee;">
                                    <td style="text-align: right; font-weight: bold;" colspan="2">Delivery Charges</td>
                                    <td style="text-align: right; font-weight: bold;"><%# FormatNumber(Eval("DeliveryCharges"), 2)%></td>
                                </tr>
                                <tr style="background-color: #eeeeee;">
                                    <td style="text-align: right; font-weight: bold; font-size: 18px;" colspan="2">Total Price</td>
                                    <td style="text-align: right; font-weight: bold; font-size: 18px;"><%#FormatNumber(Eval("TotalAmount"), 2)%></td>
                                </tr>
                            </table>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TimePeriodOptions" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </article>
</asp:Content>

