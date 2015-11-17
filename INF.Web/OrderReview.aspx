<%@ Page Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false" Inherits="INF.Web.OrderReview" CodeBehind="OrderReview.aspx.vb" %>

<%@ Import Namespace="INF.Web.UI.Settings" %>

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
        If (ConfigurationManager.AppSettings("linkCanonicalOrderReview") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("linkCanonicalOrderReview")))) Then
        %>                                
            <link rel="canonical" href="<%=ConfigurationManager.AppSettings("linkCanonicalOrderReview")%>" />
        <%
        End If
    %>   

    <%
        If (ConfigurationManager.AppSettings("descOrderReview") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("descOrderReview")))) Then
        %>                                
            <meta name="description" content="<%=ConfigurationManager.AppSettings("descOrderReview")%>" />
        <%
        End If
    %>  

</asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="PageTitle">
  
     <%
         If (ConfigurationManager.AppSettings("titleOrderReview") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titleOrderReview")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titleOrderReview")%>
        <%
        Else
            %> 
                <%= EPATheme.Current.Themes.WebsiteName%> - Order Review
            <%
        End If
    %>   



</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="HeadExtra">
    <script language="javascript" type="text/javascript" src="Scripts/ShoppingCart.js"></script>
    
    <!--
    <script src="Scripts/jquery-ui.js" type="text/javascript"></script>
    -->
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            <%--
            var previousUri = '<%= Request.UrlReferrer.AbsoluteUri%>';
            console.log("UrlReferrer = " + previousUri);
            var previousPage = previousUri.substring(previousUri.lastIndexOf('/') + 1).toLowerCase();
            console.log("PreviousPage = " + previousPage);

            if (previousPage == 'deals.aspx') {
                LoadLargeShoppingCartWithDeals();
            } else {
                LoadLargeShoppingCart();
            } 
            --%>
            //LoadLargeShoppingCart();
            LoadLargeShoppingCartWithDeals();
        });

        

        function BlockUI(elementID) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () {
                $("#" + elementID).block({
                    message: '<table align = "center"><tr><td>' +
                        '<img src="Images/loadingAnim.gif"/></td></tr></table>',
                    css: {},
                    overlayCSS: {
                        backgroundColor: '#000000', opacity: 0.6
                    }
                });
            });
            prm.add_endRequest(function () {
                $("#" + elementID).unblock();
            });
        }

        function onCollectionCheckedChanged(ctrl) {
            var extender = $find('ctl00_MainContent_cpeCustomerAddresses');
            if (extender != null) {
                if (ctrl.checked) {
                    extender._doClose();
                } else {
                    extender._doOpen();
                }
            }
        }

        function onDeliveryCheckedChanged(ctrl) {
            var extender = $find('ctl00_MainContent_cpeCustomerAddresses');
            if (extender != null) {
                if (ctrl.checked) {
                    extender._doOpen();
                } else {
                    extender._doClose();
                }
            }
        }

        function toggleTimingOption(ctrl) {
            var hours = document.getElementById('<%=ddlAtHours.ClientID%>');
            var minutes = document.getElementById('<%=ddlAtMinutes.ClientID%>');
            var tt = document.getElementById('<%=ddlAmOrPm.ClientID%>');
            if (ctrl.id =='ctl00_MainContent_radAsSoonAsPossible' && ctrl.checked) {
                hours.disabled = true;
                minutes.disabled = true;
                tt.disabled = true;
            } else {
                if (hours) {
                    hours.disabled = false;
                    minutes.disabled = false;
                    tt.disabled = false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="MainContent">

    <section class="orderReview cstmsection">

        <div id="dialog-message" title="Opps!" style="display: none; text-align: justify; font-size: 12px; clear: both;">
            <p style="color: red;">
                <span class="ui-icon ui-icon-circle-check" style="float: left; margin: 0 7px 30px 0;"></span>Opps! You have not input any address to delivery the order to.
            </p>
        </div>

        <div class="order-review-wrapper">
            <div class="order-review-header">
                <h3 class="ordrevHeader">
                    <i class="fa fa-list-alt"></i>Order Review
                </h3>
            </div>

            

            <asp:Panel runat="server" ID="divOrderSummary" CssClass="orderSumm">

                    <div class="order-review-shopping-cart"></div>
                              
                    <div class="ovbVoucher">
                        Voucher Code:
                        <asp:TextBox runat="server" ID="VoucherCodeTextBox" Width="80px"></asp:TextBox>
                    </div>

                    <div class="ovbBtmPart">
                        <p>Additional Instructions</p>
                        <div class="ovbTxtbxCont">
                            <asp:TextBox ID="txtAdditionalInstructions" runat="server" Width="100%" Height="68px"
                                TextMode="MultiLine"></asp:TextBox>
                        </div>

                         <div class="editBtnCont">                              
                            <%--<asp:ImageButton runat="server" ID="EditOrderButton" AlternateText="Edit Order" CssClass="btn flat-button"/>--%>
                            <asp:Button ID="EditOrderButton2" runat="server" Text="Edit Order" CssClass="btn flat-button" />
                   
                            
                        </div>

                    </div>
            </asp:Panel>
                        
            <asp:Panel runat="server" ID="divOrderDetail" CssClass="orderFinal">

                <div class="order-detail-wrapper">

                       <h3 class="orderDetHeader">
                            Info & Payment
                            <i class="fa fa-flip-horizontal fa-truck"></i>
                        </h3>

                        <fieldset id="fs_ordertypes">
                            <div id="checkout_div_ordertypes">
                                <div class="ordrTypeText">Order Type:</div>

                                <div class="ordrTypeVal">
                                    <% If IsDeliveryOrder Then%>
                                    <asp:RadioButton runat="server" ID="chkDeliveryOrder" Text="Delivery" GroupName="OrderTypes"
                                        BorderStyle="None" CssClass="check-delivery-order-type" onclick="onDeliveryCheckedChanged(this);" />
                                    <ajaxToolkit:ToggleButtonExtender runat="server" ID="tbeDeliveryOrder" TargetControlID="chkDeliveryOrder"
                                        ImageHeight="20" ImageWidth="20" CheckedImageUrl="images/checked.png" UncheckedImageUrl="images/unchecked.png">
                                    </ajaxToolkit:ToggleButtonExtender>
                                    <% Else%>
                                    <asp:RadioButton runat="server" ID="chkCollectionOrder" Text="Collection" GroupName="OrderTypes"
                                        BorderStyle="None" CssClass="check-collection-order-type" onclick="onCollectionCheckedChanged(this);" />
                                    <ajaxToolkit:ToggleButtonExtender runat="server" ID="tbeCollectionOrder" TargetControlID="chkCollectionOrder"
                                        ImageHeight="20" ImageWidth="20" CheckedImageUrl="images/checked.png" UncheckedImageUrl="images/unchecked.png">
                                    </ajaxToolkit:ToggleButtonExtender>
                                    <%End If%>                           
                                </div>

                            </div>
                        </fieldset>

                   
                    
                    <div class="accountInfo">

                         <table id="tbl_customer_info">
                            <tr>
                                <th style="width: 25%;">
                                    <label>
                                        Phone Number:</label>
                                </th>
                                <td style="width: 25%;">
                                    <asp:TextBox runat="server" ID="txtPhoneNumber" Width="100%" ReadOnly="True" Enabled="False"></asp:TextBox>
                                </td>
                                <th style="width: 25%; text-align: right; padding-right: 5px;">
                                    <label>
                                        Alt. Number:</label>
                                </th>
                                <td style="width: 25%;">
                                    <asp:TextBox runat="server" ID="txtAltPhoneNumber" Width="100%" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 25%;">
                                    <label>
                                        First Name:</label>
                                </th>
                                <td style="width: 25%;">
                                    <asp:TextBox runat="server" ID="txtFistName" Width="100%" ReadOnly="True" Enabled="False"></asp:TextBox>
                                </td>
                                <th style="width: 25%; text-align: right; padding-right: 5px;">
                                    <label>
                                        Last Name:</label>
                                </th>
                                <td style="width: 25%;">
                                    <asp:TextBox runat="server" ID="txtLastName" Width="100%" ReadOnly="True" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 25%;">
                                    <label>
                                        Email Address:</label>
                                </th>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="txtEmailAddress" Width="100%" ReadOnly="True" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                        </table>

               

                   <% If IsDeliveryOrder Then%>
                  
                          <%--Delivery to Address//--%>
                        <ajaxToolkit:CollapsiblePanelExtender runat="server" ID="cpeCustomerAddresses" TargetControlID="pnlCustomerAddresses">
                        </ajaxToolkit:CollapsiblePanelExtender>
                        <asp:Panel runat="server" ID="pnlCustomerAddresses">
                            <fieldset id="fs_customer_address">
                                <h3>Please select an address from below:</h3>
                                
                                <asp:UpdatePanel runat="server" ID="upnlAddressList" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <table class="tbl-existing-addresses" cellpadding="0" cellspacing="0">
                                            <asp:Repeater runat="server" ID="dlCustomerAddress" OnItemCommand="dlCustomerAddress_OnItemCommand">
                                                <ItemTemplate>
                                                    <tr>

                                                        <td class="addrChk">
                                                            <asp:CheckBox runat="server" ID="cbAddressId" GroupName="ExistingAddresses" />

                                                            <ajaxToolkit:ToggleButtonExtender runat="server" ID="tbeCollectionOrder" TargetControlID="cbAddressId"
                                                                ImageHeight="20" ImageWidth="20" CheckedImageUrl="images/checked_16.png" UncheckedImageUrl="images/unchecked_16.png">
                                                            </ajaxToolkit:ToggleButtonExtender>

                                                            <ajaxToolkit:MutuallyExclusiveCheckBoxExtender runat="server" ID="mecbeAddressId"
                                                                TargetControlID="cbAddressId" Key="ExistingAddresses">
                                                            </ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
                                                            <asp:HiddenField runat="server" ID="hfAddressId" Value='<%# Eval("ID")%>'  />
                                                        </td>
                                                        <td class="addrLbl">
                                                            <label class="addrLabel">
                                                                Address:
                                                            </label>
                                                            <label class="realAddrLabel"> 
                                                                <%# Eval("Address")%></label>
                                                            <br />
                                                            <label class="addrLabel">
                                                                Postcode:
                                                            </label>
                                                            <label class="realPostLabel"> 
                                                                <%# Eval("PostCode")%></label>
                                                            <br />
                                                            <label class="addrLabel">
                                                                City:
                                                            </label>
                                                            <label class="realCityLabel"> 
                                                                <%# Eval("City")%></label>
                                                        </td>
                                                        <td class="addrRmv">
                                                            <asp:ImageButton runat="server" ID="ibDeleteAddress" AlternateText="Delete" ImageUrl="App_Themes/default/Images/Del.png"
                                                                CommandName="DeleteAddress" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <tr class="addNewAddr">
                                                <td colspan="3">
                                                     <asp:LinkButton runat="server" ID="btnNewAddress" Text="Add a New Address" OnClick="OnNewAddress" CssClass="btn addNewAddrBtn" />    
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnNewAddress" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                               
                                <%--Update Address Panel-Modal//--%>


                                <asp:Panel runat="server" ID="pnlUpdateAddress" CssClass="customPopup transparent_class">
                                   <div class="custom-modal-popup">
                                     <div class="popup-title" id="divToppingTitleBar">
                                        <div class="TitlebarLeft">
                                           New Address
                                        </div>                      
                                    </div>

                                    <div class="popup-body">
                                        <fieldset>

                                           
                                            <div id="address_validation_summary">
                                                <asp:ValidationSummary runat="server" ID="vsCustomerAddress" ValidationGroup="CustomerAddress"
                                                    EnableClientScript="True" ShowSummary="True" CssClass="address-validation-summary"
                                                    EnableViewState="False" />
                                            </div>

                                            <div>
                                                <table id="tbl_customer_address">
                                                    <tr>
                                                        <th style="width: 25%;">
                                                            <label>
                                                                Address:</label>&nbsp;<span style="color: red;">(*)</span>
                                                        </th>
                                                        <td colspan="3">
                                                            <asp:TextBox runat="server" ID="txtAddressLine1" Width="100%" ValidationGroup="CustomerAddress"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvAddressLine" ControlToValidate="txtAddressLine1"
                                                                ErrorMessage="[Address] is required." Display="None" SetFocusOnError="True" ValidationGroup="CustomerAddress"
                                                                EnableClientScript="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 25%;">
                                                            <label>
                                                                &nbsp;</label>
                                                        </th>
                                                        <td colspan="3">
                                                            <asp:TextBox runat="server" ID="txtAddressLine2" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 25%;">
                                                            <label>
                                                                Postcode:</label>&nbsp;<span style="color: red;">(*)</span>
                                                        </th>
                                                        <td style="width: 25%;">
                                                            <asp:TextBox runat="server" ID="txtPostcode" Width="100%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvPostcode" ControlToValidate="txtPostcode"
                                                                ErrorMessage="[Postcode] is required." Display="None" SetFocusOnError="True"
                                                                ValidationGroup="CustomerAddress" EnableClientScript="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <th style="width: 25%; text-align: right; padding-right: 5px;">
                                                            <label>
                                                                City:</label>&nbsp;<span style="color: red;">(*)</span>
                                                        </th>
                                                        <td style="width: 25%;">
                                                            <asp:TextBox runat="server" ID="txtCity" Width="100%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvCity" ControlToValidate="txtCity"
                                                                ErrorMessage="[City] is required." Display="None" SetFocusOnError="True" ValidationGroup="CustomerAddress"
                                                                EnableClientScript="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td style="padding: 3px 7px;" colspan="3">
                                                          
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>

                                        </fieldset>
                                    </div>

                                        <div class="popup_Buttons" style="text-align:center;">
                                             <asp:Button runat="server" ID="btnSave" Text="ADD ADDRESS" CssClass="btn okBtn"  OnClick="OnSaveAddress"
                                            ValidationGroup="CustomerAddress" />&nbsp;
                                            <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn cancelBtn" Style="width: 100px;" OnClientClick="return hidePopup();" />
                                        </div>

                                    </div>
                                </asp:Panel>

                                <asp:LinkButton runat="server" ID="lnkFake"></asp:LinkButton>
                               
                                <ajaxToolkit:ModalPopupExtender runat="server" ID="mpeUpdateAddress" 
                                    PopupDragHandleControlID="pnlModalPopupHeader" Drag="True" PopupControlID="pnlUpdateAddress"
                                    TargetControlID="lnkFake" >
                                </ajaxToolkit:ModalPopupExtender>
                            </fieldset>
                        </asp:Panel>
                        <%--//Delivery to Address--%>
                        
                    </div>

                    <%--Asking for timing//--%>
                    <fieldset class="whatTimeFieldset">
                        <h3>What time do you expect to get the order?</h3>
                        <div class="whatTimeCont">
                            <div class="firstOpt">
                               
                                    <asp:RadioButton runat="server" ID="radAsSoonAsPossible" Text=" As Soon As Possible" GroupName="DeliveryTiming" onclick="toggleTimingOption(this);"/>
                                
                                
                            </div>
                            <div class="secondOpt">
                               
                                <asp:RadioButton runat="server" ID="radAtTime" Text=" At the Time :" GroupName="DeliveryTiming" onclick="toggleTimingOption(this);"/>
                                <br/>
                                <div class="secondoptDrop">
                                    <asp:DropDownList runat="server" ID="ddlAtHours" style="width:50px; padding:3px 5px;text-align: right;"/>&nbsp;:&nbsp;
                                    <asp:DropDownList runat="server" ID="ddlAtMinutes" style="width:50px; padding:3px 5px;text-align: right;"/>&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="ddlAmOrPm" style="width:50px; padding:3px 5px;text-align: center;"/>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <%--//Asking for timing--%>
                    <%End If%>


                  
                   <%--Payment method--%>
                    <fieldset id="fs_payment_method">
                        <h3>Payment method</h3>
                        <div id="checkout_div_payment_methods">
                            <table style="width: 100%; text-align: left;">
                                <tr>
                                    <td style="width: 50%;" class="paymentTdFirst">

                                        <div class="paymCont">
                                            <asp:RadioButton ID="chkPayByCash" runat="server" Text="Payment by Cash" GroupName="PaymentMethods"
                                                Checked="true" ForeColor="Black" BorderStyle="None" />
                                      

                                            <span style="font-size: 13px; color: #666666;">( Pay The Driver at your Door )</span>

                                      

                                            <ajaxToolkit:ToggleButtonExtender runat="server" ID="ToggleButtonExtender1" TargetControlID="chkPayByCash"
                                                ImageHeight="38" ImageWidth="48" CheckedImageUrl="App_Themes/default/imgs/cash_48_checked.png"
                                                UncheckedImageUrl="App_Themes/default/imgs/cash_48.png">
                                            </ajaxToolkit:ToggleButtonExtender>
                                        </div>
                                    </td>

                                    <td style="width: 50%;" class="paymentTdSecond">
                                         <div class="paymCont">

                                            <asp:RadioButton ID="chkPayByCard" runat="server" Text="Payment by Card" GroupName="PaymentMethods"
                                                ForeColor="Black" BorderStyle="None" />
                                           
                                              <asp:Label ID="lblCardCharges" runat="server"></asp:Label>

                                            <span style="color: #666666;">( Pay by Your Credit/Debit Card )</span>
                                          
                                           

                                            <ajaxToolkit:ToggleButtonExtender runat="server" ID="ToggleButtonExtender2" TargetControlID="chkPayByCard"
                                                ImageHeight="38" ImageWidth="48" CheckedImageUrl="App_Themes/default/imgs/credit_card_48_checked.png"
                                                UncheckedImageUrl="App_Themes/default/imgs/credit_card_48.png">
                                            </ajaxToolkit:ToggleButtonExtender>


                                        </div>
                                    </td>
                                </tr>
                            </table>

                            <div class="billdingAddrCont" style="display:none;">
                                <div class="billTitle">Billing Address</div>
                                <div class="bFNLabel">
                                    <label>First Name</label>
                                    <asp:TextBox runat="server" ID="billFirstName" Enabled="True"></asp:TextBox>
                                </div>
                                <div class="bLNLabel">
                                    <label>Last Name</label>
                                    <asp:TextBox runat="server" ID="billLastName" Enabled="True"></asp:TextBox>
                                </div>

                                <div class="bALabel">
                                    <label>Address</label>
                                    <asp:TextBox runat="server" ID="billAddress" Enabled="True"></asp:TextBox>
                                </div>

                                <div class="bCLabel">
                                    <label>City</label>
                                    <asp:TextBox runat="server" ID="billCity" Enabled="True"></asp:TextBox>
                                </div>

                                <div class="bPCLabel">
                                    <label>Post Code</label>
                                    <asp:TextBox runat="server" ID="billPostode" Enabled="True"></asp:TextBox>
                                </div>

                                <div class="bCoLabel">
                                    <label>Country</label>
                                    <asp:TextBox runat="server" ID="billCountry" Enabled="false" Value="GB"></asp:TextBox>
                                </div>                                
                               
                            </div>

                            <ajaxToolkit:MutuallyExclusiveCheckBoxExtender runat="server" ID="MutuallyExclusiveCheckBoxExtender1"
                                TargetControlID="chkPayByCash" Key="PaymentMethod">
                            </ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
                            <ajaxToolkit:MutuallyExclusiveCheckBoxExtender runat="server" ID="MutuallyExclusiveCheckBoxExtender2"
                                TargetControlID="chkPayByCard" Key="PaymentMethod">
                            </ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
                        </div>
                    </fieldset>
                    <%--//Payment method--%>
                   
                    <div class="confirmBtnCont">
                        <%--<asp:Button ID="btnConfirmOrder" runat="server" Text="Confirm Order"
                            CssClass="btn flat-button"  OnClick="ConfirmOrder" onmouseover="return hidestatus()" />--%>
                        
                        <p class="selectBillingAddr" style="color:#cc0000;display:none;">You can change the Billing Address on Payment Page.</p>

                        <input type="button" class="fakeSendOrder" value="Confirm Order"/>

                       
                    </div>

                    <asp:Button ID="btnConfirmOrder2" runat="server" Text="Confirm Order" CssClass="btn flat-button hidebtn" OnClick="ConfirmOrder" onmouseover="return hidestatus()"/>
                   
                     <script type="text/javascript">
                         $(document).ready(function () {

                            
                             var iscardpayment = 0;

                             function fillBillingContainer()
                             {
                                 /*
                                 var findFirstChecked = '';
                                 var findFirst = 0;
                                 $(document).find('.addrChk > span input').each(function () {
                                     if (findFirst == 0)
                                     {
                                         if ($(this).prop("checked")) {
                                             findFirstChecked = $(this).parent().parent().parent();
                                             findFirst = 1;
                                         }
                                     }
                                 });
                                 */


                                 var firstName = $('#ctl00_MainContent_txtFistName').val();
                                 var lastName = $('#ctl00_MainContent_txtLastName').val();

                                 var addressLine = $(document).find('.realAddrLabel').html();
                                 addressLine = $.trim(addressLine);

                                 var cityLine = $(document).find('.realCityLabel').html();
                                 cityLine = $.trim(cityLine);

                                 var postCodeLine = $(document).find('.realPostLabel').html();
                                 postCodeLine = $.trim(postCodeLine);

                                 $('.bFNLabel input').val(firstName);
                                 $('.bLNLabel input').val(lastName);
                                 $('.bALabel input').val(addressLine);
                                 $('.bCLabel input').val(cityLine);
                                 $('.bPCLabel input').val(postCodeLine);

                             }


                             if ($('.addrChk').length > 0) {
                                 fillBillingContainer();
                             }
                             

                            $('#ctl00_MainContent_chkPayByCard').click(function () {
                                
                                if($(this).prop("checked"))
                                {

                                    //enableBilling
                                    <%
                                     If (ConfigurationManager.AppSettings("enableBilling") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("enableBilling")))) Then
                                     %>                                
                                    $('.billdingAddrCont').slideDown();
                                    iscardpayment = 1;
                                     <%
                                     Else
                                     %>
                                    $('.selectBillingAddr').slideDown();
                                    iscardpayment = 0;
                                     <%
                                     End If%>

                                   // $('.selectBillingAddr').slideDown();
                                    //$('.billdingAddrCont').slideDown();
                                    //iscardpayment = 1;

                                }
                                else {
                                    //$('.selectBillingAddr').slideUp();
                                    //$('.billdingAddrCont').slideUp();
                                    //iscardpayment = 0;

                                       <%
                                     If (ConfigurationManager.AppSettings("enableBilling") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("enableBilling")))) Then
                                     %>
                                    $('.billdingAddrCont').slideUp();
                                    iscardpayment = 0;
                                    <%
                                     Else
                                     %>
                                    $('.selectBillingAddr').slideUp();
                                    iscardpayment = 0;
                                    <%
                                     End If%>


                                }
                                
                            });

                            $('#ctl00_MainContent_chkPayByCash').click(function () {

                                
                                if ($(this).prop("checked")) {
                                    //$('.selectBillingAddr').slideUp();

                                     <%
                                     If (ConfigurationManager.AppSettings("enableBilling") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("enableBilling")))) Then
                                     %>
                                    $('.billdingAddrCont').slideUp();
                                    iscardpayment = 0;
                                    <%
                                     Else
                                     %>
                                    $('.selectBillingAddr').slideUp();
                                    iscardpayment = 0;
                                    <%
                                     End If%>


                                   // $('.billdingAddrCont').slideUp();
                                   // iscardpayment = 0;
                                }
                                else {
                                    // $('.selectBillingAddr').slideDown();

                                     <%
                                     If (ConfigurationManager.AppSettings("enableBilling") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("enableBilling")))) Then
                                     %>
                                    $('.billdingAddrCont').slideDown();
                                    iscardpayment = 1;
                                    <%
                                     Else
                                     %>
                                    $('.selectBillingAddr').slideDown();
                                    iscardpayment = 0;
                                    <%
                                     End If%>

                                   // $('.billdingAddrCont').slideDown();
                                   // iscardpayment = 1;
                                }
                                
                            });

                            $('.fakeSendOrder').click(function () {
                                if(iscardpayment == 1)
                                {
                                    var tempname = $('.bFNLabel input').val();
                                    var templastname = $('.bLNLabel input').val();
                                    var addressname = $('.bALabel input').val();
                                    var cityname = $('.bCLabel input').val();
                                    var postcode = $('.bPCLabel input').val();
                                    var countryname = $('.bCoLabel input').val();

                                   // $('.bALabel input').text(addressname);

                                    if (tempname.length > 0 && templastname.length > 0 && addressname.length > 0 && cityname.length > 0 && postcode.length > 0) {
                                        

                                        $("#ctl00_MainContent_btnConfirmOrder2").click();
                                    }
                                    else {
                                        alert("Please fill in all the fields of Billing Address !");
                                    }
                                }
                                else {
                                    $("#ctl00_MainContent_btnConfirmOrder2").click();
                                }
                            });

                            if (!document.getElementById("fs_customer_address")) {
                                
                            }
                            else
                            {
                                $('#fs_customer_address').find(':checkbox').first().prop('checked', true);
                            }
                            

                            $('.addNewAddrBtn').click(function () {
                                $('#ctl00_MainContent_pnlUpdateAddress').removeClass("transparent_class");
                            });

                            


                           
                        });
                    </script>
                </div>
            </asp:Panel>
            
        </div>
    </section>
</asp:Content>
