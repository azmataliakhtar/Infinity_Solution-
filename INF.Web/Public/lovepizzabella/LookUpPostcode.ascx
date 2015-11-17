<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="LookUpPostcode.ascx.vb" Inherits="INF.Web.Public.default.LookUpPostcode" %>
<div class="order-selection" id="orderselection">
    <div class="postcodeTitle">Enter your Postcode below</div>
    <div class="postcodeDelivery">
        <div class="postcodeHeader">Delivery</div>
        <div class="postcode" id="postcode">
            <input name="pcode" id="pcode" type="text" placeholder="Enter Your Postcode..."/>
        </div>
        <div class="postcodeButton">
            <a id="searchPcode" href="javascript:void(0);">ENTER Restaurant</a>
        </div>
        <div class="clear"></div>
        <div class="message"></div>
    </div>

    <div class="postcodeCollection">
        <div class="postcodeHeader">Collection</div>
        <div class="postcodeButton">
            <a id="opt_collection_order" href="javascript:void(0);">Select</a>
        </div>
    </div>
</div>