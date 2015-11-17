<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="false" Inherits="INF.Web.Admin.Customers" CodeBehind="Customers.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageContent" runat="Server">
    <article class="page-header" style="margin-top: 10px;">
        <h2>Customer List</h2>
    </article>
    <div id="error_messages_wrapper">
        <asp:ValidationSummary runat="server" ID="ValidationMsg" EnableClientScript="True" />
        <asp:Label runat="server" ID="MessageLabel" Text="" EnableViewState="True" Visible="False"></asp:Label>
    </div>
    <article>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Search customers</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-10">
                    <div style="line-height: 10px;">&nbsp;</div>
                    <div class="row">
                        <div class="col-sm-2"><span>Customer Name:</span></div>
                        <div class="col-sm-10"><asp:TextBox runat="server" ID="CustomerNameTextBox" CssClass="form-control"></asp:TextBox></div>
                    </div>
                    <div style="line-height: 10px;">&nbsp;</div>
                    <div class="row">
                        <div class="col-sm-2">
                            <span>Email:</span>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox runat="server" ID="EmailTextBox" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2">
                            <span>Telephone/Mobile:</span>
                        </div>
                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TelephoneTextBox" CssClass="form-control"></asp:TextBox></div>
                    </div>
                    <div style="line-height: 10px;">&nbsp;</div>
                    <div class="row">
                        <div class="col-sm-2">
                            <span>Recently placed orders:</span>
                        </div>
                        <div class="col-sm-10">
                            <asp:DropDownList runat="server" ID="TimePeriodOptions" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div style="line-height: 10px;">&nbsp;</div>
                </div>
                <div class="col-sm-2">
                    <div style="line-height: 10px;">&nbsp;</div>
                    <asp:Button runat="server" ID="SearchButton" Text="Search" CssClass="btn btn-danger" Width="110px" />
                    <div style="line-height: 10px;">&nbsp;</div>
                    <asp:Button runat="server" ID="ResetButton" Text="Reset" CssClass="btn btn-danger" Width="110px" />
                    <div style="line-height: 10px;">&nbsp;</div>
                    <asp:Button runat="server" ID="UnBlockAllCustomerButton" Text="UnBlock All" CssClass="btn btn-default" Width="110px" />
                    <div style="line-height: 10px;">&nbsp;</div>
                </div>
            </div>
        </div>
    </article>
    <article>
        <p>
            Number of customer: <b>
                <asp:Literal runat="server" ID="ltrNumberOfCustomers"></asp:Literal></b>
        </p>
        <asp:DataGrid runat="server" ID="CustomerDataGrid" DataKeyField="ID" AutoGenerateColumns="False" CssClass="table table-striped table-bordered"
            AllowPaging="True" PageSize="25" BorderStyle="None" BorderWidth="0" GridLines="None">
            <PagerStyle Mode="NumericPages" PageButtonCount="10" Position="Bottom" HorizontalAlign="Right" Height="26" BorderStyle="None" Font-Bold="True"></PagerStyle>
            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ItemStyle CssClass="table-item"></ItemStyle>
            <Columns>
                <asp:TemplateColumn ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" HeaderText="#">
                    <ItemTemplate>
                        <a href="CustomerDetails.aspx?CustomerId=<%# Eval("ID")%>" title="Address" class="btn btn-sm btn-info">Details</a>&nbsp;
                        <a href="CustomerOrders.aspx?CustomerId=<%# Eval("ID")%>" title="Orders" class="btn btn-sm btn-warning">Orders</a>
                        <asp:HiddenField runat="server" ID="hdnCustomerID" Value='<%# Eval("ID") %>' />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="FirstName" DataField="FirstName"/>
                <asp:BoundColumn HeaderText="LastName" DataField="LastName"/>
                <asp:BoundColumn HeaderText="Telephone" DataField="Telephone"/>
                <asp:BoundColumn HeaderText="Mobile" DataField="Mobile"/>
                <asp:BoundColumn HeaderText="Email" DataField="Email" />
            </Columns>
            <FooterStyle CssClass="table-footer"></FooterStyle>
        </asp:DataGrid>
    </article>
</asp:Content>

