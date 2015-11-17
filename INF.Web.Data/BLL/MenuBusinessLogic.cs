using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.DAL.SqlClient;
using INF.Web.Data.Entities;

namespace INF.Web.Data.BLL
{
    public class MenuBusinessLogic : BaseBusinessLogic
    {
        private readonly string _connectionString;
        public MenuBusinessLogic()
        { }

        public MenuBusinessLogic(string connectionString)
        {
            _connectionString = connectionString;
        }

        public String GetTreeCategory(int catId, int itemId, int subId)
        {
            var str = "<ol class='tree'>";
            var menuCategories = GetAllMenuCategories();
            var check = "";
            foreach (var csMenuCategory in menuCategories)
            {
                check = (catId > 0 && catId == csMenuCategory.ID) ? "checked" : "";
                str += "<li><label><a href='Category.aspx?CatID=" + csMenuCategory.ID + "'>" + csMenuCategory.Name + "</a></label><input type='checkbox' " + check + " />";
                var menuItems = GetMenuItemsByCategory(csMenuCategory.ID);
                var csMenuItems = menuItems as IList<CsMenuItem> ?? menuItems.ToList();
                if (csMenuItems.Any())
                {
                    str += "<ol>";
                    foreach (var csMenuItem in csMenuItems)
                    {
                        check = (itemId > 0 && itemId == csMenuItem.ID) ? "checked" : "";
                        str += "<li><label><a href='MenuItem.aspx?CatID=" + csMenuCategory.ID + "&ItemID=" + csMenuItem.ID + "'>" + csMenuItem.Name + "</a></label> <input type='checkbox' " + check + " />";
                        var subMenuItems = GetSubMenuItemsByMenuID(csMenuItem.ID);
                        var csSubMenuItems = subMenuItems as IList<CsSubMenuItem> ?? subMenuItems.ToList();
                        if (csSubMenuItems.Any())
                        {
                            str += "<ol>";
                            foreach (var csSubMenuItem in csSubMenuItems)
                            {
                                str += "<li class='file'><a href='SubMenuItem.aspx?CatID=" + csMenuCategory.ID + "&ItemID=" + csMenuItem.ID + "&SubID=" + csSubMenuItem.ID + "'>" + csSubMenuItem.Name + "</a></li>";
                            }
                            str += "</ol>";
                        }
                        str += "</li>";
                    }
                    str += "</ol>";
                }
                str += "</li>";
            }
            str += "</ol>";
            return str;
        }

        public IEnumerable<CsMenuCategory> GetAllMenuCategories()
        {
            string key = string.Format("AllMenuCategories_{0}", 0);
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[key] != null)
            {
                return (IEnumerable<CsMenuCategory>)Cache[key];
            }
            var menuCategories = MenuCategoryProvider.GetInstance(_connectionString).GetAllMenuCategories();
            CacheData(key, menuCategories);
            return menuCategories;
        }

        public IEnumerable<CsMenuCategory> GetNormalMenuCategories()
        {
            string key = string.Format("AllNormalMenuCategories_{0}", 0);
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[key] != null)
            {
                return (IEnumerable<CsMenuCategory>)Cache[key];
            }
            var menuCategories = MenuCategoryProvider.GetInstance(_connectionString).GetNormalMenuCategories().ToList();
            foreach (var mc in menuCategories)
            {
                if (mc.NormalImage == null)
                {
                    mc.NormalImage = string.Empty;
                }
                if (mc.MouseOverImage == null)
                {
                    mc.MouseOverImage = string.Empty;
                }
            }
            CacheData(key, menuCategories);
            return menuCategories;
        }

        public IEnumerable<CsMenuCategory> GetDealsMenuCategories()
        {
            string key = string.Format("GetDealsMenuCategories_{0}", 0);
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[key] != null)
            {
                return (IEnumerable<CsMenuCategory>)Cache[key];
            }
            var menuCategories = MenuCategoryProvider.GetInstance(_connectionString).GetDealsMenuCategories();
            CacheData(key, menuCategories);
            return menuCategories;
        }

        public CsMenuCategory GetMenuCategoryByID(int id)
        {
            string key = string.Format("MenuCategoryByID_{0}", id);
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[key] != null)
            {
                return (CsMenuCategory)Cache[key];
            }

            var menu = MenuCategoryProvider.GetInstance(_connectionString).GetMenuCategoryByID(id);
            CacheData(key, menu);
            return menu;
        }

        public bool SaveMenuCategory(CsMenuCategory menu)
        {
            var savedMenuCategory = MenuCategoryProvider.GetInstance(_connectionString).SaveMenuCategory(menu);
            if (savedMenuCategory != null)
            {
                var key = string.Format("MenuCategoryByID_{0}", menu.ID);
                if (WebSettings.Instance.EnaleCachingMasterData && Cache[key] != null)
                {
                    Cache[key] = savedMenuCategory;
                }
            }
            return savedMenuCategory != null;
        }

        public bool DeleteMenuCategory(CsMenuCategory menu)
        {
            var deleted = MenuCategoryProvider.GetInstance(_connectionString).DeleteMenuCategory(menu);
            if (deleted)
            {
                string key = string.Format("MenuCategoryByID_{0}", menu.ID);
                if (WebSettings.Instance.EnaleCachingMasterData && Cache[key] != null)
                {
                    PurgeCacheItems("MenuCategoryByID_");
                }
            }
            return deleted;
        }

        public IEnumerable<CsMenuItem> GetAllMenuItems()
        {
            return MenuItemProvider.GetInstance(_connectionString).GetAllMenuItems();
        }

        public IEnumerable<CsMenuItem> GetMenuItemsByCategory(decimal categoryId)
        {
            string key = string.Format("MenuItemsByCategoryID_{0}", categoryId);
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[key] != null)
            {
                return (IEnumerable<CsMenuItem>)Cache[key];
            }

            var items = MenuItemProvider.GetInstance(_connectionString).GetMenuItemsByCategory(categoryId);
            CacheData(key, items);
            return items;
        }

        public CsMenuItem GetMenuItem(int id)
        {
            return MenuItemProvider.GetInstance(_connectionString).GetMenuItemByID(id);
        }

        public CsMenuItem GetMenuItemByName(int categoryId, string name, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            return MenuItemProvider.GetInstance(_connectionString).GetMenuItemByName(categoryId,name, ignoreCase);
        }

        public bool DeleteMenuItem(int id)
        {
            return MenuItemProvider.GetInstance(_connectionString).DeleteMenuItem(id);
        }

        public IEnumerable<CsSubMenuItem> GetSubMenuItemsByMenuID(decimal menuId)
        {
            string key = string.Format("SubMenuItemsByMenuID_{0}", menuId);
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[key] != null)
            {
                return (IEnumerable<CsSubMenuItem>)Cache[key];
            }

            var items = MenuItemProvider.GetInstance(_connectionString).GetSubMenuItems(menuId);
            CacheData(key, items);
            return items;
        }

        public CsSubMenuItem GetSubMenuItemByID(decimal subMenuId)
        {
            return MenuItemProvider.GetInstance(_connectionString).GetSubMenuItemByID(subMenuId);
        }

        public bool DeleteSubMenuItem(int subItemId)
        {
            return MenuItemProvider.GetInstance(_connectionString).DeleteSubMenuItem(subItemId);
        }

        public IEnumerable<CsBaseSelection> GetBaseSelections(int index)
        {
            string key = string.Format("BaseSelectionsBySelectedIndex_{0}", index);
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[key] != null)
            {
                return (IEnumerable<CsBaseSelection>)Cache[key];
            }

            var result = MenuItemProvider.GetInstance(_connectionString).GetBaseSelections(index);
            CacheData(key, result);
            return result;
        }

        public CsSubMenuItem GetSubMenuItem(int id)
        {
            return MenuItemProvider.GetInstance(_connectionString).GetSubMenuItem(id);
        }

        public bool SaveSubMenuItem(CsSubMenuItem subMenuItem)
        {
            return MenuItemProvider.GetInstance(_connectionString).SaveSubMenuItem(subMenuItem) != null;
        }

        public bool SaveMenuItem(CsMenuItem menuItem)
        {
            return MenuItemProvider.GetInstance(_connectionString).SaveMenuItem(menuItem) != null;
        }

        public CsDealDetail SaveDealDetail(CsDealDetail detail)
        {
            return MenuItemProvider.GetInstance(_connectionString).SaveDealDetail(detail);
        }

        public bool DeleteDealDetail(int id)
        {
            return MenuItemProvider.GetInstance(_connectionString).DeleteDealDetail(new CsDealDetail { ID = id });
        }

        public CsDealDetail GetDealDetailById(int id, bool includeLinkedMenu)
        {
            string cachedKey = string.Format("GetDealDetailById_{0}_IncludeLinkedMenu_{1}", id, includeLinkedMenu);
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[cachedKey] != null)
            {
                return (CsDealDetail)Cache[cachedKey];
            }

            var item = MenuItemProvider.GetInstance(_connectionString).GetDealDetailById(id, includeLinkedMenu);
            CacheData(cachedKey, item);
            return item;
        }

        public CsDealDetail[] GetDealDetailsByCategory(int categoryId, bool includeLinkedMenu)
        {
            string cachedKey = string.Format("GetDealDetailsByCategory_{0}", categoryId);
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[cachedKey] != null)
            {
                return (CsDealDetail[])Cache[cachedKey];
            }

            var details = MenuItemProvider.GetInstance(_connectionString).GetDealDetails(categoryId, includeLinkedMenu);
            var items = new CsDealDetail[] { };
            if (details != null)
            {
                items = details.ToArray();
            }
            CacheData(cachedKey, items);
            return items;
        }

        public IEnumerable<CsMenuOption> GetAllMenuOptions()
        {
            string cachedKey = string.Format("GetAllMenuOptions_{0}", "All");
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[cachedKey] != null)
            {
                return (IEnumerable<CsMenuOption>) Cache[cachedKey];
            }

            var items = MenuOptionsProvider.GetInstance(_connectionString).GetAllMenuOptions();
            CacheData(cachedKey, items);
            return items;
        }

        public IEnumerable<CsOptionDetail> GetOptionDetails(int optionID)
        {
            string cachedKey = string.Format("GetOptionDetails_{0}", "optionID");
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[cachedKey] != null)
            {
                return (IEnumerable<CsOptionDetail>)Cache[cachedKey];
            }

            var items = MenuOptionsProvider.GetInstance(_connectionString).GetOptionDetails(optionID);
            CacheData(cachedKey, items);
            return items;
        }

        public CsOptionDetail GetOptionDetailByID(int id )
        {
            string cachedKey = string.Format("GetOptionDetailByID_{0}", id);
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[cachedKey] != null)
            {
                return (CsOptionDetail)Cache[cachedKey];
            }

            var item = MenuOptionsProvider.GetInstance(_connectionString).GetOptionDetailByID(id);
            CacheData(cachedKey, item);
            return item;
        }

        #region "Menu Dressing"

        public IEnumerable<CsMenuDressing> GetAllMenuDressings()
        {
            string cachedKey = string.Format("GetAllMenuDressings");
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[cachedKey] != null)
            {
                return (IEnumerable<CsMenuDressing>)Cache[cachedKey];
            }

            var item = MenuDressingProvider.GetInstance(_connectionString).GetAllDressing();
            CacheData(cachedKey, item);
            return item;
        }

        public CsMenuDressing GetDressingByID(int id)
        {
            string cachedKey = string.Format("GetDressingByID_{0}", id);
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[cachedKey] != null)
            {
                return (CsMenuDressing)Cache[cachedKey];
            }

            var item = MenuDressingProvider.GetInstance(_connectionString).GetDressingByID(id);
            CacheData(cachedKey, item);
            return item;
        }

        #endregion

        public CsSubMenuItem FindSubMenuItemByName(int id, string name)
        {
            return MenuItemProvider.GetInstance(_connectionString).FindSubMenuItemByName(id, name);            
        }
    }
}
