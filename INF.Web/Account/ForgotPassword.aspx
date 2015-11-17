<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ForgotPassword.aspx.vb" Inherits="INF.Web.Account.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administrative Panel - Forgot Password</title>
    <link type="text/css" rel="stylesheet" href="../Css/admin-styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="margin: 200px auto; width: 600px; background-color: #ffffff; padding: 20px;">
                <div>
                    <asp:ValidationSummary runat="server" ID="vmValidation" EnableClientScript="True"/>
                    <asp:PlaceHolder runat="server" ID="phMessages" Visible="False">
                        <ul>
                            <li><span style="color: red;"><asp:Literal runat="server" ID="ltrMessage"></asp:Literal></span></li>
                        </ul>
                    </asp:PlaceHolder>
                </div>
                <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td style="width: 270px; text-align: right;padding: 5px;">Enter your email to get new password:&nbsp;</td>
                        <td style="padding: 5px;">
                            <asp:TextBox runat="server" ID="txtEmail" Width="250px" CssClass="normal-input"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="Validator1" ControlToValidate="txtEmail" ErrorMessage="You have to input email to get new password!" Display="None"
                                SetFocusOnError="True" EnableClientScript="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ID="Validator2" ControlToValidate="txtEmail" ErrorMessage="This is not a email address!" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" EnableClientScript="True" Display="None"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px;">&nbsp;</td>
                        <td style="padding: 5px;"><asp:Button runat="server" ID="btnSendEmail" Text="Get New Password" CssClass="flat-button"/></td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
