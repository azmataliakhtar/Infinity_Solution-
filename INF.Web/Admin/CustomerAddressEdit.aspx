<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="false" Inherits="INF.Web.Admin.CustomerAddressEdit" CodeBehind="CustomerAddressEdit.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="Server">
    <article class="page-header" style="margin-top: 10px;">
        <h2><asp:Literal runat="server" ID="ltrCustomer" /></h2>
    </article>
    <article>
        <asp:HiddenField runat="server" ID="hdnCustomerId" />
        <asp:HiddenField runat="server" ID="hdnAddressId" />
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Address Details</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-10">
                    <div style="line-height: 10px;">&nbsp;</div>
                    <div>
                        <div class="col-sm-2">PostCode:</div>
                        <div class="col-sm-4">
                            <asp:TextBox runat="server" ID="PostcodeTextBox" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="PostcodeTextBox" Display="Dynamic"
                                SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="EditCustomerAddress"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-2">City:</div>
                        <div class="col-sm-4">
                            <asp:TextBox runat="server" ID="CityTextBox" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="CityTextBox" Display="Dynamic"
                                SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="EditCustomerAddress"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div style="line-height: 10px;">&nbsp;</div>
                    <div>
                        <div class="col-sm-2">Address:</div>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" ID="AddressTextBox" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="AddressTextBox" Display="Dynamic"
                                SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="EditCustomerAddress"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div style="line-height: 10px;">&nbsp;</div>
                    <div>
                        <div class="col-sm-2">Address Note:</div>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" ID="AddressNoteTextBox" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></div>
                    </div>
                    <div style="line-height: 10px;">&nbsp;</div>
                </div>
                <div class="col-sm-2">
                    <div style="line-height: 10px;">&nbsp;</div>
                    <asp:Button runat="server" ID="SaveButton" Text="Save" CssClass="btn btn-danger" Width="110px" ValidationGroup="EditCustomerAddress" />
                    <div style="line-height: 10px;">&nbsp;</div>
                    <asp:Button runat="server" ID="CancelButton" Text="Cancel" CssClass="btn btn-default" Width="110px" />
                    <div style="line-height: 10px;">&nbsp;</div>
                </div>
            </div>
        </div>
    </article>
</asp:Content>

