using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using INF.Database;
using INF.Web.Data;
using INF.Web.Data.BLL;
using INF.Web.Data.Entities;
using INF.Web.UI.Settings;

namespace INF.Web.UI.Shopping
{
    public enum ItemTypes
    {
        Undefined = 0,
        DealItem = 1,
        MenuItem = 2,
        SubMenuItem = 3,
        MenuDressing = 4,
        MenuTopping = 5,
        MenuOption = 6
    }

    public class GenericItem : IEquatable<GenericItem>
    {
        public ItemTypes ItemType { get; set; }
        public int ID { get; set; }
        public decimal UnitPrice { get; set; }
        public string Name { get; set; }
        public CsMenuCategory Category { get; set; }

        public GenericItem(int id)
        {
            ItemType = ItemTypes.Undefined;
            ID = id;
        }

        public virtual void Load(bool isDeliveryOrder)
        {
        }

        public bool Equals(GenericItem other)
        {
            return (ItemType == other.ItemType & ID == other.ID);
        }

        private string _connectionString;
        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    GetDbConnectionString();
                }
                return _connectionString;
            }
        }

        private void GetDbConnectionString()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PizzaWebConnectionString"].ConnectionString;
        }
    }

    public class BxDealItem : GenericItem
    {
        private List<GenericItem> _items;
        public List<GenericItem> Items
        {
            get { return _items ?? (_items = new List<GenericItem>()); }
        }

        public BxDealItem(int id)
            : base(id)
        {
            ItemType = ItemTypes.DealItem;
        }

        public override void Load(bool isDeliveryOrder)
        {
            var bzMenu = new MenuBusinessLogic(ConnectionString);
            var deal = bzMenu.GetDealDetailById(ID,false);
            if (deal != null)
            {
                Name = deal.Name;
                UnitPrice = isDeliveryOrder ? deal.DeliveryUnitPrice : deal.CollectionUnitPrice;
                Category = bzMenu.GetMenuCategoryByID(deal.CategoryID);
            }
        }

        public bool Contains(BxMenuItem item)
        {
            return _items != null && _items.Any(tempItem => tempItem.Equals(item));
        }

        public BxMenuItem GetMenuItem(int menuItemID)
        {
            return (from item in Items where (item is BxMenuItem && item.ID == menuItemID) select item as BxMenuItem).FirstOrDefault();
        }
    }

    public class BxMenuItem : GenericItem
    {
        private List<GenericItem> _items;
        public List<GenericItem> Items
        {
            get { return _items ?? (_items = new List<GenericItem>()); }
        }

        //public string isWhatSubItem { get; set; }
        public BxMenuItem(int id)
            : base(id)
        {
            ItemType = ItemTypes.MenuItem;
        }

        public bool Contains(GenericItem item)
        {
            return _items != null && _items.Any(tempItem => tempItem.Equals(item));
        }

        public int CountOf(ItemTypes vItemType)
        {
            if ((Items == null) || Items.Count == 0)
            {
                return 0;
            }
            return Items.AsEnumerable().Count(i => (i.ItemType == vItemType));
        }

        public int CountOfMenuOptions(int vParentOptionId)
        {
            if ((Items == null) || Items.Count == 0)
            {
                return 0;
            }
            return Items.AsEnumerable().Count(i => (i.ItemType == ItemTypes.MenuOption && ((i) is MenuOption)) && ((MenuOption)i).ParentId == vParentOptionId);
        }

        public override void Load(bool isDeliveryOrder)
        {
            var bzMenu = new MenuBusinessLogic(ConnectionString);
            var menuItem = bzMenu.GetMenuItem(ID);
            if (menuItem != null)
            {
                Name = menuItem.Name;
                UnitPrice = isDeliveryOrder ? menuItem.DeliveryPrice : menuItem.CollectionPrice;
                Category = bzMenu.GetMenuCategoryByID(menuItem.CategoryID);
            }
        }
    }

    public class SubMenuItem : GenericItem
    {
        private int _parentID;
        public int ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }

        public virtual CsMenuItem MenuItem { get; set; }

        public SubMenuItem(int parentID, int id)
            : base(id)
        {
            _parentID = parentID;
            ItemType = ItemTypes.SubMenuItem;
        }

        public override void Load(bool isDeliveryOrder)
        {
            var bzMenu = new MenuBusinessLogic(ConnectionString);
            var subMenuItem = bzMenu.GetSubMenuItemByID(ID);
            if (subMenuItem != null)
            {
                Name = subMenuItem.Name;
                UnitPrice = isDeliveryOrder ? (decimal)subMenuItem.DeliveryPrice : (decimal) subMenuItem.CollectionPrice;
                MenuItem = bzMenu.GetMenuItem((int)subMenuItem.MenuID);
            }
        }
    }

    public class MenuTopping : GenericItem
    {
        public int MenuItemID { get; set; }
        public int SubMenuItemID { get; set; }

        public MenuTopping(int menuItemID, int subMenuItemID, int toppingID)
            : base(toppingID)
        {
            MenuItemID = menuItemID;
            SubMenuItemID = subMenuItemID;
            ItemType = ItemTypes.MenuTopping;
        }

        public MenuTopping(int menuItemID, int toppingID)
            : base(toppingID)
        {
            MenuItemID = menuItemID;
            ItemType = ItemTypes.MenuTopping;
        }

        public override void Load(bool isDeliveryOrder)
        {
            var bzTopping = new BzMenuTopping(ConnectionString);
            var tpp = bzTopping.GetMenuTopping(ID);
            if (tpp != null)
            {
                Name = tpp.Name;
                UnitPrice = GetToppingPrice(tpp.CategoryID);
            }
        }

        private decimal GetToppingPrice(int toppingCategoryId)
        {
            var bzTopping = new BzMenuTopping(ConnectionString);
            var allTpps = bzTopping.GetToppingCategories();
            int pos = -1;
            if (allTpps != null)
            {
                var lstTpps = allTpps.ToList();
                for (int index = 0; index < lstTpps.Count; index++)
                {
                    if (lstTpps[index].ID == toppingCategoryId)
                    {
                        pos = index;
                        break;
                    }
                }
            }

            if (pos > -1)
            {
                var bzMenu = new MenuBusinessLogic(ConnectionString);
                if (SubMenuItemID > 0)
                {
                    var subMenuItem = bzMenu.GetSubMenuItemByID(SubMenuItemID);
                    if (subMenuItem != null)
                    {
                        if (pos == 0)
                        {
                            return subMenuItem.ToppingPrice1;
                        }
                        if (pos == 1)
                        {
                            return subMenuItem.ToppingPrice2;
                        }
                        if (pos == 2)
                        {
                            return subMenuItem.ToppingPrice3;
                        }
                    }
                }
                else
                {
                    if (MenuItemID > 0)
                    {
                        var menuItem = bzMenu.GetMenuItem(MenuItemID);
                        if (menuItem != null)
                        {
                            if (pos == 0)
                            {
                                return menuItem.ToppingPrice1;
                            }
                            if (pos == 1)
                            {
                                return menuItem.ToppingPrice2;
                            }
                            if (pos == 2)
                            {
                                return menuItem.ToppingPrice3;
                            }
                        }
                    }
                }
            }
            return 0;
        }
    }

    public class MenuDressing : GenericItem
    {
        public int MenuItemID { get; set; }

        public MenuDressing(int menuItemID, int dressingID)
            : base(dressingID)
        {
            MenuItemID = menuItemID;
            ItemType = ItemTypes.MenuDressing;
        }

        public override void Load(bool isDeliveryOrder)
        {
            var bzMenu = new MenuBusinessLogic(ConnectionString);
            var tpp = bzMenu.GetDressingByID(ID);
            if (tpp != null)
            {
                Name = tpp.Name;
            }
        }
    }

    public class MenuOption : GenericItem
    {
        public int MenuItemID { get; set; }

        public MenuOption(int vItemId, int vOptionId)
            : base(vOptionId)
        {
            MenuItemID = vItemId;
            ItemType = ItemTypes.MenuOption;
        }

        public int GetAllowedItems(int vOptionId)
        {
            if (vOptionId == 0)
                return 0;

            var sessionFactory = DataProvider.GetInstance().CreateSessionFactory();
            try
            {
                using (var session = sessionFactory.CreateSession())
                {
                    var optionType = session.Get<CsMenuOption>(vOptionId);
                    return optionType.ItemsAllowed;
                }
            }
            finally
            {
                sessionFactory = null;
            }
        }

        public int ParentId { get; private set; }

        public override void Load(bool isDeliveryOrder)
        {
            var bzMenu = new MenuBusinessLogic(ConnectionString);
            var optDetail = bzMenu.GetOptionDetailByID(ID);
            if (optDetail != null)
            {
                Name = optDetail.Name;
                UnitPrice = optDetail.UnitPrice;
                ParentId = optDetail.OptionID;
            }
        }

    }
}