<%@ Control Language="VB" AutoEventWireup="false" Inherits="INF.Web.Admin.MainNavigation" Codebehind="MainNavigation.ascx.vb" %>
<div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
    <div class="container-fluid">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
              <span class="sr-only">Toggle navigation</span>
              <span class="icon-bar"></span>
              <span class="icon-bar"></span>
              <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="#" style="font-size: 200%; font-weight: bold;">EPOS Anytime</a>
        </div>
        <div class="navbar-collapse collapse" style="font-size: 110%;text-transform: uppercase;">
            <asp:PlaceHolder runat="server" ID="phMainNavigation"></asp:PlaceHolder>
        </div>
    </div>
</div>