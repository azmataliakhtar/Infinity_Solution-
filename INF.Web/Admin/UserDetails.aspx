<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" Inherits="INF.Web.Admin.UserDetails" Codebehind="UserDetails.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div>
        <h2>User Details</h2>
    </div>
    <hr style="color: #d2691e; border-bottom: 1px solid #d2691e; border-top: none; border-left: none; border-right: none;" />
    <div>
        <asp:PlaceHolder runat="server" ID="MessageBox" Visible="False">
            <ul>
                <li style="color: #C44113;">
                    <asp:Literal runat="server" ID="Messages"></asp:Literal></li>
            </ul>
        </asp:PlaceHolder>
    </div>
    <fieldset>
        <legend style="margin-bottom: 10px;"></legend>
        <div style="width: 610px; float: left;">
            <table style="width: 600px" cellpadding="0" cellspacing="0" border="0" class="customer-detail-table">
                <tr>
                    <th style="width: 150px; text-align: left;">User Name:</th>
                    <td>
                        <asp:TextBox runat="server" ID="UserNameTextBox" Width="90%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Email:</th>
                    <td>
                        <asp:TextBox runat="server" ID="EmailTextBox" Width="90%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Is Actived:</th>
                    <td>
                        <asp:TextBox runat="server" ID="IsActivedTextBox" Width="90%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <th>Role:</th>
                    <td>
                        <asp:TextBox runat="server" ID="UserRoleTextBox" Width="90%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-left: 620px;">
            <asp:Button runat="server" ID="ChangePasswordButton" Text="Change Pwd" CssClass="flat-button" Width="110px"/>
            <div style="height: 5px; margin: 0; padding: 0;"></div>
            <asp:Button runat="server" ID="BlockAndUnBlockButton" Text="Block" CssClass="flat-button" Width="110px"/>
            <div style="height: 20px; margin: 0; padding: 0;"></div>
            <asp:Button runat="server" ID="CancelButton" Text="Cancel" CssClass="flat-button" Width="110px" />
        </div>
    </fieldset>
</asp:Content>

