// Dressing IDs will be added to the menu
var _arrDressingID = new Array();
var _curMenuItemId = 0;
var _curSubMenuItemId = 0;

var _selMenuId = 0;
var _selSubMenuId = 0;
var _selToppingPrice = "";
var _selItemName = "";
var _selItemPrice = "";

// This keeps id of topping that will be included to the menu item
var _arrayOfIncludeTopppingId = new Array();
// This keeps id of topping that will NOT be included to the menu item
var _arrayOfNotIncludeToppingId = new Array();

var _nextOptionId = 0;
var _optionIds = new Array();

var _allOptionIds = new Array();

var _isNewAddingItem = true;
var _limitDressing = 0;

// Load the small shopcart when load page
//$(function () {
//    LoadSmallShopCart();
//});

/**
* Adds the product to the cart
**/
function AddProductToShoppingCart(menuId, subMenuId) {
    _curCartId = '';

    console.log('Add new item to the basket with  MenuID = ' + menuId + ' and SubMenuID = ' + subMenuId);
    //AddToShopCart(menuId, subMenuId, 0);

    // Do not add the item to basket yet... wait until user finishing their dressing/ topping/ option selection.
    // So just mark the item IDs for later
    _curMenuItemId = menuId;
    _curSubMenuItemId = subMenuId;
    AddToShopCart(menuId, subMenuId, 0);

   
}

function SelectMenuId(menuId, subMenuId, toppingPrice) {
    _selMenuId = menuId;
    _selSubMenuId = subMenuId;
    _selToppingPrice = toppingPrice;

    // Clear the previous chosen
    for (i = 0; i < document.forms[0].elements.length; i++) {
        elm = document.forms[0].elements[i];
        if (elm.type == 'checkbox') {
            elm.checked = false;
        }
    }
    _arrayOfIncludeTopppingId = new Array();
    _arrayOfNotIncludeToppingId = new Array();
}

/****************************************************************/
/* Functions for topping operations
/****************************************************************/
var _curCartId;
var _inclToppingIds = new Array();
var _exclToppingIds = new Array();
var _leftToppingIds = new Array();
var _rightToppingIds = new Array();

var _customizeTopping = 'yes';

function ClearPreviousSelection() {
    _inclToppingIds = new Array();
    _exclToppingIds = new Array();
    _leftToppingIds = new Array();
    _rightToppingIds = new Array();
    UpdateToppingCheckBoxes(new Array());
}

function PopupToppings(menuItemId, subMenuItemId, nextOptionId) {
    var modal = $find('ctl00_MainContent_ModalPopupExtenderTopping');
    if (modal != null) {
        _curMenuItemId = menuItemId;
        _curSubMenuItemId = subMenuItemId;

        ClearPreviousSelection();
        modal.show();
    }
    _nextOptionId = nextOptionId;
}

function AddToppingToTheItemInBasket() {
    console.log('MenuItem to have toppings add to: ' + _selMenuId + ', ' + _selSubMenuId + ', ' + _selToppingPrice);
    var inclToppingIdStr = '';
    if (_arrayOfIncludeTopppingId.length > 0) {
        for (i = 0; i < _arrayOfIncludeTopppingId.length; i++) {
            inclToppingIdStr += _arrayOfIncludeTopppingId[i] + ';';
        }
    }
    var exclToppingIdStr = '';
    if (_arrayOfNotIncludeToppingId.length > 0) {
        for (j = 0; j < _arrayOfNotIncludeToppingId.length; j++) {
            exclToppingIdStr += _arrayOfNotIncludeToppingId[j] + ';';
        }
    }

    console.log('Include Topping: ' + inclToppingIdStr);
    console.log('Exclude Topping: ' + exclToppingIdStr);

    $.post("/Ajax/AddToShopCart.aspx", { menuItemId: _selMenuId, subMenuItemId: _selSubMenuId, inclToppingIds: inclToppingIdStr, exclToppingIds: exclToppingIdStr, toppingPrice: _selToppingPrice }, function (result) {
        if (result == "Error") {
            alert('There is a error occurred, the menu is not added to the shop cart! Please try again later.');
        }
        else {
            /* Commented by Bogdan
            $.post("/Ajax/SmallShoppingCartWithoutDeals.aspx", function (result1) {
                $(".shopping_cart").html(result1);
            });
            */
            $.post("/Ajax/SmallShoppingCart.aspx", function (result1) {
                $(".shopping_cart").html(result1);

                /*
                $('html,body').animate({
                                scrollTop: $("#" + "sidebar").offset().top
                            },
                 'fast');
                */

                $('.basketMobi').removeClass('bsktActive');
                $('.basketMobi').addClass('bsktActive');
                setTimeout(function () {
                    $('.basketMobi').removeClass('bsktActive');
                }, 1500);


            });
        }
        return false;
    });
}

function AddToppingToTheItemInBasketNew() {
    if (_nextOptionId > 0) {
        PopupOptions(_curMenuItemId, _curSubMenuItemId, _nextOptionId, 0);
        _isNewAddingItem = false;
    } else {
        AddMenuItemWithSelection();
    }
}

function MarkToppingIdToAdd(toppingId, control, isIncluding) {
    if (control.checked) {
        console.log('ToppingID to be added:' + toppingId);
        if (isIncluding) {
            _arrayOfIncludeTopppingId[_arrayOfIncludeTopppingId.length] = toppingId;
        } else {
            _arrayOfNotIncludeToppingId[_arrayOfNotIncludeToppingId.length] = toppingId;
        }
    } else {
        console.log('ToppingID to be removed:' + toppingId);
        var pos = -1;
        if (isIncluding) {
            pos = _arrayOfIncludeTopppingId.indexOf(toppingId);
            if (pos != -1) {
                _arrayOfIncludeTopppingId.splice(pos, 1);
            }
        } else {
            pos = _arrayOfNotIncludeToppingId.indexOf(toppingId);
            if (pos != -1) {
                _arrayOfNotIncludeToppingId.splice(pos, 1);
            }
        }
    }
}

function CustomizeToppingChanged(customization) {
    console.log('The topping option is now: ' + customization);
    _customizeTopping = customization;
    UpdateToppingCheckBoxesDependOnSelection();
}

function UpdateToppingCheckBoxesDependOnSelection() {
    if (_customizeTopping == 'yes') {
        UpdateToppingCheckBoxes(_inclToppingIds);
    } else if (_customizeTopping == 'no') {
        UpdateToppingCheckBoxes(_exclToppingIds);
    } else if (_customizeTopping == 'left') {
        UpdateToppingCheckBoxes(_leftToppingIds);
    } else if (_customizeTopping == 'right') {
        UpdateToppingCheckBoxes(_rightToppingIds);
    } else {
        UpdateToppingCheckBoxes(_inclToppingIds);
    }
}

function UpdateToppingCheckBoxes(arr) {
    for (var i = 0; i < document.forms[0].elements.length; i++) {
        var elm = document.forms[0].elements[i];
        if (elm.type == 'checkbox') {
            elm.checked = false;
            for (var j = 0; j < arr.length; j++) {
                var elmId = 'tpp_' + arr[j];
                if (elm.id == elmId) {
                    elm.checked = true;
                }
            }
        }
    }
}

function MarkUpToppingIdAndPosition(toppingId, ctrl) {
    if (ctrl.checked) {
        console.log('ToppingID to be added: ' + toppingId);

        if (_customizeTopping == 'yes') {
            _inclToppingIds[_inclToppingIds.length] = toppingId;
        } else if (_customizeTopping == 'no') {
            _exclToppingIds[_exclToppingIds.length] = toppingId;
        } else if (_customizeTopping == 'left') {
            _leftToppingIds[_leftToppingIds.length] = toppingId;
        } else if (_customizeTopping == 'right') {
            _rightToppingIds[_rightToppingIds.length] = toppingId;
        } else {
            _inclToppingIds[_inclToppingIds.length] = toppingId;
        }
    } else {
        console.log('ToppingID to be removed:' + toppingId);

        var pos = -1;
        if (_customizeTopping == 'yes') {
            pos = _inclToppingIds.indexOf(toppingId);
            if (pos != -1) {
                _inclToppingIds.splice(pos, 1);
            }
        } else if (_customizeTopping == 'no') {
            pos = _exclToppingIds.indexOf(toppingId);
            if (pos != -1) {
                _exclToppingIds.splice(pos, 1);
            }
        } else if (_customizeTopping == 'left') {
            pos = _leftToppingIds.indexOf(toppingId);
            if (pos != -1) {
                _leftToppingIds.splice(pos, 1);
            }
        } else if (_customizeTopping == 'right') {
            pos = _rightToppingIds.indexOf(toppingId);
            if (pos != -1) {
                _rightToppingIds.splice(pos, 1);
            }
        } else {
            pos = _inclToppingIds.indexOf(toppingId);
            if (pos != -1) {
                _inclToppingIds.splice(pos, 1);
            }
        }
    }
}

/****************************************************************************/
/* Functions for Dressing
/****************************************************************************/

function PopupDressings(menuItemId, subMenuItemId, nextOptionId, categoryId) {
    var modal = $find('ctl00_MainContent_DressingModalPopupExtender');
    if (modal != null) {
        _curMenuItemId = menuItemId;
        _curSubMenuItemId = subMenuItemId;

        modal.show();
        // Clear the previous chosen
        for (i = 0; i < document.forms[0].elements.length; i++) {
            elm = document.forms[0].elements[i];
            if (elm.type == 'checkbox') {
                elm.checked = false;
                elm.disabled = false;
            }
        }
        _arrDressingID = new Array();
    }
    _nextOptionId = nextOptionId;

    $.post("/Ajax/GetMaxDressing.aspx", { id: categoryId }, function (result) {
        if (result == "Error") {
            alert('There is a error occurred! Please try again later.');
        }
        else {
            if (!isNaN(result)) _limitDressing = result;
        }
    });
}

/**
* Adds chosen dressings to the menu in the shopping cart
**/
function MarkDressingIdToAdd(dressingId, ctrl) {
    if (ctrl.checked) {
        _arrDressingID[_arrDressingID.length] = dressingId;
    } else {
        var pos = _arrDressingID.indexOf(dressingId);
        if (pos != -1) {
            _arrDressingID.splice(pos, 1);
        }
    }
    // Check limit of how many dressings a customer can choose
    var i;
    var elm;
    var inputs = document.getElementById("ctl00_MainContent_DressingPanel").getElementsByTagName('input');
    if (_limitDressing != 0 && _arrDressingID.length == _limitDressing) {
        for (i = 0; i < inputs.length; i++) {
            elm = inputs[i];
            if (elm.type == 'checkbox') {
                if (!elm.checked) elm.disabled = true;
            }
        }
    } else {
        for (i = 0; i < inputs.length; i++) {
            elm = inputs[i];
            if (elm.type == 'checkbox') {
                elm.disabled = false;
            }
        }
    }
}

/*
*
*/
function AddMenuItemWithDressing() {
    if (_nextOptionId > 0) {
        PopupOptions(_curMenuItemId, _curSubMenuItemId, _nextOptionId, 0);
        _isNewAddingItem = false;
    } else {
        AddMenuItemWithSelection();
    }
}

function MarkAsNewItemAdding() {
    _isNewAddingItem = true;
    _allOptionIds = new Array();
    _arrDressingID = new Array();
    ClearPreviousSelection();
}

/************************************************************/
/* Functions for menu-option operations
/************************************************************/
function PopupOptions(itemId, subItemId, optionId, nextOptionId) {
    _curMenuItemId = itemId;
    _curSubMenuItemId = subItemId;

    var popupId = 'ctl00_MainContent_' + optionId + '_ModalPopupExtender';
    var modal = $find(popupId);

    if (modal != null) {
        // Clear the previous chosen
        for (i = 0; i < document.forms[0].elements.length; i++) {
            elm = document.forms[0].elements[i];
            if (elm.type == 'checkbox') {
                elm.checked = false;
            }
        }
        _optionIds = new Array();
        _currentOptionId = optionId;
        modal.show();
        
    }

    _nextOptionId = nextOptionId;

   
   

}

function MarkOptionIdToAdd(optionId, ctrl) {

    var hdnItemsAllowedId = '#ctl00_MainContent_' + _currentOptionId + '_hdnItemsAllowed';
    var hdnItemsAllowed = $(hdnItemsAllowedId);
    var maxOfAllowedItems = 0;

    if (hdnItemsAllowed != null) {
        maxOfAllowedItems = parseInt(hdnItemsAllowed.val());

        if (maxOfAllowedItems > 0 && _optionIds.length + 1 > maxOfAllowedItems) {
            alert('You have maximum of ' + maxOfAllowedItems + ' items');
            return;
        }
    }

    if (ctrl.checked) {
        _optionIds[_optionIds.length] = optionId;
    } else {
        var pos = _optionIds.indexOf(optionId);
        if (pos != -1) {
            _optionIds.splice(pos, 1);
        }
    }
}

function AddMenuItemWithOptions() {
    var pos = _allOptionIds.length;
    if (_optionIds.length > 0) {
        for (var j = 0; j < _optionIds.length; j++) {
            _allOptionIds[pos + j] = _optionIds[j];
        }
    }

    if (_nextOptionId > 0) {
        // Open next selection
        PopupOptions(_curMenuItemId, _curSubMenuItemId, _nextOptionId, 0);
        _isNewAddingItem = false;
    } else {
        AddMenuItemWithSelection();
    }
}

function AddMenuItemWithSelection() {
    
    console.log('MenuItem to have toppings add to: ' + _selMenuId + ', ' + _selSubMenuId + ', ' + _selToppingPrice);

    var inclToppingIdStr = '';
    if (_inclToppingIds.length > 0) {
        for (var i = 0; i < _inclToppingIds.length; i++) {
            inclToppingIdStr += _inclToppingIds[i] + ';';
        }
    }

    var exclToppingIdStr = '';
    if (_exclToppingIds.length > 0) {
        for (var j = 0; j < _exclToppingIds.length; j++) {
            exclToppingIdStr += _exclToppingIds[j] + ';';
        }
    }

    var leftToppingIdStr = '';
    if (_leftToppingIds.length > 0) {
        for (var k = 0; k < _leftToppingIds.length; k++) {
            leftToppingIdStr += _leftToppingIds[k] + ';';
        }
    }

    var rightToppingIdStr = '';
    if (_rightToppingIds.length > 0) {
        for (var l = 0; l < _rightToppingIds.length; l++) {
            rightToppingIdStr += _rightToppingIds[l] + ';';
        }
    }

    console.log('Include Topping: ' + inclToppingIdStr);
    console.log('Exclude Topping: ' + exclToppingIdStr);
    console.log('Left Topping: ' + leftToppingIdStr);
    console.log('Right Topping: ' + rightToppingIdStr);
    
    var dressingIds = '';
    if (_arrDressingID.length > 0) {
        for (var index = 0; index < _arrDressingID.length; index++) {
            dressingIds += _arrDressingID[index] + ';';
        }
    }

    var optionIds = '';
    //if (_optionIds.length > 0) {
    //    for (var j = 0; j < _optionIds.length; j++) {
    //        optionIds += _optionIds[j] + ';';
    //    }
    //}
    
    if (_allOptionIds.length > 0) {
        for (var j = 0; j < _allOptionIds.length; j++) {
            optionIds += _allOptionIds[j] + ';';
        }
    }

    //End of the selection, and then add menu-item with the selection to the basket
    $.post("/ajax/AddToShopCart.aspx", { MenuItemId: _curMenuItemId, SubMenuItemId: _curSubMenuItemId, dressingIds: dressingIds, optionIds: optionIds, inclToppingIds: inclToppingIdStr, exclToppingIds: exclToppingIdStr, leftToppingIds: leftToppingIdStr, rightToppingIds: rightToppingIdStr  }, function (result) {
        if (result == "Error") {
            alert('There is a error occurred, the menu is not added to the shop cart! Please try again later.');
        } else {
            _curCartId = result;
        
            // here was get
            $.post("/ajax/SmallShoppingCart.aspx", function (data, status) {
                if (status == 'success') {
                    $(".shopping_cart").html(data);

                    $('.basketMobi').removeClass('bsktActive');
                    $('.basketMobi').addClass('bsktActive');
                    setTimeout(function () {
                        $('.basketMobi').removeClass('bsktActive');
                    }, 1500);
                }
            });
        }
        return false;
    });
}

function RemoveMenuItemFromShopCartWithDeals(cartId) {
    $.post("/Ajax/RemoveItemFromShopCart.aspx", { CartID: cartId }, function (result) {
        if (result == "Error") {
            alert('There is a error occurred, the menu is not remvoed from the shop cart! Please try again later.');
        }
        else {
           
            // here was get
            $.post("/Ajax/SmallShoppingCart.aspx", function (data, status) {
                if (status == 'success') {
                    //$(".shopping_cart_deals").html(data);
                    $(".shopping_cart").html(data);


                }
            });
        }
        return false;
    });
}

function RemoveMenuItemFromShopCart(cartId) {
    showInProgressModal();
    $.post("/Ajax/RemoveItemFromShopCart.aspx", { CartID: cartId }, function (result) {
        if (result == "Error") {
            hideInProgressModal();
            alert('There is a error occurred, the menu is not remvoed from the shop cart! Please try again later.');
        }
        else {
           
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

function LoadSmallShopCart() {
    // here was get
    $.post("/Ajax/SmallShoppingCartWithoutDeals.aspx", function (result) {
        $(".shopping_cart").html(result);

        $('.basketMobi').removeClass('bsktActive');
        $('.basketMobi').addClass('bsktActive');
        setTimeout(function () {
            $('.basketMobi').removeClass('bsktActive');
        }, 1500);

    });

}

function LoadSmallShopCartWithDeals() {
    // here was get
    $.post("/Ajax/SmallShoppingCart.aspx", function (result) {
        //$(".shopping_cart_deals").html(result);
        $(".shopping_cart").html(result);
       
        $('.basketMobi').removeClass('bsktActive');
        $('.basketMobi').addClass('bsktActive');
        setTimeout(function () {
            $('.basketMobi').removeClass('bsktActive');
        }, 1500);

    });
}

function Refresh_MinOrderPrice(totalPrice) {
    if (totalPrice == "") {
        totalPrice = 0;
    }
    var minOrder = 0;

    $.ajax({
        type: "GET",
        cache: false,
        url: "/Ajax/GetMinOrderPostCode.aspx",
       // contentType: 'application/json; charset=utf-8',
       // dataType: 'json',
        success: function(res) {
            minOrder = res;
            //minOrder = jQuery.parseJSON(res);
            if (totalPrice < minOrder) {
                alert("Minimum Order value should be £ " + minOrder);
            }
            else {
                var instruction = $("#txtAdditionalInstruction").val();
                $.post("/Ajax/SaveInstructions.aspx", { instructions: instruction }, function (result) {
                    if (result == "Error") {
                        alert('There is a error occurred! Please try again later.');
                    } else {
                        window.location = "OrderReview.aspx";
                    }
                    return true;
                });
            }
        },
        error: function(res) {
           // var r = jQuery.parseJSON(res.responseText);
           // alert("Message: " + r.Message);
           // alert("StackTrace: " + r.StackTrace);
            // alert("ExceptionType: " + r.ExceptionType);

            alert("Message: " + res);
        }
    });

    
}

function CheckDecimal(input) {
    var decimal = /^[-+]?[0-9]+\.[0-9]+$/;
    if (input.match(decimal)) {
        return true;
    }
    else {
        return false;
    }
}

function Refresh_WithoutMinOrderPrice() {
    var instruction = $("#txtAdditionalInstruction").val();
    $.post("/Ajax/SaveInstructions.aspx", { instructions: instruction }, function (result) {
        if (result == "Error") {
            alert('There is a error occurred! Please try again later.');
        } else {
            window.location = "OrderReview.aspx";
        }
        return true;
    });
}

function OnSaveAdditionalInstructionCompleted(result) {
    alert('SaveAdditionalInstruction was done ' + result);
}

function OnFailed(error) {
    alert(error);
}

function Check_MinOrderPrice(totalPrice) {
    if (totalPrice == "") {
        totalPrice = 0;
    }
    if (totalPrice < 8.0) {
        alert("Minimum Order value should be £ 8.00 ");
    }
    else {
        window.location = "Thankyou.aspx";
    }
}

function LoadLargeShoppingCart() {
    // here was get
    $.post("/Ajax/ShoppingCart.aspx", function (result) {
        $(".order-review-shopping-cart").html(result);
    });
}

function LoadLargeShoppingCartWithDeals() {
    // here was get
    $.post("/Ajax/ShoppingCartWithDeals.aspx", function (result) {
        $(".order-review-shopping-cart").html(result);
    });
}

function RemoveMenuItemFromLargeShopCart(menuItemId, subMenuItemId) {
    $.post("/Ajax/RemoveItemFromShopCart.aspx", { menuItemId: menuItemId, subMenuItemId: subMenuItemId }, function (result) {
        if (result == "Error") {
            alert('There is a error occurred, the menu is not remvoed from the shop cart! Please try again later.');
        }
        else {
            $.post("/Ajax/ShoppingCart.aspx", function (result1) {
                $(".order-review-shopping-cart").html(result1);
            });
        }
        return false;
    });
}

function AddProductToLargeShoppingCart(menuId, subMenuId) {
    AddToLargeShopCart(menuId, subMenuId, 0);
}

function AddToLargeShopCart(menuItemId, subMenuItemId, dressingId) {
    $.post("/Ajax/AddToShopCart.aspx", { menuItemId: menuItemId, subMenuItemId: subMenuItemId, dressingId: dressingId }, function (result) {
        if (result == "Error") {
            alert('There is a error occurred, the menu is not added to the shop cart! Please try again later.');
        }
        else {
            $.post("/Ajax/ShoppingCart.aspx", function (result1) {
                $(".order-review-shopping-cart").html(result1);
            });
        }
        return false;
    });
}

function RemoveOptionsFromBasket(itemId, subItemId, optionId, itemType) {

}

function hidePopup() {
    $find("ctl00_MainContent_mpeUpdateAddress").hide();
    return false;
}

function IncreaseQuantityOfItem(cartId) {
    $.post("/Ajax/IncreaseQuantity.aspx", { CartId: cartId }, function (result) {
        if (result == "Error") {
            alert('There is a error occurred! Please try again later.');
        }
        else {
            $.post("/Ajax/ShoppingCart.aspx", function (result1) {
                $(".order-review-shopping-cart").html(result1);
            });
        }
        return false;
    });
}

function DecreaseQuantityOfItem(cartId) {
    $.post("/Ajax/DecreaseQuantity.aspx", { CartId: cartId }, function (result) {
        if (result == "Error") {
            alert('There is a error occurred! Please try again later.');
        }
        else {
            $.post("/Ajax/ShoppingCart.aspx", function (result1) {
                $(".order-review-shopping-cart").html(result1);
            });
        }
        return false;
    });
}

function IncreaseQuantityOfItemWithDeals(cartId) {
    $.post("/Ajax/IncreaseQuantity.aspx", { CartId: cartId }, function (result) {
        if (result == "Error") {
            alert('There is a error occurred! Please try again later.');
        }
        else {
            $.post("/Ajax/ShoppingCartWithDeals.aspx", function (result1) {
                $(".order-review-shopping-cart").html(result1);
            });
        }
        return false;
    });
}

function DecreaseQuantityOfItemWithDeals(cartId) {
    $.post("/Ajax/DecreaseQuantity.aspx", { CartId: cartId }, function (result) {
        if (result == "Error") {
            alert('There is a error occurred! Please try again later.');
        }
        else {
            $.post("/Ajax/ShoppingCartWithDeals.aspx", function (result1) {
                $(".order-review-shopping-cart").html(result1);
            });
        }
        return false;
    });
}

var _currentOptionId = 0;
function SetCurrentOptionId(id) {
    _currentOptionId = id;
    return false;
}

function ShowDealsOption(detailId) {

}

function MarkMenuItemToAdd(menuId, subMenuId) {
    _selMenuId = menuId;
    _selSubMenuId = subMenuId;
}