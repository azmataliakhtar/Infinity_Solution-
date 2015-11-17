<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="false" Inherits="INF.Web.Admin.CustomerDetails" CodeBehind="CustomerDetails.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="Server">
    <asp:ScriptManager runat="server" ID="smAdmin"></asp:ScriptManager>
    <article class="page-header" style="margin-top: 10px;">
        <h2>Customer&nbsp;-&nbsp;<asp:Literal runat="server" ID="lblCustomerName"></asp:Literal></h2>
    </article>

    <article id="functions_wrapper">
        <div style="float: right;">
            <asp:Button runat="server" ID="GoBack" Text="Back" CssClass="btn btn-danger" Width="110px" />
        </div>
        <div style="line-height: 10px;">&nbsp;</div>
        <div style="clear: both;"></div>
    </article>
    <article>
        <div class="" role="alert">
            <asp:Label runat="server" ID="MessageLabel" Text="" EnableViewState="False"></asp:Label>
        </div>
        <div style="line-height: 10px;">&nbsp;</div>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Basic Information</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-10">
                    <div style="line-height: 10px;">&nbsp;</div>
                    <div>
                        <div class="col-sm-2">Email:</div>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" ID="EmailTextBox" Enabled="False" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RFVEmail" ControlToValidate="EmailTextBox" Display="Dynamic"
                                SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="EditCustomer">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div style="line-height: 10px;">&nbsp;</div>
                    <div>
                        <div class="col-sm-2">Telephone:</div>
                        <div class="col-sm-4">
                            <asp:TextBox runat="server" ID="TelephoneTextBox" Enabled="False" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="TelephoneTextBox" Display="Dynamic"
                                SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="EditCustomer"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-2">Mobile:</div>
                        <div class="col-sm-4">
                            <asp:TextBox runat="server" ID="MobileTextBox" Enabled="False" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div style="line-height: 10px;">&nbsp;</div>
                    <div>
                        <div class="col-sm-2">Last Name:</div>
                        <div class="col-sm-4">
                            <asp:TextBox runat="server" ID="LastNameTextBox" Enabled="False" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="LastNameTextBox" Display="Dynamic"
                                SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="EditCustomer"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-2">First Name:</div>
                        <div class="col-sm-4">
                            <asp:TextBox runat="server" ID="FirstNameTextBox" Enabled="False" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="FirstNameTextBox" Display="Dynamic"
                                SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="EditCustomer"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div style="line-height: 10px;">&nbsp;</div>
                    <div>
                        <div class="col-sm-2">Member Since:</div>
                        <div class="col-sm-4">
                            <asp:TextBox runat="server" ID="MemberSinceTextBox" Enabled="False" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2">Is Active:</div>
                        <div class="col-sm-4">
                            <asp:TextBox runat="server" ID="IsActiveTextBox" Enabled="False" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div style="line-height: 10px;">&nbsp;</div>
                </div>
                <div class="col-sm-2">
                    <div style="line-height: 10px;">&nbsp;</div>
                    <asp:Button runat="server" ID="EditButton" Text="Edit" CssClass="btn btn-danger" Width="110px" />
                    <div style="line-height: 10px;">&nbsp;</div>
                    <asp:Button runat="server" ID="SaveButton" Text="Save" CssClass="btn btn-danger" Width="110px" ValidationGroup="EditCustomer" />
                    <div style="line-height: 10px;">&nbsp;</div>
                    <asp:Button runat="server" ID="BlockCustomerButton" Text="Block" CssClass="btn btn-default" Width="110px" />
                    <div style="line-height: 10px;">&nbsp;</div>
                    <asp:Button runat="server" ID="UnBlockCustomerButton" Text="UnBlock" CssClass="btn btn-default" Width="110px" />
                    <div style="line-height: 10px;">&nbsp;</div>
                </div>
            </div>
        </div>
    </article>
    <article>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Registered Address</h3>
            </div>
            <div class="panel-body">
                <asp:DataGrid runat="server" ID="CustomerAddressDataGrid" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                    <ItemStyle CssClass="table-item"></ItemStyle>
                    <Columns>
                        <asp:BoundColumn HeaderText="Postcode" DataField="PostCode"/>
                        <asp:BoundColumn HeaderText="City" DataField="City" />
                        <asp:BoundColumn HeaderText="East" DataField="GridEast"/>
                        <asp:BoundColumn HeaderText="North" DataField="GridNorth"/>
                        <asp:BoundColumn HeaderText="Distance" DataField="Distance"/>
                        <asp:BoundColumn HeaderText="Address" DataField="Address" />
                        <asp:TemplateColumn ItemStyle-Width="90px">
                            <ItemTemplate>
                                <a href="CustomerAddressEdit.aspx?CustomerId=<%#Eval("CustomerID")%>&AddressId=<%# Eval("ID")%>" class="btn btn-sm btn-info" style="width: 80px;">Edit</a><%--&nbsp;|&nbsp;
                            <a href="CustomerAddressEdit.aspx?CustomerId=<%#Eval("CustomerID")%>&AddressId=<%# Eval("ID")%>">Delete</a>--%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <FooterStyle CssClass="table-footer"></FooterStyle>
                </asp:DataGrid>
            </div>
        </div>

    </article>

</asp:Content>

