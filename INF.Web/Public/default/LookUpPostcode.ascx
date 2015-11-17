<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="LookUpPostcode.ascx.vb" Inherits="INF.Web.Public.default.LookUpPostcode" %>
<%@ Import Namespace="INF.Web.UI.Settings" %>

<div class="order-selection" id="orderselection" style="">
    
    <div class="contentpop">

        <h3 class="howdoyou">How do you want to order ?</h3>

       

        <div class="poporderDelivery">
            <p>Please enter your postcode and select the Delivery button.<br/></p>

            <p class="pstcderr" style="display:none;color:#cc0000;">Please input a valid Post Code</p>

            <div class="message" style="display:none;color:#cc0000;margin-bottom:10px;">We are sorry ! We do not deliver to your place.</div>

             <input name="pcode" id="pcode" type="text" value="" style="" />
                
            <a id="facekDelBtn"><i class="fa fa-truck"></i> Delivery</a>
            <a id="searchPcode" href="#" class="button" style="display:none;"></a>

        </div>

         <div class="poporderCollection">
              <p>Don't want a delivery?<br/> Order and come and collect from our Restaurant.</p>
             <a id="opt_collection_order" href="#" class="button" style=""><span>Collection</span></a>
        </div>

        

        
 

    </div>

    <div class="contentpopLoading">

    </div>

    <%--Collection Order//--%>
    <%--//Collection Order--%>
</div>