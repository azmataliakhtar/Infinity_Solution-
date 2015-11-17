<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="../default/DefaultPageUserControl.ascx.vb" Inherits="INF.Web.Public.default.DefaultPageUserControl" %>
<%@ Import Namespace="INF.Web.UI.Settings" %>
<div class="HomeContainer" style="background-image: url('<%= EPATheme.Current.Themes.BackgroundHomePage %>');background-size: cover;">
    <div class="col-md-12 col-xs-12" style="padding-top: 35px;padding-right: 20px;">
        <div class="form-group row">
            <div class="pull-right">
                <a href="Menu.aspx"><img alt="click here to order now" src="/App_Themes/expressdiner/Images/OrderNow.png" /></a>
            </div>
        </div>
        <div class="form-group row">
            <div class="pull-right">
                <img alt="pay by card or by cash at your door" src="/App_Themes/expressdiner/Images/payment.png" />
            </div>
        </div>
    </div>
</div>
