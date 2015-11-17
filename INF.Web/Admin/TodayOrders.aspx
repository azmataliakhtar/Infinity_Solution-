<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/DashboardMaster.master" AutoEventWireup="false" Inherits="INF.Web.Admin.TodayOrders" CodeBehind="TodayOrders.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="Server">EPOS Anytime - Dashboard</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
    <link rel="stylesheet" href="css/datepicker.css" />
    <%--JavaScript--%>
    <script src="js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=StartDateOnTextBox.ClientID%>').datepicker({
                format: "mm/dd/yyyy"
            });
            $('#<%=EndDateOnTextBox.ClientID%>').datepicker({
                format: "mm/dd/yyyy"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <article class="page-header" style="margin-top: 10px;">
        <h2>Todays Orders</h2>
    </article>
    <article>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Search Orders</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-10">
                    <div style="line-height: 10px;">&nbsp;</div>
                    <div class="input-group" style="float: left; padding-right: 100px">
                        <span class="input-group-addon" style="width: 150px;">Start date </span>
                        <asp:TextBox runat="server" ID="StartDateOnTextBox" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" style="width: 150px;">End date </span>
                        <asp:TextBox runat="server" ID="EndDateOnTextBox" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-2">
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
                <asp:Literal runat="server" ID="ltrNumberOfOrders"></asp:Literal></b>
        </p>
        <asp:DataGrid runat="server" ID="OrderDataGrid" DataKeyField="ID" AutoGenerateColumns="False" CssClass="table table-striped table-responsive table-bordered"
            AllowPaging="True" PageSize="25" BorderStyle="None" BorderWidth="0" GridLines="None">
            <PagerStyle Mode="NumericPages" PageButtonCount="10" Position="Bottom" HorizontalAlign="Right" Height="26" BorderStyle="None" Font-Bold="True"></PagerStyle>
            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ItemStyle CssClass="table-item"></ItemStyle>
            <Columns>
                <asp:BoundColumn HeaderText="Order No" DataField="ID" ItemStyle-Width="20px" />
                <asp:BoundColumn HeaderText="Address" DataField="CustomerAddress" ItemStyle-Width="200px" />
                <asp:BoundColumn HeaderText="Total" DataField="TotalAmount" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}" />
                <asp:BoundColumn HeaderText="Pay Status" DataField="PayStatus" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="OrderView.aspx?OrderID=<%# Eval("ID")%>&CustomerId=<%# Eval("CustomerID")%>" title="Order Details">[View order]</a>
                        <asp:HiddenField runat="server" ID="hdnOrderID" Value='<%# Eval("ID") %>' />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <FooterStyle CssClass="table-footer"></FooterStyle>
        </asp:DataGrid>
    </article>
</asp:Content>
