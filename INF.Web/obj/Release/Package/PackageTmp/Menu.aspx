<%@ Page Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false"
    Inherits="INF.Web.Menu" CodeBehind="Menu.aspx.vb" %>

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
        If (ConfigurationManager.AppSettings("linkCanonicalMenu") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("linkCanonicalMenu")))) Then
        %>                                
            <link rel="canonical" href="<%=ConfigurationManager.AppSettings("linkCanonicalMenu")%>" />
        <%
        End If
    %>   

     <%
         If (ConfigurationManager.AppSettings("descMenu") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("descMenu")))) Then
        %>                                
            <meta name="description" content="<%=ConfigurationManager.AppSettings("descMenu")%>" />
        <%
        End If
    %>   


</asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="PageTitle">
   <%
       If (ConfigurationManager.AppSettings("titleMenu") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titleMenu")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titleMenu")%>
        <%
        Else
            %> 
                <%= EPATheme.Current.Themes.WebsiteName%>  
            <%
        End If
    %>   
</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="HeadExtra">
    <script type="text/javascript" src="Scripts/ShoppingCart.js"></script>
    <script type='text/javascript'>



        $(function () {
            // LoadSmallShopCart();
            LoadSmallShopCartWithDeals();

        });



        /*
        $(function () {
            var offset = $("#sidebar").offset();
            var topPadding = 10;
            $(window).scroll(function () {
                if ($(window).scrollTop() > offset.top) {
                    $("#sidebar").stop().animate({
                        marginTop: $(window).scrollTop() - offset.top + topPadding
                    });
                } else {
                    $("#sidebar").stop().animate({
                        marginTop: 0
                    });
                }
            });
        });
        */

        /*
        $(function () {
            var offset = $("#category_sidebar").offset();
            var topPadding = 5;

            $(window).scroll(function () {
                var objectHeight = $('#category_sidebar').outerHeight();
                var windowHeight = $(window).height();

                if ($(window).scrollTop() > offset.top && windowHeight > objectHeight) {
                    $("#category_sidebar").stop().animate({
                        marginTop: $(window).scrollTop() - offset.top + topPadding
                    });
                } else {
                    $("#category_sidebar").stop().animate({
                        marginTop: 0
                    });
                }
            });
        });
        */

        function AddToShopCart(menuItemId, subMenuItemId, dressingId) {
            showInProgressModal();
            $.post("/Ajax/AddToShopCart.aspx", { menuItemId: menuItemId, subMenuItemId: subMenuItemId, dressingId: dressingId }, function (result) {
                if (result == 'error') {
                    hideInProgressModal();
                    console.log('There is a error when adding the menu-item to the basket!');
                    alert('There is a error occurred, the menu is not added to the shop cart! Please try again later.');
                }
                else {
                    _curCartId = result;
                    console.log('New CartItemID = ' + _curCartId);
                    /*$.post("/Ajax/SmallShoppingCart.aspx", function (result1) {
                        $(".shopping_cart").html(result1);
                    });*/

                 
                    // here was get
                    $.post("/Ajax/SmallShoppingCart.aspx", function (data, status) {
                        if (status == 'success') {
                            $(".shopping_cart").html(data);
                        }
                    });

                    hideInProgressModal();
                }
                return false;
            });
        }
    </script>

</asp:Content>

<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="MainContent">
    <section class="menuSection">

        <div class="col-md-12 col-xs-12" style="padding-left: 0; padding-right: 0;">
            
            <div class="leftSideContent">

                 <nav class="navbar navbar-default" id="menunavbarMobi">
                    <div class="container-fluid">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#menuMobiNav">
                                <span class="sr-only">Toggle navigation</span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                         
                                <a class="navbar-brand" href="#"><i class="fa fa-cutlery"></i>Menu</a>
                        </div>

                        <div class="collapse navbar-collapse" id="menuMobiNav">
                            <ul class="nav navbar-nav">
                                <asp:Repeater runat="server" ID="MenuCategoryRepeaterMobi">
                                <ItemTemplate>
                                    <li>
                                        <a href="Menu.aspx?CategoryId=<%# Eval("ID") %>">
									        <input type="button" class="menuButtons" id="<%# Eval("Name") %>" value="<%# Eval("Name") %>"/>
                                        </a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </nav>

                <div class="leftCol">
                  
                     <div id="category_sidebar">
                        
                            <div class="menuTitle""><i class="fa fa-cutlery"></i>Menu</div>
                           
                          
                             <ul class="menu-categories">
                                
                                <asp:Repeater runat="server" ID="MenuCategoryRepeater">
                                    <ItemTemplate>
                                        <li>
                                            <a href="Menu.aspx?CategoryId=<%# Eval("ID") %>">
											    <input type="button" class="menuButtons" id="<%# Eval("Name") %>" value="<%# Eval("Name") %>"/>
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                      
                    </div>

                   
                    

                    <%--Menu Category on the Left sidebar//--%>
                </div>

                <div class="middleCol">
                    <%--//Menu Category on the Left sidebar--%>
                       
          

                        <asp:Repeater runat="server" ID="rptMenus">
                            <ItemTemplate>

                                <asp:UpdatePanel runat="server" ID="upMenuItem" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="whiteContainer">

                                            <div class="prodFirstRow">
                                               
                                                <div class="prodName">
                                                    <asp:Label ID="ItemNameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                                    <asp:HiddenField ID="hdMenuId" runat="server" Value='<%# Eval("ID") %>' />
                                                </div>

                                                 <div class="prodPrice">
                                                    <div class="prodActualPrice">
                                                        <% If IsDeliveryOrder Then%>
                                                        <asp:Label ID="lblDeliveryPrice" runat="server" Text='<%# FormatCurrency(Eval("DeliveryPrice"), 2)%>'/>
                                                        <% Else%>
                                                        <asp:Label ID="lblCollectionPrice" runat="server" Text='<%# FormatCurrency(Eval("CollectionPrice"), 2)%>' />
                                                        <%End If%>
                                                    </div>
                                                </div>   
                                            </div>



                                            <div class="prodSecondRow">

                                                <div class="prodDesc">
                                                    
                                                   
                                                     <div class="menu-item-options">
                                                        <asp:DropDownList ID="SubMenuDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SubMenuDropDownList_Changed" CssClass="form-control" Style="min-width: 100px; width: 50%;"></asp:DropDownList>
                                                        <asp:DropDownList ID="BaseSelectionDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="BaseSelectionDropDownList_Changed" CssClass="form-control" Style="min-width: 100px; width: 50%;"></asp:DropDownList>
                                                    </div>

                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Remarks") %>' CssClass="menu-item-remarks"/>

                                                </div>

                                                <div class="Add2CartButton" style="margin-right: 0;">
                                                    <asp:ImageButton ID="ImgAdd2Cart" runat="server" ImageUrl="App_Themes/default/imgs/AddToCart.png" CommandName="Select" />
                                                </div>

                                                <div class="prodPromo">
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("PromotionText") %>' />
                                                </div>

                                            </div>


                                            <%            If IsDeliveryOrder Then%>
                                            <asp:HiddenField ID="PriceHiddenField" runat="server" Value='<%# Eval("DeliveryPrice") %>' />
                                            <% Else%>
                                            <asp:HiddenField ID="CollectionPriceHiddenField" runat="server" Value='<%# Eval("CollectionPrice") %>' />
                                            <%End If%>
                                            <asp:HiddenField ID="hdHasBase" runat="server" Value='<%# Eval("HasBase") %>' />
                                            <asp:HiddenField ID="hdHasTopping" runat="server" Value='<%# Eval("HasTopping") %>' />
                                            <asp:HiddenField ID="hdHasDressing" runat="server" Value='<%# Eval("HasDressing") %>' />
                                            <asp:HiddenField ID="hdToppingPrice" runat="server" Value='<%# Eval("ToppingPrice") %>' />
                                            <asp:HiddenField runat="server" ID="hdnOptionID1" Value='<%# Eval("OptionId1") %>' />
                                            <asp:HiddenField runat="server" ID="hdnOptionID2" Value='<%# Eval("OptionId2") %>' />
                                            
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="SubMenuDropDownList" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="BaseSelectionDropDownList" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

             
                <%--//Menu Items--%>
            </div>
           
             <div class="rightSideContent">
                <%--Basket//--%>
                <div id="sidebar" class="" style="padding-left: 0;">

                   
                        <asp:PlaceHolder runat="server" ID="phShoppingCart">
                            <div class="shopping_cart">
                            </div>
                        </asp:PlaceHolder>
                   

                
                    <asp:Image ID="imgClosedSign" runat="server" SkinID="ShopClosed" Visible="false"  />
                   
                    
                    <div class="paymentIcons">
                        <asp:Image runat="server" SkinID="PaymentImage" BorderColor="0" CssClass="img-responsive" />
                    </div>

                </div>
                <%--//Basket--%>
            </div>
        </div>
        
        
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


        <%--Dressing Popup//--%>
        <div>
            <a href="#" style="display: none; visibility: hidden;" onclick="return false" id="PopupDressingLink" runat="server">Popup Dressing Link</a>

            <ajaxToolkit:ModalPopupExtender ID="DressingModalPopupExtender" runat="server" CancelControlID="ButtonDeleteCancel"
                OkControlID="ButtonDeleleOkay" TargetControlID="PopupDressingLink" PopupControlID="DressingPanel"
                PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modal-background">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel runat="server" ID="DressingPanel" Style="display: none;" CssClass="customPopup">
                <div class="custom-modal-popup">
                    <div class="popup-title" id="PopupHeader">
                        <div class="TitlebarLeft">
                           Please choose dressing
                        </div>                       
                    </div>

                    <div class="popup-body">
                       
                            <asp:DataList ID="DLDressing" DataSourceID="SDSDressing" runat="server" BackColor="#ffffff"
                                BorderColor="#FFFEB2" BorderStyle="None" BorderWidth="0" CellPadding="3" CellSpacing="2"
                                Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Font-Size="13px" GridLines="None" Height="14px" HorizontalAlign="Center"
                                RepeatColumns="3" RepeatDirection="Vertical" Width="100%">
                                <ItemStyle CssClass="list-dressing-item"></ItemStyle>
                                <ItemTemplate>
                                    <div class="checkbox">
                                        <label style="font-weight: bold;">
                                            <input type="checkbox" onchange="MarkDressingIdToAdd('<%# Eval("ID") %>', this);" /><%# Eval("Name") %>     
                                        </label>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                       
                        <asp:SqlDataSource ID="SDSDressing" runat="server" ConnectionString="<%$ ConnectionStrings:PizzaWebConnectionString %>"
                            SelectCommand="GetDressings" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                    </div>

                    <div class="popup_Buttons" style="text-align:center;" >
                        <asp:Button ID="ButtonDeleleOkay" Text="ADD TO BASKET" runat="server" class="okBtn"  OnClientClick="AddMenuItemWithDressing();" />
                        <input type="button" id="ButtonDeleteCancel" value="Cancel" class="cancelBtn" style="width: 100px;" />
                    </div>

                </div>
            </asp:Panel>
        </div>

        <%--//Dressing Popup--%>


        <%--Topping Popup//--%>
        <div>
            <a href="#" style="display: none; visibility: hidden;" onclick="return false"
                id="lnkExtraTopping" runat="server">Customize Pizza Topping</a>

            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderTopping" runat="server" CancelControlID="btnCancelTopping"
                OkControlID="btnOKTopping" TargetControlID="lnkExtraTopping" PopupControlID="pnlTopping"
                PopupDragHandleControlID="divToppingTitleBar" Drag="true" BackgroundCssClass="ModalPopupBG">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel runat="server" ID="pnlTopping" Style="display: none;" CssClass="customPopup">
                <div class="custom-modal-popup">
                    <div class="popup-title" id="divToppingTitleBar">
                        <div class="TitlebarLeft">
                           Customize Pizza Topping
                        </div>                      
                    </div>

                    <div class="popup-body">
                       


                            <div class="toppingDecision">
                                <h3>Custom Topping ? </h3>

                                <div class="yesToTopping">
                                    <asp:RadioButton runat="server" ID="chkYesTopping" Checked="True" Text="Yes" GroupName="ToppingOptions"
                                            onclick="CustomizeToppingChanged('yes');" />
                                        <ajaxToolkit:ToggleButtonExtender runat="server" ID="tbeYesTopping" TargetControlID="chkYesTopping"
                                            ImageHeight="20" ImageWidth="20" CheckedImageUrl="images/checked.png" UncheckedImageUrl="images/unchecked.png">
                                        </ajaxToolkit:ToggleButtonExtender>
                                        <ajaxToolkit:MutuallyExclusiveCheckBoxExtender runat="server" ID="MutuallyExclusiveCheckBoxExtender1"
                                            TargetControlID="chkYesTopping" Key="ToppingOptions">
                                        </ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
                                </div>

                                <div class="noToTopping">
                                    <asp:RadioButton runat="server" ID="chkNoTopping" Text="No" GroupName="ToppingOptions"
                                            onclick="CustomizeToppingChanged('no')" />
                                    <ajaxToolkit:ToggleButtonExtender runat="server" ID="tbeNoTopping" TargetControlID="chkNoTopping"
                                        ImageHeight="20" ImageWidth="20" CheckedImageUrl="images/checked.png" UncheckedImageUrl="images/unchecked.png">
                                    </ajaxToolkit:ToggleButtonExtender>
                                    <ajaxToolkit:MutuallyExclusiveCheckBoxExtender runat="server" ID="MutuallyExclusiveCheckBoxExtender2"
                                        TargetControlID="chkNoTopping" Key="ToppingOptions">
                                    </ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
                                </div>
                            </div>

                            <div class="toppingPosition">
                                <h3>Topping for the pizza side:</h3>
                                <div class="topPosLeft">
                                    <asp:RadioButton runat="server" ID="chkLeftTopping" Text="Left Side" GroupName="ToppingOptions"
                                            onclick="CustomizeToppingChanged('left')" />
                                    <ajaxToolkit:ToggleButtonExtender runat="server" ID="tbeLeftTopping" TargetControlID="chkLeftTopping"
                                        ImageHeight="20" ImageWidth="20" CheckedImageUrl="images/checked.png" UncheckedImageUrl="images/unchecked.png">
                                    </ajaxToolkit:ToggleButtonExtender>
                                    <ajaxToolkit:MutuallyExclusiveCheckBoxExtender runat="server" ID="MutuallyExclusiveCheckBoxExtender3"
                                        TargetControlID="chkLeftTopping" Key="ToppingOptions">
                                    </ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
                                 </div>
                                <div class="topPosRight">
                                    <asp:RadioButton runat="server" ID="chkRightTopping" Text="Right Side" GroupName="ToppingOptions"
                                            onclick="CustomizeToppingChanged('right')" />
                                        <ajaxToolkit:ToggleButtonExtender runat="server" ID="tbeRightTopping" TargetControlID="chkRightTopping"
                                            ImageHeight="20" ImageWidth="20" CheckedImageUrl="images/checked.png" UncheckedImageUrl="images/unchecked.png">
                                        </ajaxToolkit:ToggleButtonExtender>
                                        <ajaxToolkit:MutuallyExclusiveCheckBoxExtender runat="server" ID="MutuallyExclusiveCheckBoxExtender4"
                                            TargetControlID="chkRightTopping" Key="ToppingOptions">
                                        </ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
                                 </div>
                            </div>
                       

                        <asp:DataList ID="dlToppings" runat="server" CssClass="toppingTable"
                            BorderWidth="0" CellPadding="2" CellSpacing="2"                             
                            Font-Size="13px" GridLines="None" HorizontalAlign="Center" RepeatColumns="3"
                            RepeatDirection="Vertical" Width="100%">

                            <ItemStyle CssClass="list-topping-item"></ItemStyle>
                            <ItemTemplate>
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" id="tpp_<%# Eval("ID") %>" onchange="MarkUpToppingIdAndPosition('<%# Eval("ID") %>',this);" /><%# Eval("Name")%>
                                    </label>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                   
                     <div class="popup_Buttons" style="text-align:center;">
                        <asp:Button ID="btnOKTopping" Text="Add to Basket" runat="server" class="okBtn"
                            OnClientClick="AddToppingToTheItemInBasketNew();" />
                        <asp:Button runat="server" ID="btnCancelTopping" class="cancelBtn" Text="Cancel"></asp:Button>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <%--//Topping Popup--%>
        <%--Option Popups--%>
        <asp:PlaceHolder runat="server" ID="phOptionPopups"></asp:PlaceHolder>
        <%--//Option Popups--%>
    </section>
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
