<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" Inherits="INF.Web.Admin.CreateUser" CodeBehind="CreateUser.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div>
        <asp:PlaceHolder runat="server" ID="MessageBox" Visible="False">
            <ul>
                <li style="color: #C44113;">
                    <asp:Literal runat="server" ID="Messages"></asp:Literal></li>
            </ul>
        </asp:PlaceHolder>
    </div>
    <fieldset>
        <legend style="margin-bottom: 10px;">Create User</legend>
        <div style="width: 610px; float: left;">
            <table style="width: 600px" cellpadding="0" cellspacing="0" border="0" class="customer-detail-table">
                <tr>
                    <th>User Name:</th>
                    <td>
                        <asp:TextBox runat="server" ID="UserNameTextBox" Width="90%"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="EmailTextBox" Display="Dynamic"
                            SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="CreateUser">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>Email:</th>
                    <td>
                        <asp:TextBox runat="server" ID="EmailTextBox" Width="90%"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RFVEmail" ControlToValidate="EmailTextBox" Display="Dynamic"
                            SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="CreateUser">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>Confirm Email:</th>
                    <td>
                        <asp:TextBox runat="server" ID="ConfirmEmailTextBox" Width="90%"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="ConfirmEmailTextBox" Display="Dynamic"
                            SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="CreateUser">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="ConfirmEmailTextBox" ControlToCompare="EmailTextBox" Display="Dynamic"
                            SetFocusOnError="True" ErrorMessage="Comfirm Email does not match the Email!" ValidationGroup="CreateUser"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <th style="width: 150px; text-align: left;">Password:</th>
                    <td>
                        <asp:TextBox runat="server" ID="PasswordTextBox" Width="90%" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="PasswordTextBox" Display="Dynamic"
                            SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>Confirm Password:</th>
                    <td>
                        <asp:TextBox runat="server" ID="ConfirmPasswordTextBox" Width="90%" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="ConfirmPasswordTextBox" Display="Dynamic"
                            SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                        <asp:CompareValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="ConfirmPasswordTextBox" ControlToCompare="PasswordTextBox" Display="Dynamic"
                            SetFocusOnError="True" ErrorMessage="Confirm Password does not match the Password" ValidationGroup="CreateUser"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <th>Last Name:</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtLastName"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>First Name:</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtFirstName"></asp:TextBox></td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <th>Role:</th>
                    <td>
                        <asp:DropDownList runat="server" ID="UserRolesDropDownList" Width="93%"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-left: 620px;">
            <asp:Button runat="server" ID="SaveButton" Text="Save" CssClass="flat-button" Width="110px" ValidationGroup="CreateUser" />
            <div style="height: 5px; margin: 0; padding: 0;"></div>
            <asp:Button runat="server" ID="CancelButton" Text="Cancel" CssClass="flat-button" Width="110px" />
        </div>
    </fieldset>
</asp:Content>

