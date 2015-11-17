<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="LookUpPostcode.ascx.vb" Inherits="INF.Web.UserControls.LookUpPostcode" %>
<style type="text/css">
    /*
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

    */
</style>
<div id="overlay" class="overlay">
</div>
<asp:PlaceHolder runat="server" ID="phPostcodeLookUp">
</asp:PlaceHolder>
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

                    $(".contentpop").fadeOut();
                    $('.contentpopLoading').fadeIn();


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

                $(".contentpop").fadeOut();
                $('.contentpopLoading').fadeIn();


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