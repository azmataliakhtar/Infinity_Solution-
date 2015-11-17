using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

namespace INF.Web.UI.Shopping
{
    public class BxShoppingCart
    {
        public const string SHOPPING_CART_NAME = "ASPNETShoppingCart";
        public const string POST_CODE_CHARGE = "POST_CODE_CHARGE";
        public const string SS_ORDER_TYPE = "SS_ORDER_TYPE";
        public const string SS_POST_CODE = "SS_POST_CODE";

        public const string SS_MINIMUM_ORDER_VALUE = "SS_MINIMUM_ORDER_VALUE";

        public const string ORDER_TYPE_DELIVERY = "DELIVERY";
        public const string ORDER_TYPE_COLLECTION = "COLLECTION";
        public const string CK_POST_CODE = "CK_POST_CODE";
        public const string CK_POST_CODE_CHARGE = "CK_POST_CODE_CHARGE";
        public const string CK_ORDER_TYPE = "CK_ORDER_TYPE";

        private static double _toppingPrice = 0;

        public bool IsErrorMessage { get; private set; }
        public string Message { get; private set; }
        public decimal DeliveryCharge { get; set; }

        private decimal _discountInPercent;

        public decimal DiscountInPercent
        {
            get
            {
                double specialDiscount = 0;
                if (IsDeliveryOrder)
                {
                    if ((double)GetSubTotal() >= DeliverySpecialDiscountOrderValue)
                    {
                        specialDiscount = DeliverySpecialDiscount;
                    }
                }
                else
                {
                    if ((double)GetSubTotal() >= CollectionSpecialDiscountOrderValue)
                    {
                        specialDiscount = CollectionSpecialDiscount;
                    }
                }
                if (specialDiscount > (double)_discountInPercent)
                    _discountInPercent = (decimal)specialDiscount;
                return _discountInPercent;
            }
            set
            {
                _discountInPercent = value;
            }
        }

        public double CollectionSpecialDiscountOrderValue { get; set; }
        public double CollectionSpecialDiscount { get; set; }
        public double DeliverySpecialDiscountOrderValue { get; set; }
        public double DeliverySpecialDiscount { get; set; }
        public bool IsDeliveryOrder { get; set; }
        public string AddtitionalInstruction { get; set; }
        public List<BxCartItem> Items { get; private set; }

        /// <summary>
        /// The static constructor is called as soon as the class is loaded into memory
        /// </summary>
        public static BxShoppingCart GetShoppingCart()
        {
            var instance = HttpContext.Current.Session[SHOPPING_CART_NAME];
            if (instance == null)
            {
                instance = new BxShoppingCart();
                HttpContext.Current.Session[SHOPPING_CART_NAME] = instance;
            }
            return (BxShoppingCart)instance;
        }

        /// <summary>
        /// A protected constructor ensures that an object can't be created from outside
        /// </summary>
        private BxShoppingCart()
        {
            AddtitionalInstruction = string.Empty;
            DiscountInPercent = 0;
            DeliveryCharge = 0;
            IsErrorMessage = false;
            Items = new List<BxCartItem>();
        }

        public string HasExistedItem(int dealId, int[] menuIds, int[] subMenuIds, string[] toppingIds, string[] dressingIds, string[] optionIds)
        {
            if (this.Items.Count == 0)
            {
                return string.Empty;
            }

            var cartItemId = string.Empty;
            var cartItemList = new List<string>();
            for (int index = 0; index < menuIds.Length; index++)
            {
                int menuId = menuIds[index];
                int subMenuId = subMenuIds[index];

                // Check if there is any topping adding to the item
                var intToppingIDs = new int[] { };
                if (toppingIds != null && toppingIds.Length > index && !string.IsNullOrEmpty(toppingIds[index]))
                {
                    intToppingIDs = ExtractValuesFromString(toppingIds[index]);
                }

                // Check if there is any dressing adding to the item
                var intDressingIDs = new int[] { };
                if (dressingIds != null && dressingIds.Length > index && !string.IsNullOrEmpty(dressingIds[index]))
                {
                    intDressingIDs = ExtractValuesFromString(dressingIds[index]);
                }

                var intOptionIds = new int[] {};
                if (optionIds != null && optionIds.Length > index && !string.IsNullOrEmpty(optionIds[index]))
                {                  
                    intOptionIds = ExtractValuesFromString(optionIds[index]);
                }

                bool isFound = false;
                foreach (var item in this.Items)
                {
                    if (item.DeepEquals(dealId, menuId, subMenuId, intToppingIDs,intDressingIDs, intOptionIds))
                    {
                        isFound = true;
                        cartItemList.Add(item.CartId);
                        break;
                    }
                }
                if (!isFound)
                {
                    cartItemList.Add("");
                }
            }

            if (cartItemList.Count > 1)
            {
                for (int index = 0; index < cartItemId.Length-1; index++)
                {
                    if (cartItemList[index] != cartItemList[index + 1])
                    {
                        cartItemId = "";
                        break;
                    }
                    cartItemId = cartItemList[index];
                }
            }
            else if (cartItemList.Count == 1)
            {
                cartItemId = cartItemList[0];
            }
            else
            {
                cartItemId = "";
            }

            return cartItemId;
        }

        public int[] ExtractValuesFromString(string source)
        {
            var idStr = source.Split(';');
            var intVals = (from sId in idStr
                           where !string.IsNullOrEmpty(sId) && Regex.IsMatch(sId, @"\d+")
                           select Convert.ToInt32(sId)).ToArray();
            return intVals;
        }

        public string HasExistedItem(int dealId, int menuId, int subMenuId, int[] toppingIds, int[] dressingIds, int[] optionIds)
        {
            var cartItemId = string.Empty;

            // Check to see if the item already existed in the basket
            // If it existed, get its CartID

            foreach (var item in this.Items)
            {
                if (item.DeepEquals(dealId, menuId, subMenuId, toppingIds, dressingIds, optionIds))
                {
                    cartItemId = item.CartId;
                    break;
                }
            }
            return cartItemId;

        }

        public string AddItem(string cartId, int dealId, int menuId, int subMenuId)
        {
            // Check to see if there is already item created for the deal
            BxCartItem existedItem = null;
            if (dealId > 0 && Items != null && Items.Count > 0)
            {
                for (int index = 0; index < Items.Count; index++)
                {
                    if (Items[index].CartId == cartId)
                    {
                        existedItem = Items[index];
                        break;
                    }
                }
            }

            // Add the menu item to the deal
            if (existedItem != null)
            {
                AddMenuItemOnToExistedDeal(existedItem.CartId, menuId, subMenuId);
                return existedItem.CartId;
            }

            var newItem = new BxCartItem(dealId, menuId, subMenuId, 0, 0);
            newItem.Load(IsDeliveryOrder);
            newItem.Quantity = 1;
            if (Items == null)
                Items = new List<BxCartItem>();
            Items.Add(newItem);
            Message = "An item has been added to your basket!";
            return newItem.CartId;

        }

        public string AddItem(int dealId, int menuId, int subMenuId)
        {
            // Check to see if there is already item created for the deal
            BxCartItem existedItem = null;
            if (dealId > 0 && Items != null && Items.Count > 0)
            {
                for (int index = 0; index < Items.Count; index++)
                {
                    if (Items[index].DealID == dealId)
                    {
                        existedItem = Items[index];
                        break;
                    }
                }
            }

            // Add the menu item to the deal
            if (existedItem != null)
            {
                AddMenuItemOnToExistedDeal(existedItem.CartId, menuId, subMenuId);
                return existedItem.CartId;
            }

            var newItem = new BxCartItem(dealId, menuId, subMenuId, 0, 0);
            newItem.Load(IsDeliveryOrder);
            newItem.Quantity = 1;
            if (Items == null)
                Items = new List<BxCartItem>();
            Items.Add(newItem);
            Message = "An item has been added to your basket!";
            return newItem.CartId;

        }

        public void AddMenuItemOnToExistedDeal(string cartItemId, int menuItemId, int subMenuItemId)
        {
            var cartItem = Items.FirstOrDefault(i => i.CartId == cartItemId);
            if (cartItem == null) return;

            var subItem = new BxMenuItem(menuItemId) { };
            subItem.Load(IsDeliveryOrder);
            if (!string.IsNullOrWhiteSpace(subItem.Name) && !cartItem.Product.Contains(subItem))
            {
                cartItem.Product.Items.Add(subItem);
            }

            if (subMenuItemId > 0)
            {
                var grandSubItem = new SubMenuItem(menuItemId, subMenuItemId);
                grandSubItem.Load(IsDeliveryOrder);
                if (!string.IsNullOrWhiteSpace(grandSubItem.Name) && !cartItem.Product.GetMenuItem(menuItemId).Contains(grandSubItem))
                {
                    cartItem.Product.GetMenuItem(menuItemId).Items.Add(grandSubItem);
                    if ((grandSubItem.UnitPrice > 0))
                    {
                        cartItem.Product.GetMenuItem(menuItemId).UnitPrice = grandSubItem.UnitPrice;
                    }
                }
            }
        }

        public void AddItem(string cartItemId)
        {
            if (string.IsNullOrWhiteSpace(cartItemId))
                return;

            foreach (var crtItem in Items.Where(crtItem => crtItem.CartId == cartItemId))
            {
                crtItem.Quantity = crtItem.Quantity + 1;
                Message = "An item has been added to your basket!";
            }
        }

        public void AddToppingOnMenuItem(string cartItemId, int menuId, int subMenuId, int toppingId, string toppingPos)
        {
            var cartItem = Items.FirstOrDefault(i => i.CartId == cartItemId);
            if (cartItem != null)
            {
                AddToppingOnMenuItemWithPosition(menuId, subMenuId,toppingId, toppingPos, ref cartItem);
            }
        }

        public string AddItemWithOptions(string cartItemId, int menuId, int optionId)
        {
            if (string.IsNullOrEmpty(cartItemId)) return "";

            BxCartItem cartItem = Items.FirstOrDefault(i => i.CartId == cartItemId);
            if (cartItem == null) return "";

            AddItemWithOptions(menuId, optionId, ref cartItem);
            return cartItemId;
        }

        public string AddItemWithOptions(int dealId, int menuId, int subMenuId, int optionId)
        {
            //Look up cart-item that has the same IDs given
            BxCartItem cartItem = FindCartItemByIDs(dealId, menuId);

            if (cartItem == null)
            {
                var newItem = new BxCartItem(dealId, menuId, subMenuId, 0, 0);
                newItem.Load(IsDeliveryOrder);
                newItem.Quantity = 1;
                Items.Add(newItem);
                Message = "An item has been added to your basket!";
                cartItem = newItem;
            }

            AddItemWithOptions(menuId, optionId, ref cartItem);
            return cartItem.CartId;
        }


        public void AddItemWithOptions(int menuId, int optionId, ref BxCartItem cartItem)
        {
            if (cartItem == null || optionId <= 0)
                return;

            var menuItemToAddOptionsOn = cartItem.Product.GetMenuItem(menuId);
            if (menuItemToAddOptionsOn == null)
            {
                AddMenuItemOnToExistedDeal(cartItem.CartId, menuId, 0);
                menuItemToAddOptionsOn = cartItem.Product.GetMenuItem(menuId);
            }

            var optDetail = new MenuOption(menuId, optionId);
            optDetail.Load(IsDeliveryOrder);

            // Check the limitation of number of items to be added
            IsErrorMessage = false;
            int allowedItems = optDetail.GetAllowedItems(optDetail.ParentId);
            if ((allowedItems != 0))
            {
                if (menuItemToAddOptionsOn.CountOfMenuOptions(optDetail.ParentId) == allowedItems)
                {
                    IsErrorMessage = true;
                    Message = "You can add only " + allowedItems + " options to this item";
                    return;
                }
            }

            if (!menuItemToAddOptionsOn.Contains(optDetail))
            {
                //cartItem.Product.Items.Add(optDetail);
                menuItemToAddOptionsOn.Items.Add(optDetail);
                Message = "An item has been added to your basket!";
            }
        }

        /// <summary>
        /// Changes the quantity of an item in the cart
        /// </summary>    
        public void SetItemQuantity(int dealId, int menuId, int subMenuId, int quantity)
        {
            // If we are setting the quantity to 0, remove the item entirely
            if (quantity <= 0)
            {
                RemoveItem(dealId, menuId, subMenuId);
                return;
            }

            // Find the item and update the quantity
            var updatedItem = FindCartItemByIDs(dealId, menuId);
            if (updatedItem != null)
            {
                updatedItem.Quantity = quantity;
            }
        }

        /// <summary>
        /// Removes an item from the shopping cart
        /// </summary>
        public void RemoveItem(int dealId, int menuId, int subMenuId)
        {
            // Find the item in the shop cart
            BxCartItem cartItemToRemove = FindCartItemByIDs(dealId, menuId);

            if (cartItemToRemove == null)
                return;

            // Check if the item should be removed or reduced the number in the cart
            if (cartItemToRemove.Quantity <= 1)
            {
                Items.Remove(cartItemToRemove);
            }
            else
            {
                cartItemToRemove.Quantity = cartItemToRemove.Quantity - 1;
            }
            Message = "An item has been removed from your basket!";
        }

        public void RemoveItem(string vCartID, bool vIncludeSubtract = true)
        {
            // Find the item in the shop cart
            var itemToRemove = Items.FirstOrDefault(item => item.CartId == vCartID);

            if (itemToRemove != null)
            {
                if (vIncludeSubtract)
                {
                    // Check if the item should be removed or reduced the number in the cart
                    if (itemToRemove.Quantity <= 1)
                    {
                        Items.Remove(itemToRemove);
                    }
                    else
                    {
                        itemToRemove.Quantity = itemToRemove.Quantity - 1;
                    }
                }
                else
                {
                    Items.Remove(itemToRemove);
                }
                Message = "An item has been removed from your basket!";
            }
        }

        /// <summary>
        /// Returns the total price of all the items before tax, shipping, etc.
        /// </summary>
        /// <returns></returns>
        public decimal GetSubTotal()
        {
            decimal subTotal = Items.Sum(item => item.TotalPrice);
            subTotal = subTotal + Convert.ToDecimal(_toppingPrice);
            return subTotal;
        }

        public decimal GetSubTotalExclCategoriesDonotHaveOnlineDiscount()
        {
            decimal subTotal = Items.Where(item => !item.ExclOnlineDiscount).Sum(item => item.TotalPrice);
            return subTotal + Convert.ToDecimal(_toppingPrice);
        }

        public decimal GetTotal()
        {
            decimal subTotal = GetSubTotal();
            decimal subTotalForOnlineDiscountCalculating = GetSubTotalExclCategoriesDonotHaveOnlineDiscount();
            decimal total = (subTotal - ((subTotalForOnlineDiscountCalculating * DiscountInPercent) / 100) + DeliveryCharge);
            return total;
        }

        public void Clear()
        {
            HttpContext.Current.Session[SHOPPING_CART_NAME] = null;
        }

        public static void AddToppingPrice(double toppingPrice)
        {
            _toppingPrice = toppingPrice;
        }

        public void AddDressingOnMenuItem(string cartItemId, int menuId, int dressingId)
        {
            if (dressingId > 0)
            {
                var cartItem = Items.FirstOrDefault(i => i.CartId == cartItemId);
                if (cartItem != null)
                {
                    var menuItemToAddDressingOn = cartItem.Product.GetMenuItem(menuId);
                    if (menuItemToAddDressingOn == null)
                    {
                        AddMenuItemOnToExistedDeal(cartItem.CartId, menuId, 0);
                        menuItemToAddDressingOn = cartItem.Product.GetMenuItem(menuId);
                    }

                    var dressing = new MenuDressing(menuId, dressingId);
                    dressing.Load(IsDeliveryOrder);
                    if (menuItemToAddDressingOn != null && !menuItemToAddDressingOn.Contains(dressing))
                    {
                        menuItemToAddDressingOn.Items.Add(dressing);
                    }
                }
            }
        }

        private void AddToppingOnMenuItemWithPosition(int menuId, int subMenuId, int toppingId, string toppingPos, ref BxCartItem item)
        {
            if (item == null)
                return;

            BxDealItem dealItem = item.Product;
            if (dealItem == null)
                return;

            BxMenuItem menuItemToAddToppingOn = null;
            for (int index = 0; index < dealItem.Items.Count; index++)
            {
                BxMenuItem mnuItem = dealItem.Items[index] as BxMenuItem;
                if (mnuItem == null)
                    continue;

                if (mnuItem.ID == menuId)
                {
                    menuItemToAddToppingOn = mnuItem;
                    break;
                }
            }

            //var menuItemToAddToppingOn = item.Product.GetMenuItem(menuId);
            if (menuItemToAddToppingOn == null)
                return;

            if (toppingId != 0)
            {
                var isExcluding = toppingId < 0;

                var topping = new MenuTopping(menuId,subMenuId, Math.Abs(toppingId));
                topping.Load(IsDeliveryOrder);

                if (!string.IsNullOrWhiteSpace(topping.Name) && !menuItemToAddToppingOn.Contains(topping))
                {
                    if (isExcluding)
                    {
                        topping.Name = "NO " + topping.Name;
                        topping.UnitPrice = 0;
                    }
                    else
                    {
                        if (toppingPos.ToLower() == "left")
                        {
                            topping.Name = "LEFT " + topping.Name;
                        }
                        else
                        {
                            if (toppingPos.ToLower() == "right")
                            {
                                topping.Name = "RIGHT " + topping.Name;
                            }
                        }
                    }

                    menuItemToAddToppingOn.Items.Add(topping);
                }
            }
        }

        private BxCartItem FindCartItemByIDs(int dealId, int menuId)
        {
            BxCartItem foundItem = null;

            // Find the item in the shop cart
            if (dealId > 0)
            {
                foundItem = Items.FirstOrDefault(i => i.DealID == dealId);
            }
            else
            {
                foreach (var cartItem in Items)
                {
                    if (cartItem.DealID != 0)
                        continue;

                    if (cartItem.Product.Items.OfType<BxMenuItem>().Any(itemToRemove => itemToRemove.ID == menuId))
                    {
                        foundItem = cartItem;
                    }
                }
            }
            return foundItem;
        }

        private XmlElement GetXMLElement(XmlDocument doc, string name, string value)
        {
            XmlElement element = doc.CreateElement(name);
            element.InnerText = value;
            return element;
        }

        private XmlElement GetXMLElement(XmlDocument doc, string name, decimal value)
        {
            return GetXMLElement(doc, name, value.ToString());
        }

        public string ToXml()
        {
            var doc = new XmlDocument();
            doc.LoadXml("<basket></basket>");
            XmlNode basketNode = doc.GetElementsByTagName("basket")[0];

            foreach (BxCartItem item in Items)
            {
                XmlElement xmlElementItem = doc.CreateElement("item");
                xmlElementItem.AppendChild(GetXMLElement(doc, "description", item.Description));
                xmlElementItem.AppendChild(GetXMLElement(doc, "quantity", item.Quantity));
                xmlElementItem.AppendChild(GetXMLElement(doc, "unitNetAmount", item.UnitPrice.ToString("N2"))); //**NET Price: Price ex-Vat **
                xmlElementItem.AppendChild(GetXMLElement(doc, "unitTaxAmount", 0.5m));//'**Tax: VAT component **
                xmlElementItem.AppendChild(GetXMLElement(doc, "unitGrossAmount", item.UnitPrice.ToString("N2")));//Gross Price: Item price
                xmlElementItem.AppendChild(GetXMLElement(doc, "totalGrossAmount", (item.UnitPrice * item.Quantity).ToString("N2")));
                basketNode.AppendChild(xmlElementItem);
            }

            if (IsDeliveryOrder)
            {
                basketNode.AppendChild(GetXMLElement(doc, "deliveryNetAmount", DeliveryCharge.ToString("N2")));
                basketNode.AppendChild(GetXMLElement(doc, "deliveryTaxAmount", DeliveryCharge.ToString("N2")));
                basketNode.AppendChild(GetXMLElement(doc, "deliveryGrossAmount", DeliveryCharge.ToString("N2")));
            }
            else
            {
                basketNode.AppendChild(GetXMLElement(doc, "deliveryNetAmount", DeliveryCharge.ToString("N2")));
                basketNode.AppendChild(GetXMLElement(doc, "deliveryTaxAmount", DeliveryCharge.ToString("N2")));
                basketNode.AppendChild(GetXMLElement(doc, "deliveryGrossAmount", DeliveryCharge.ToString("N2")));
            }

            return doc.InnerXml.ToString();
        }
    }
}