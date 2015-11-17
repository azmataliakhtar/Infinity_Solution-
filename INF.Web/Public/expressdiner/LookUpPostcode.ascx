<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="../default/LookUpPostcode.ascx.vb" Inherits="INF.Web.Public.default.LookUpPostcode" %>
<div class="order-selection" id="orderselection">
    <div class="postcodeTitle">Enter your Postcode below</div>
    <div class="postcodeDelivery">
        <div class="postcodeHeader">Delivery</div>
        <div class="postcode" id="postcode">
            <input name="pcode" id="pcode" type="text" placeholder="Enter your postcode"/>
        </div>
        <div class="postcodeButton">
            <a id="searchPcode" href="javascript:void(0);">Enter Restaurant</a>
        </div>
        <div class="clear"></div>
        <div class="message"></div>
    </div>
</div>