<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GetShortBasketInfo.aspx.vb" Inherits="INF.Web.Ajax.GetShortBasketInfo" %>

<strong>Your basket:</strong>&nbsp;Items: #<asp:literal runat="server" id="ltrItemNbr"></asp:literal>&nbsp;&nbsp;-&nbsp;&nbsp;
<strong>Total Price:</strong><asp:literal runat="server" id="ltrTotalPrice"></asp:literal>&nbsp;&nbsp;&nbsp;&nbsp;
<a href="javascript:Refresh_WithoutMinOrderPrice();" class="btn btn-sm" style="background-color: #ff0000; color: #ffffff;">Check Out</a>