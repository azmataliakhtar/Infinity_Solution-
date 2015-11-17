using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using INF.Web.Data.BLL;

namespace INF.Web.UI.Shopping
{
    public class BxCartItem
    {
        public string CartId { get; private set; }

        private bool _isDeliveryOrder;

        private readonly int _dealId;
        private readonly int _menuItemId;
        private readonly int _subMenuItemId;
        private readonly int _dressingId;
        private readonly int _toppingId;

        public string tempName2 { get; set;}
        
        public string isWhat { get; set; }
        
        public int DealID
        {
            get { return _dealId; }
        }

        public int Quantity { get; set; }

        //public string TempName = "x";


        public BxDealItem Product { get; set; }

        public List<SubCartItem> Items
        {
            get
            {
                var subItems = new List<SubCartItem>();

                if (Product != null && Product.Items != null && Product.Items.Count > 0)
                {
                    // Loop through all menu-items of the deal
                    foreach (var item in Product.Items)
                    {
                        var menuItem = item as BxMenuItem;
                        if (menuItem == null)
                            continue;

                        var subCartItem = new SubCartItem()
                        {
                            ID = menuItem.ID,
                            Name = menuItem.Name,
                            UnitPrice = menuItem.UnitPrice,
                            ItemType = menuItem.ItemType,
                            CartID = CartId,
                            DealID = DealID,
                            isWhatSI = isWhat
                        };

                        //var grandSubItems = new List<SubCartItem>();
                        // Loop through its sub-menu or topping or dressing items
                        if (menuItem.ItemType == ItemTypes.MenuItem && menuItem.Items.Count > 0)
                        {
                            foreach (var subItem in menuItem.Items)
                            {
                                var grandSubCartItem = new SubCartItem()
                                {
                                    ID = subItem.ID,
                                    Name = subItem.Name,
                                    UnitPrice = subItem.UnitPrice,
                                    ItemType = subItem.ItemType,
                                    isWhatSI = subCartItem.isWhatSI + "SI"
                                };

                                subCartItem.Items.Add(grandSubCartItem);
                            }
                            //subCartItem.Items = grandSubItems;
                        }

                        subItems.Add(subCartItem);
                    }
                }
                return subItems;
            }
        }

        public string Description
        {
            get { return Product.Name; }
        }

        public decimal UnitPrice
        {
            get { return Product.UnitPrice; }
        }

        public decimal TotalPrice
        {
            get
            {
                decimal unitPriceWithOther = UnitPrice;
                if (Product != null && Product.Items != null)
                {
                    //unitPriceWithOther = Product.Items
                    //    .Where(item => item.ItemType == ItemTypes.MenuItem)
                    //    .Aggregate(unitPriceWithOther, (current, item) => current + item.UnitPrice);

                    foreach (var item in Product.Items)
                    {
                        unitPriceWithOther += item.UnitPrice;
                        var menuItem = item as BxMenuItem;
                        if (menuItem != null)
                        {
                            unitPriceWithOther += menuItem.Items.Where(subItem => subItem.ItemType != ItemTypes.SubMenuItem).Sum(subItem => subItem.UnitPrice);
                        }
                    }
                }
                unitPriceWithOther = unitPriceWithOther * Quantity;
                return unitPriceWithOther;
            }
        }

        public bool ExclOnlineDiscount
        {
            get
            {
                if ((Product != null) && (Product.Category != null))
                {
                    return Product.Category.ExclOnlineDiscount;
                }
                return false;
            }
        }

        public BxCartItem(int dealID, int menuItemID, int subMenuItemID, int dressingID, int toppingID)
        {
            CartId = Guid.NewGuid().ToString();

            _dealId = dealID;
            _menuItemId = menuItemID;
            _subMenuItemId = subMenuItemID;
            _dressingId = dressingID;
            _toppingId = toppingID;
        }

        public void Load(bool isDeliveryOrder)
        {
            _isDeliveryOrder = isDeliveryOrder;

            // CartItem -> Product -> DealItem
            Product = new BxDealItem(_dealId);
            if (_dealId > 0)
            {
                Product.Load(_isDeliveryOrder);
            }

            // CartItem -> Product -> DealItem -> (*) MenuItem
            if (_menuItemId > 0)
            {
                var item = new BxMenuItem(_menuItemId) {};

                item.Load(_isDeliveryOrder);
                //item.Name = "asd";
                if (!string.IsNullOrWhiteSpace(item.Name) && !Product.Contains(item))
                {
                    Product.Items.Add(item);
                }
            }

            // CartItem -> Product -> DealItem -> (*) MenuItem -> (*) SubMenuItem
            if (_subMenuItemId != 0)
            {
                var subItem = new SubMenuItem(_menuItemId, _subMenuItemId);
                subItem.Load(_isDeliveryOrder);
                if (!string.IsNullOrWhiteSpace(subItem.Name) && !Product.GetMenuItem(_menuItemId).Contains(subItem))
                {
                    Product.GetMenuItem(_menuItemId).Items.Add(subItem);
                    if ((subItem.UnitPrice > 0))
                    {
                        Product.GetMenuItem(_menuItemId).UnitPrice = subItem.UnitPrice;
                    }
                }
            }

            // Is this add dressing?
            // CartItem -> Product -> DealItem -> (*) MenuItem -> (*) Dressing
            if (_dressingId != 0)
            {
                var dressing = new MenuDressing(_menuItemId, _dressingId);
                dressing.Load(_isDeliveryOrder);
                if (!Product.GetMenuItem(_menuItemId).Contains(dressing))
                {
                    Product.Items.Add(dressing);
                }

            }

            // Is this adding topping?
            // CartItem -> Product -> DealItem -> (*) MenuItem -> (*) Topping
            if (_toppingId != 0)
            {
                var isExcluding = _toppingId < 0;

                var topping = new MenuTopping(_menuItemId, _subMenuItemId, Math.Abs(_toppingId));
                topping.Load(_isDeliveryOrder);

                if (!string.IsNullOrWhiteSpace(topping.Name) && !Product.GetMenuItem(_menuItemId).Contains(topping))
                {
                    if (isExcluding)
                    {
                        topping.Name = "NOT " + topping.Name;
                    }

                    Product.Items.Add(topping);
                }
            }
        }

        public bool DeepEquals(int dealId, int menuId, int subMenuId, int[] toppingIds, int[] dressingIds, int[] optionIds)
        {
            // Firstly, check the DealID
            bool blnIsEquals = dealId == DealID;
            if (!blnIsEquals)
                return false;

            // Secondly, check the collection of menu-item
            bool isExistedMenuItem = false;
            foreach (var item in Product.Items)
            {
                if (item.ID == menuId)
                {
                    isExistedMenuItem = true;
                    break;
                }
            }
            if (!isExistedMenuItem)
                return false;


            // Thirdly, check the sub-menu/dressing/topping items
            List<int> toppingList = toppingIds != null && toppingIds.Length > 0 ? new List<int>(toppingIds) : new List<int>();
            List<int> dressingList = dressingIds != null && dressingIds.Length > 0 ? new List<int>(dressingIds) : new List<int>();
            List<int> optionList = optionIds != null && optionIds.Length > 0 ? new List<int>(optionIds) : new List<int>();

            foreach (var item in Product.Items)
            {
                var menuItem = item as BxMenuItem;
                if (menuItem == null)
                    continue;

                bool hasTheSameSubMenuItem = true;
                bool hasTheSameDressing = dressingList.Count == menuItem.CountOf(ItemTypes.MenuDressing);
                bool hasTheSameTopping = toppingList.Count == menuItem.CountOf(ItemTypes.MenuTopping);
                bool hasTheSameOptions = optionList.Count == menuItem.CountOf(ItemTypes.MenuOption);

                if (menuItem.Items.Count > 0)
                {
                    foreach (var subItem in menuItem.Items)
                    {
                        if (subItem.ItemType == ItemTypes.SubMenuItem)
                        {
                            hasTheSameSubMenuItem = subItem.ID == subMenuId;
                        }
                        else if (subItem.ItemType == ItemTypes.MenuTopping)
                        {
                            hasTheSameTopping &= toppingList.Contains(subItem.ID);
                        }
                        else if (subItem.ItemType == ItemTypes.MenuDressing)
                        {
                            hasTheSameDressing &= dressingList.Contains(subItem.ID);
                        }
                        else if (subItem.ItemType == ItemTypes.MenuOption)
                        {
                            hasTheSameOptions &= optionList.Contains(subItem.ID);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    // Is not the item existed?
                    if (hasTheSameSubMenuItem & hasTheSameDressing & hasTheSameTopping & hasTheSameOptions)
                    {
                        isExistedMenuItem = true;
                        break;
                    }
                    else
                    {
                        isExistedMenuItem = false;
                    }
                }
                else
                {
                    isExistedMenuItem = true;
                }
            }
            return isExistedMenuItem;
        }
    }

    public class SubCartItem
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public string CartID { get; set; }

        public int DealID { get; set; }

        public string isWhatSI { get; set; }
       
        public SubCartItem()
        {
            ItemType = ItemTypes.Undefined;
        }

        public ItemTypes ItemType { get; set; }

        private List<SubCartItem> _items;

        /// <summary>
        /// Holds sub item in the case the ItemType is MenuItem
        /// </summary>
        public List<SubCartItem> Items
        {
            get { return _items ?? (_items = new List<SubCartItem>()); }
            private set { _items = value; }
        }

        public override bool Equals(object obj)
        {
            return ((obj is SubCartItem) && ID == ((SubCartItem)obj).ID);
        }
    }
}