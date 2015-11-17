<%@ page title="" language="VB" masterpagefile="~/SiteMaster.master" autoeventwireup="false" Inherits="INF.Web.ThankYou" Codebehind="ThankYou.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadExtra" runat="Server">
<link href="Css/styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style3
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="MainContentPlaceHolderContent" ContentPlaceHolderID="MainContent"
    runat="Server">
    <section class="cstmsection">
        <table class="style3">
            
             <tr>
                <td align="left" class="menuheading">
                      Thanks for your Order !
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="bodytext2" align="left" style="margin-left: 10px" >
                <br />
                    Thank you for placing an order on our website.
                    <br/>
                    We have received your order and an email has been sent at your email ID with which you registered with us.
                    <br /><br />
                    You can <a href="TrackOrder.aspx"> <span style="color: #FFFF00">Track your order</span> </a> untill we deliver it to you.<br />
                    <br />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </section>
</asp:Content>

