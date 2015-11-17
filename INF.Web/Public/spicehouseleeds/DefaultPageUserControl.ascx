<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DefaultPageUserControl.ascx.vb" Inherits="INF.Web.Public.default.DefaultPageUserControl" %>
<div class="HomeContainer">
    <div id="slider">
        <div class="orderNow" style="position: absolute; top: 60px; right: 50px; z-index: 99; /* float: right; */">
            <a href="Menu.aspx">
                <img alt="" src="/App_Themes/spicehouseleeds/Images/OrderNow.png" border="0"></a>
        </div>
        <div class="slides_container">

            <div class="slide">
                <a href="#">
                    <img src="/App_Themes/spicehouseleeds/images/indian_food_banner1.jpg" alt="Authentic Indian Cuisine in Leeds" /></a>
            </div>
            <div class="slide">
                <a href="#">
                    <img src="/App_Themes/spicehouseleeds/images/indian_food_banner2.jpg" alt="Authentic Indian Cuisine in Leeds" /></a>

            </div>
            <div class="slide">
                <a href="#">
                    <img src="/App_Themes/spicehouseleeds/images/indian_food_banner3.jpg" alt="Authentic Indian Cuisine in Leeds" /></a>
            </div>
        </div>
        <a href="#" class="prev">
            <img src="/App_Themes/spicehouseleeds/images/arrow-prev.png" width="24" height="43" alt="Authentic Indian Cuisine in Leeds" /></a>
        <a href="#" class="next">
            <img src="/App_Themes/spicehouseleeds/images/arrow-next.png" width="24" height="43" alt="Authentic Indian Cuisine in Leeds" /></a>
    </div>
</div>