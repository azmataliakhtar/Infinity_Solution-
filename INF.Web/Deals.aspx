<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.master" CodeBehind="Deals.aspx.vb" Inherits="INF.Web.Deals" %>

<%@ Import Namespace="INF.Web.UI.Settings" %>
<%@ Import Namespace="System.Security.Policy" %>


<asp:Content ID="GoogleContent" ContentPlaceHolderID="GooglePlaceHolder" runat="Server">
        
      <%
          If (ConfigurationManager.AppSettings("google-site-verification") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("google-plus-link")))) Then
        %>                                
             <meta name="google-site-verification" content="<%=ConfigurationManager.AppSettings("google-site-verification")%>"/>
        <%
        End If
    %>    
    
     <%
         If (ConfigurationManager.AppSettings("google-plus-link") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("google-plus-link")))) Then
        %>                                
             <link href="<%=ConfigurationManager.AppSettings("google-plus-link")%>" rel="publisher"/>
        <%
        End If
    %>   
   
     <%
         If (ConfigurationManager.AppSettings("geo-region") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("geo-region")))) Then
        %>                                
             <meta name="geo.region" content="<%=ConfigurationManager.AppSettings("geo-region")%>" />
        <%
        End If
    %> 

    <%
        If (ConfigurationManager.AppSettings("geo-placename") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("geo-placename")))) Then
        %>                                
             <meta name="geo.placename" content="<%=ConfigurationManager.AppSettings("geo-placename")%>" />
        <%
        End If
    %> 


     <%
         If (ConfigurationManager.AppSettings("geo-position") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("geo-position")))) Then
        %>                                
             <meta name="geo.position" content="<%=ConfigurationManager.AppSettings("geo-position")%>" />
        <%
        End If
    %> 

     <%
         If (ConfigurationManager.AppSettings("google-icbm") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("google-icbm")))) Then
        %>                                
             <meta name="ICBM" content="<%=ConfigurationManager.AppSettings("google-icbm")%>" />
        <%
        End If
    %> 

</asp:Content>

<asp:Content runat="server" ID="LinkCanonicalContent" ContentPlaceHolderID="LinkCanonical">

    <%
        If (ConfigurationManager.AppSettings("linkCanonicalDeals") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("linkCanonicalDeals")))) Then
        %>                                
            <link rel="canonical" href="<%=ConfigurationManager.AppSettings("linkCanonicalDeals")%>" />
        <%
        End If
    %>   

     <%
         If (ConfigurationManager.AppSettings("descDeals") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("descDeals")))) Then
        %>                                
            <meta name="description" content="<%=ConfigurationManager.AppSettings("descDeals")%>" />
        <%
        End If
    %>   

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <%
        If (ConfigurationManager.AppSettings("titleDeals") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titleDeals")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titleDeals")%>
        <%
        Else
            %> 
                <%= EPATheme.Current.Themes.WebsiteName%>  
            <%
        End If
    %>   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadExtra" runat="server">
    <script type="text/javascript" src="Scripts/ShoppingCart.js"></script>
    <script type="text/javascript">
        /**************************************************************/
        /* Client scripts for the page Deals.aspx
        /**************************************************************/
        var _selectedDealId = 0;
        var _selectedDealMenuId = 0;
        var _toppingIdsToAdd = new Array();
        var _dressingIdsToAdd = new Array();
        var _optionsIdToAdd = new Array();
        var _arrMenuItems = new Array();

        var _newCartItemId = "";
        var _isSecondOption = false;
        var _selectionCtrl;

        /**
        * Load the basket when entering the page
        **/
        $(function () {
            LoadSmallShopCartWithDeals();
        });

        /**
        * This function to handle the onclick event of "AddToCart" button
        **/
        function onInitAddingNewItem() {
            _arrMenuItems = new Array();
        }

        /**
        *
        **/
        function getFlickerSolved() {
            $('#<%=pnlDealOptions.ClientID%>').hide();
            $('#<%=pnlTopping.ClientID%>').hide();
            _newCartItemId = "";
        }

        /**
        *
        **/
        function ShowToppingOrDressingOnSeletionOfOption(slc) {
            _selectedDealId = $('#<%=hdnDealSelected.ClientID%>').val();

            var selVal = slc.options[slc.selectedIndex].value;
            $('#<%=hdnSelectedOptionId.ClientID%>').value = selVal;

            _selectionCtrl = slc.id;

            _selectedDealMenuId = selVal;
            _toppingIdsToAdd = new Array();
            _dressingIdsToAdd = new Array();
            _optionsIdToAdd = new Array();
            _isSecondOption = false;

            $.ajax({
                type: "POST",
                cache: false,
                url: "Ajax/ShoppingService.asmx/GetMenuLinkingInfo",
                data: "{'menuId':'" + _selectedDealMenuId + "', 'dealId':'" + _selectedDealId + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (result) {
                    if (result != null && result.d != "") {
                        if (result.d == "no_extra_items") {
                            OnCloseToppingOrDressingModal();
                        } else {
                            $find("<%=mpeToppingModal.ClientID%>").set_dynamicContextKey(result.d);
                            $find('<%= mpeToppingModal.ClientID%>').show();
                        }
                    }
                },
                error: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert("Message: " + r.Message);
                    alert("StackTrace: " + r.StackTrace);
                    alert("ExceptionType: " + r.ExceptionType);
                }
            });

        }

        /************************************************************/
        /* Update the selection on the popup
        /************************************************************/
        function OnCloseToppingOrDressingModal() {

            var inclToppingIdStr = '';
            if (_toppingIdsToAdd.length > 0) {
                for (var i = 0; i < _toppingIdsToAdd.length; i++) {
                    inclToppingIdStr += _toppingIdsToAdd[i] + ';';
                }
            }

            var dressingIdStr = '';
            if (_dressingIdsToAdd.length > 0) {
                for (var i = 0; i < _dressingIdsToAdd.length; i++) {
                    dressingIdStr += _dressingIdsToAdd[i] + ';';
                }
            }

            var optionIdStr = '';
            if (_optionsIdToAdd.length > 0) {
                for (var i = 0; i < _optionsIdToAdd.length; i++) {
                    optionIdStr += _optionsIdToAdd[i] + ';';
                }
            }

            var menuItem = {
                dealId: _selectedDealId,
                menuId: _selectedDealMenuId,
                subMenuId: _selSubMenuId,
                selectId: _selectionCtrl,
                optionIds: optionIdStr,
                toppingIds: inclToppingIdStr,
                dressingIds: dressingIdStr
            };

            var indexOfExistElement = -1;
            if (_arrMenuItems != null && _arrMenuItems.length > 0) {
                for (var index = 0; index < _arrMenuItems.length; index++) {
                    if (_arrMenuItems[index].dealId == menuItem.dealId && _arrMenuItems[index].selectId == menuItem.selectId) {
                        indexOfExistElement = index;
                        break;
                    }
                }
            }
            if (indexOfExistElement == -1) {
                _arrMenuItems[_arrMenuItems.length] = menuItem;
            } else {
                var existedElement = _arrMenuItems[indexOfExistElement];
                existedElement.toppingIds += menuItem.toppingIds;
                existedElement.dressingIds += menuItem.dressingIds;
                existedElement.optionIds += menuItem.optionIds;

                _arrMenuItems[indexOfExistElement] = existedElement;
            }

            console.log("Your selection is: DealID = " + _arrMenuItems[_arrMenuItems.length - 1].dealId +
                "/ MenuID = " + _arrMenuItems[_arrMenuItems.length - 1].menuId +
                " WITH OptionIDs(" + _arrMenuItems[_arrMenuItems.length - 1].optionIds + ")" +
                "/ ToppingIDs(" + _arrMenuItems[_arrMenuItems.length - 1].toppingIds + ")" +
                "/ DressingIDs(" + _arrMenuItems[_arrMenuItems.length - 1].dressingIds + ")");

            if (indexOfExistElement != -1) {
                inclToppingIdStr = _arrMenuItems[indexOfExistElement].toppingIds;
                dressingIdStr = _arrMenuItems[indexOfExistElement].dressingIds;
                optionIdStr = _arrMenuItems[indexOfExistElement].optionIds;
            }

            $.ajax({
                type: "POST",
                cache: false,
                url: "Ajax/ShoppingService.asmx/UpdateSelectionForDeal",
                data: "{'menuId':'" + _selectedDealMenuId + "', 'toppingIds':'" + inclToppingIdStr + "', 'dressingIds':'" + dressingIdStr + "', 'optionIds':'" + optionIdStr + "', 'selector':'" + _selectionCtrl + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (result) {
                    if (result != null) {
                        var id = "#div_current_selection_" + _selectionCtrl + "";
                        var existedDiv = $(id);
                        if (existedDiv.length > 0) {
                            existedDiv.html(result.d);
                        } else {
                            $('#deal_selection').append(result.d);
                        }
                    }
                },
                error: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert("Message: " + r.Message);
                    alert("StackTrace: " + r.StackTrace);
                    alert("ExceptionType: " + r.ExceptionType);
                }
            });

            // Hide the current popup modal
            hideToppingOrDressingModal();

            // Check to see if the item has second option
            if (!_isSecondOption) {
                shouldOpenNextExtraItems();
            }

            if ($('#<%=pnlDealOptions.ClientID%>') != null) {
                $('#<%=pnlDealOptions.ClientID%>').show();
            }
        }

        function onDealOptionsClose() {
            addDealWithSelection();
            hidePopupModals();
        }

        function hidePopupModals() {
            $('#<%=pnlDealOptions.ClientID%>').hide();
            $('#<%=pnlTopping.ClientID%>').hide();
        }

        function addDealWithSelection() {
            // Make sure that the basket is not empty.
            if (_arrMenuItems == null || _arrMenuItems.length == 0)
                return;

            showInProgressModal();

            var dealId;
            var menuId = "";
            var subMenuId = "";
            var inclToppingIdStr = "";
            var dressingIdStr = "";
            var optionIdStr = "";

            dealId = _arrMenuItems[0].dealId;
            for (var i = 0; i < _arrMenuItems.length; i++) {
                menuId += _arrMenuItems[i].menuId + ";";
                subMenuId += _arrMenuItems[i].subMenuId + ";";
                inclToppingIdStr += "{" + _arrMenuItems[i].toppingIds + "}";
                dressingIdStr += "{" + _arrMenuItems[i].dressingIds + "}";
                optionIdStr += "{" + _arrMenuItems[i].optionIds + "}";
            }

            $.post("/Ajax/AddToShopCart.aspx",
                {
                    DealId: dealId,
                    MenuItemId: menuId,
                    SubMenuItemId: subMenuId,
                    inclToppingIds: inclToppingIdStr,
                    dressingIds: dressingIdStr,
                    optionIds: optionIdStr
                },
                function (result) {
                    if (result == 'error') {
                        alert('There is a error occurred, the menu is not added to the shop cart! Please try again later.');
                        hideInProgressModal();
                    } else {
                        // here was get
                        $.post("/Ajax/SmallShoppingCart.aspx", function (data, status) {
                            if (status == 'success') {
                                //$(".shopping_cart_deals").html(data);
                                $(".shopping_cart").html(data);
                                hideInProgressModal();
                            }
                        });
                    }
                });
        }

        function hideToppingOrDressingModal() {
            $find('<%= mpeToppingModal.ClientID%>').hide();
        }

        function shouldOpenNextExtraItems() {
            $.ajax({
                type: "POST",
                cache:false,
                url: "Ajax/ShoppingService.asmx/GetSecondMenuLinkingInfo",
                data: "{'menuId':'" + _selectedDealMenuId + "', 'dealId':'" + _selectedDealId + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (result) {
                    if (result != null && result.d != "") {
                        if (result.d == "no_second_extra_items") {
                            // Do nothing yet!
                        } else {
                            _isSecondOption = true;
                            _toppingIdsToAdd = new Array();
                            _dressingIdsToAdd = new Array();
                            _optionsIdToAdd = new Array();
                            
                            if ($('#<%=pnlDealOptions.ClientID%>') != null) {
                                $('#<%=pnlDealOptions.ClientID%>').hide();
                            }

                            $find("<%=mpeToppingModal.ClientID%>").set_dynamicContextKey(result.d);
                            $find('<%= mpeToppingModal.ClientID%>').show();
                        }
                    }
                },
                error: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert("Message: " + r.Message);
                    alert("StackTrace: " + r.StackTrace);
                    alert("ExceptionType: " + r.ExceptionType);
                }
            });
        }

        function MarkToppingToAdd(checkbox, toppingId) {
            var pos = -1;
            if (checkbox.checked) {
                pos = _toppingIdsToAdd.indexOf(toppingId);
                if (pos == -1)
                    _toppingIdsToAdd[_toppingIdsToAdd.length] = toppingId;
            } else {
                pos = _toppingIdsToAdd.indexOf(toppingId);
                if (pos != -1)
                    _toppingIdsToAdd.splice(pos, 1);
            }
        }

        function MarkDressingToAdd(checkbox, dressingId) {
            var pos = -1;
            if (checkbox.checked) {
                pos = _dressingIdsToAdd.indexOf(dressingId);
                if (pos == -1)
                    _dressingIdsToAdd[_dressingIdsToAdd.length] = dressingId;
            } else {
                pos = _dressingIdsToAdd.indexOf(dressingId);
                if (pos != -1)
                    _dressingIdsToAdd.splice(pos, 1);
            }
        }

        function MarkOptionToAdd(checkbox, optionId) {
            var pos = -1;
            if (checkbox.checked) {
                pos = _optionsIdToAdd.indexOf(optionId);
                if (pos == -1)
                    _optionsIdToAdd[_optionsIdToAdd.length] = optionId;
            } else {
                pos = _optionsIdToAdd.indexOf(optionId);
                if (pos != -1)
                    _optionsIdToAdd.splice(pos, 1);
            }
        }
    </script>
    <script type="text/javascript">
        // Get the instance of PageRequestManager.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            showInProgressModal();

            // Disable button that caused a postback
            $get(args._postBackElement.id).disabled = true;
        }

        // Called when async postback ends
        function prm_EndRequest(sender, args) {
            hideInProgressModal();

            // Enable button that caused a postback
            $get(sender._postBackSettings.sourceElement.id).disabled = false;
        }

        function showInProgressModal() {
            var modal = $find('<%= InProgressModalPopupExtender.ClientID %>');
            if (modal != null) {
                modal.show();
            }
        }

        function hideInProgressModal() {
            var modal = $find('<%= InProgressModalPopupExtender.ClientID %>');
            if (modal != null) {
                modal.hide();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


            
            <section class="cstmsection dealsSection">
               
                <h3 class="page-header page-title"><i class="fa fa-flag"></i>Deals</h3>

                <div class="right-sidebar">
                    <div class="deals-details">
                         <ul>
                            <asp:Repeater runat="server" ID="rptDealsDetails">
                                <ItemTemplate>
                                    <li>
                                        
                                                <div class="deals-details-item-col1">
                                                    <asp:HiddenField ID="hdnDealDetailId" runat="server" Value='<%# Eval("ID") %>' />
                                                    


                                                    <h3 class="deals-details-title">
                                                        <%# Eval("Name") %>

                                                    </h3>

                                                   <div class="deals-details-price">
                                                                <% If IsDeliveryOrder Then%>
                                                                <%# FormatCurrency(Eval("DeliveryUnitPrice"), 2)%>
                                                                <%Else%>
                                                                <%# FormatCurrency(Eval("CollectionUnitPrice"), 2)%>
                                                                <%End If%>
                                                            </div>

                                                    
                                                   
                                                </div>
                                                <div class="deals-details-item-col2">
                                                                                                        
                                                     <div class="deals-details-item-add-to-cart">
                                                        <asp:ImageButton ID="ImgAdd2Cart" runat="server" ImageUrl="App_Themes/default/imgs/AddToCart.png" OnClientClick="onInitAddingNewItem();" CommandArgument='<%# Eval("ID") %>' />
                                                    </div>

                                                    <div class="menu-item-remarks">
                                                        <label><%# Eval("Remarks") %></label>
                                                    </div>

                                                    <div class="deals-details-promotion"><%# Eval("PromotionText")%></div>

                                                </div>
                                               
                                              
                                           
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>

                
            </section>

            <section class="cstmsection dealBasket">
                
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <asp:PlaceHolder runat="server" ID="phShoppingCart">
                                    <!--
                                    <div class="shopping_cart_deals">
                                    </div>
                                    -->
                                    <div class="shopping_cart">
                                    </div>
                                    <br />
                                </asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Image ID="imgClosedSign" runat="server" ImageAlign="Middle" ImageUrl="Images/closed.gif"
                                    Visible="false" />
                            </td>
                        </tr>

                       
                    </table>
               

                    <div class="paymentIcons">
                        <asp:Image runat="server" SkinID="PaymentImage" BorderColor="0" CssClass="img-responsive" />
                    </div>

            </section>

    
   
    
    
    <div>
        <ajaxToolkit:ModalPopupExtender ID="InProgressModalPopupExtender" runat="server" TargetControlID="UpdateProgress"
            PopupControlID="UpdateProgress" BackgroundCssClass="modal-background">
        </ajaxToolkit:ModalPopupExtender>
        <asp:UpdateProgress ID="UpdateProgress" runat="server">
            <ProgressTemplate>
                <asp:Image ID="Image1" ImageUrl="~/Images/loadingAnim.gif" AlternateText="Processing" runat="server" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <%--Deals Options//--%>
    <asp:HiddenField runat="server" ID="btnShowDealOptions" />

    <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeDealOptions" TargetControlID="btnShowDealOptions" PopupControlID="pnlDealOptions"
        DynamicControlID="pnlDynamicContent" DynamicServicePath="Ajax/ShoppingService.asmx" DynamicServiceMethod="GetDealDetailById"
        BackgroundCssClass="modal-background" OkControlID="btnDealClose" OnOkScript="onDealOptionsClose();" />
   
     <asp:Panel runat="server" ID="pnlDealOptions" CssClass="customPopup">
        <div class="custom-modal-popup dealspop">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                     <div class="popup-title" id="">
                        <div class="TitlebarLeft">
                           <asp:Literal runat="server" ID="ltrDealName"></asp:Literal>
                        </div>                       
                    </div>

                    <div class="popup-body">
                       
                        <div id="deal_selection" class="dealselection"></div>

                     
                        <p class="mealOptP">Please choose your meal options</p>
                       

                        <div class="dealsDropDown">
                           <asp:Panel runat="server" ID="pnlDynamicContent"></asp:Panel>
                        </div>

                    </div>

                    <asp:HiddenField runat="server" ID="hdnModalPopupName" Value="DealsOptions" />
                </ContentTemplate>
            </asp:UpdatePanel>

             <div class="popup_Buttons" style="text-align:center;" >
                <%--<asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="buttonpls dark center" Width="100px" />--%>
                <asp:Button runat="server" ID="btnDealClose" Text="Add To Basket" CssClass="buttonpls okBtn"  />
            </div>
        </div>
    </asp:Panel>

    <asp:HiddenField runat="server" ID="hdnDealSelected" Value="" />
    <asp:HiddenField runat="server" ID="hdnDealOptionsSelected" Value="" />
    <%--//Deals Options--%>

    <asp:HiddenField runat="server" ID="hdnShowToppingModal" />

    <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeToppingModal" TargetControlID="hdnShowToppingModal" PopupControlID="pnlTopping"
        DynamicControlID="pnlDynamicTopping" DynamicServicePath="Ajax/ShoppingService.asmx" DynamicServiceMethod="GetExtraItems"
        BackgroundCssClass="modal-background" OkControlID="btnToppingClose" OnOkScript="OnCloseToppingOrDressingModal();" />
   
     <asp:Panel runat="server" ID="pnlTopping" CssClass="customPopup">

        <div class="custom-modal-popup">

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>

                    <div class="popup-body">
                       
                       
                        <asp:Panel runat="server" ID="pnlDynamicTopping"></asp:Panel>
                        

                    </div>

                    <asp:HiddenField runat="server" ID="hdnSelectedOptionId" Value="0" />
                    <asp:HiddenField runat="server" ID="hdnNextMenuItemWithExtra" Value="" />
                    <asp:HiddenField runat="server" ID="hdnSelectedToppingIds" Value="" />

                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="popup_Buttons" style="text-align:center;" >
                <asp:Button runat="server" ID="btnToppingClose" Text="ADD SELECTION" CssClass="buttonpls okBtn" />
            </div>

        </div>
    </asp:Panel>
</asp:Content>
