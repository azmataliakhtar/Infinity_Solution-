<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/DashboardMaster.master" AutoEventWireup="false" Inherits="INF.Web.Admin.PendingOrders" CodeBehind="PendingOrders.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="Server">
    EPOS Anytime - Pending Orders
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <article class="page-header" style="margin-top: 10px;">
        <h2>Pending Orders</h2>
    </article>
    <article id="error_messages_wrapper">
        <asp:ValidationSummary runat="server" ID="ValidationMsg" EnableClientScript="True" />
        <asp:Label runat="server" ID="MessageLabel" Text="" EnableViewState="True" Visible="False"></asp:Label>
    </article>
    <article>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Search Pending Orders</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-10">
                    <div style="line-height: 10px;">&nbsp;</div>
                    <div class="input-group">
                        <span class="input-group-addon">Recently placed orders:</span>
                        <asp:DropDownList runat="server" ID="TimePeriodOptions" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div style="line-height: 10px;">&nbsp;</div>
                </div>
                <div class="col-sm-2">
                    <div style="line-height: 10px;">&nbsp;</div>
                    <asp:Button runat="server" ID="SearchButton" Text="Search" CssClass="btn btn-danger" Width="110px" />
                    <div style="line-height: 10px;">&nbsp;</div>
                </div>
            </div>
        </div>
    </article>
    <div style="line-height: 10px;">&nbsp;</div>
    <article>
        <p>
            Number of Order: <b>
                <asp:Literal runat="server" ID="ltrNumberOfPendingOrders"></asp:Literal></b>
        </p>
        <asp:DataGrid runat="server" ID="OrdersDataGrid" DataKeyField="ID" AutoGenerateColumns="False" CssClass="table table-striped table-responsive table-bordered"
            AllowPaging="True" PageSize="25" BorderStyle="None" BorderWidth="0" GridLines="None">
            <PagerStyle Mode="NumericPages" PageButtonCount="10" Position="Bottom" HorizontalAlign="Right" Height="26" BorderStyle="None" Font-Bold="True"></PagerStyle>
            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ItemStyle CssClass="table-item"></ItemStyle>
            <Columns>
                <asp:TemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="OrderDetails.aspx?OrderID=<%# Eval("ID")%>&CustomerId=<%# Eval("CustomerID")%>" title="Order Details" class="btn btn-sm btn-info">Details</a>
                        <asp:HiddenField runat="server" ID="hdnOrderID" Value='<%# Eval("ID") %>' />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="Date" DataField="OrderDate"  DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                <asp:BoundColumn HeaderText="Type" DataField="OrderType" />
                <asp:BoundColumn HeaderText="Status" DataField="SagePayStatus" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundColumn HeaderText="Amount" DataField="TotalAmount"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}" />
                <asp:BoundColumn HeaderText="Pay Status" DataField="PayStatus"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundColumn HeaderText="Email" DataField="Email" />
                <asp:BoundColumn HeaderText="Name" DataField="CustomerName"/>
                <asp:BoundColumn HeaderText="Address" DataField="CustomerAddress" />
            </Columns>
            <FooterStyle CssClass="table-footer"></FooterStyle>
        </asp:DataGrid>
    </article>
</asp:Content>
