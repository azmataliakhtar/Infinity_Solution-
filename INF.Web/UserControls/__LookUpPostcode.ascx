<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="__LookUpPostcode.ascx.vb" Inherits="INF.Web.UserControls.__LookUpPostcode" %>
<%@ Import Namespace="INF.Web.UI.Settings" %>
<style type="text/css">
    .order-type-header-text
    {
        font-size: 26px;
        font-weight: bold;
        font-family: sans-serif;
        text-transform: uppercase;
        font-style:normal;
        color: #000000;
        text-shadow: 2px 2px 2px #a0a0a0;
    }
    th.order-type-header {
        width: 200px; 
        text-align: left;
        padding-left: 10px;
        background-color: transparent;
        border: 1px solid gainsboro;
    }
    td.order-type-option {
        border: 1px solid gainsboro;
    }
</style>
<div id="overlay" class="overlay">
</div>
<div class="order-selection" id="orderselection" style="display: block;">
    <table style="width: 100%;">
        <tr>
            <th class="order-type-header">
                <span class="order-type-header-text">Delivery</span>
            </th>
            <td class="order-type-option">
                <div>
                    <h3 class="title" style="text-align: left;padding-left:10px;"> Enter your postcode to order from <%= EPATheme.Current.Themes.WebsiteName%></h3>
                    <div class="postcode postcode2" id="postcode" style="display: block;width: 100%;">
                        <input name="pcode" id="pcode" type="text" value="" style="" />
                        <a id="searchPcode" href="javascript:void(0);" class="button" style="<%= BackgroundCss%>">
                            <span>Search</span></a>
                    </div>
                    <div class="clear" style="height: 42px;">&nbsp;</div>
                    <div class="message" style="margin: 5px 0;padding-left:10px;"></div>
                    <div class="clear"></div>
                </div>
            </td>
        </tr>
        <tr>
            <th colspan="2">
                <hr style="border-color: #d3d3d3;" />
            </th>
        </tr>
        <tr>
            <th class="order-type-header">
                <span class="order-type-header-text">Collection</span>
            </th>
            <td class="order-type-option">
                <div class="postcode postcode2" id="Div1" style="display: block; padding: 20px 10px;">
                    <a id="opt_collection_order" href="javascript:void(0);" class="button" style="<%= BackgroundCss%>"><span>Continue</span></a>
                </div>
                <div class="clear" style="height: 42px;">&nbsp;</div>
            </td>
        </tr>
    </table>
    <%--Collection Order//--%>
    <%--//Collection Order--%>
</div>
<script type="text/javascript">
    /*
    $(document).ready(function () {
        $("#orderselection").css("top", ($(window).height()) / 2 - $("#orderselection").height());
        $("#orderselection").css("left", (($(window).width() - 996) / 2) + ((996 - $("#orderselection").width()) / 2));
    });
    */

    $("#pcode").keypress(function (event) {
        if (event.keyCode == 13) {
            CheckPostCode();
            return false;
        }
        return true;
    });

    function CheckPostCode() {
        var pcode = $("#pcode").val();
        if (pcode == "") {
            $(".order-selection .message").css("display", "block");
            $(".order-selection .message").html("Please enter postcode to start placing delivery order");
            return false;
        }

        $.ajax({
            url: "/Ajax/CheckPostCode.ashx",
            type: "POST",
            cache:false,
            data: {
                'pcode': pcode
            },
            dataType: "html",
            success: function (result) {
                if (result == 1) {
                    $(".order-selection .message").css("display", "none");
                    //window.location.href = window.location.href;
                    location.reload();
                }
                else if (result == -1) {
                    $(".order-selection .message").css("display", "block");
                    $(".order-selection .message").html("Were are sorry! We do not deliver goods to your place.");
                }
                else {
                    alert("Were are sorry! We do not deliver goods to your place.");
                }
            },
            error: function (error) { }
        });
        return false;
    }

    function StartCollectionOrder() {
        $.ajax({
            url: "ajax/StartCollectionOrderHandler.ashx",
            type: "POST",
            cache:false,
            data: {
                'order_type': 'COLLECTION'
            },
            dataType: 'html',
            success: function (result) {
                console.log(result);
                $(".order-selection .message").css("display", "none");
               // window.location.href = window.location.href;
                location.reload();
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    $("#searchPcode").click(function () {
        CheckPostCode();
    });

    $("#opt_collection_order").click(function () {
        StartCollectionOrder();
    });
</script>