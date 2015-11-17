using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class MenuItemProvider : DataAccess
    {
        private static MenuItemProvider _instance;

        MenuItemProvider()
        {
        }

        static MenuItemProvider()
        {
            _instance = new MenuItemProvider();
        }

        //public static MenuItemProvider Instance
        //{
        //    get { return _instance ?? (_instance = new MenuItemProvider()); }
        //}

        public static MenuItemProvider GetInstance(string connectionString)
        {
            ConnectionString = connectionString;
            return _instance ?? (_instance = new MenuItemProvider());
        }

        public IEnumerable<CsMenuItem> GetAllMenuItems()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                //var query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = " + categoryId + " ORDER BY [ItemPosition]");
                //return query.GetResults<CsMenuItem>();
                return session.FindAll<CsMenuItem>();
            }
        }

        public IEnumerable<CsMenuItem> GetMenuItemsByCategory(decimal categoryId)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = " + categoryId + " ORDER BY [ItemPosition]");
                return query.GetResults<CsMenuItem>();
            }
        }

        public CsMenuItem GetMenuItemByID(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var item= session.Get<CsMenuItem>(id);
                if (item != null && item.CategoryID > 0)
                {
                    item.Category = session.Get<CsMenuCategory>(item.CategoryID);
                }
                return item;
            }
        }

        public bool DeleteMenuItem(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var itemToDelete = session.Get<CsMenuItem>(id);
                if (itemToDelete == null)
                    return false;

                var tranx = session.GetTransaction();
                try
                {
                    session.Delete(itemToDelete);
                    tranx.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tranx.Rollback();
                    throw;
                }
            }   
        }

        public CsMenuItem GetMenuItemByName(int categoryId, string name, bool ignoreCase)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var nameToTest = ignoreCase ? name.ToUpper() : name;
                var query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = " + categoryId + " AND UPPER([Name]) = '" + nameToTest + "' ORDER BY [ItemPosition]");
                return query.GetResults<CsMenuItem>().FirstOrDefault();
            }
        }

        public IEnumerable<CsSubMenuItem> GetSubMenuItems(decimal menuId)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsSubMenuItem>(" WHERE [IsActive] = 'True' AND [Menu_Id] = " + menuId + " ORDER BY [ItemPosition]");
                return query.GetResults<CsSubMenuItem>();
            }
        }

        public CsSubMenuItem GetSubMenuItemByID(decimal id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var subMenuItem = session.Get<CsSubMenuItem>(id);
                if (subMenuItem != null && subMenuItem.MenuID > 0)
                {
                    subMenuItem.MenuItem = session.Get<CsMenuItem>(subMenuItem.MenuID);
                }
                return subMenuItem;
            }
        }

        public IEnumerable<CsBaseSelection> GetBaseSelections(int index)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsBaseSelection>(" WHERE [SelectedIndex] = " + index + "");
                return query.GetResults<CsBaseSelection>();
            }
        }

        public CsMenuItem SaveMenuItem(CsMenuItem menuItem)
        {
            CsMenuItem savedMenuItem = null;
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                savedMenuItem = menuItem.ID == 0 ? session.Insert(menuItem) : session.Update(menuItem);
                tranx.Commit();
            }
            return savedMenuItem;
        }

        public CsSubMenuItem SaveSubMenuItem(CsSubMenuItem subMenuItem)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    var savedItem = subMenuItem.ID == 0 ? session.Insert(subMenuItem) : session.Update(subMenuItem);
                    tranx.Commit();
                    return savedItem;
                }
                catch (Exception)
                {
                    tranx.Rollback();
                    throw;
                }
            }
        }

        public CsSubMenuItem GetSubMenuItem(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                return session.Get<CsSubMenuItem>(id);
            }
        }

        public IEnumerable<CsDealDetail> GetDealDetails(int categoryId, bool includeLinkedMenu)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsDealDetail>(" WHERE [IsActive] = 1 AND [CategoryID] = @CategoryId ORDER BY [Position]");
                query.AddParameter("@CategoryId", categoryId, DbType.Int32);
                var details = query.GetResults<CsDealDetail>();
                if (includeLinkedMenu)
                {
                    if (details != null)
                    {
                        foreach (var detail in details)
                        {
                            if (detail != null && detail.ID > 0)
                            {
                                if (detail.OptionEnabled1 && detail.LinkedMenu1 > 0)
                                {
                                    query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                                    query.AddParameter("@CategoryId", detail.LinkedMenu1, DbType.Int32);
                                    detail.LinkedMenuList1 = query.GetResults<CsMenuItem>().ToList();
                                    //detail.LinkedMenuList1 = session.Get<CsMenuCategory>(detail.LinkedMenu1);
                                }

                                if (detail.OptionEnabled2 && detail.LinkedMenu2 > 0)
                                {
                                    query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                                    query.AddParameter("@CategoryId", detail.LinkedMenu2, DbType.Int32);
                                    detail.LinkedMenuList2 = query.GetResults<CsMenuItem>().ToList();
                                    //detail.LinkedMenuCategory2 = session.Get<CsMenuCategory>(detail.LinkedMenu2);
                                }

                                if (detail.OptionEnabled3 && detail.LinkedMenu3 > 0)
                                {
                                    query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                                    query.AddParameter("@CategoryId", detail.LinkedMenu3, DbType.Int32);
                                    detail.LinkedMenuList3 = query.GetResults<CsMenuItem>().ToList();
                                    //detail.LinkedMenuCategory3 = session.Get<CsMenuCategory>(detail.LinkedMenu3);
                                }

                                if (detail.OptionEnabled4 && detail.LinkedMenu4 > 0)
                                {
                                    query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                                    query.AddParameter("@CategoryId", detail.LinkedMenu4, DbType.Int32);
                                    detail.LinkedMenuList4 = query.GetResults<CsMenuItem>().ToList();
                                    //detail.LinkedMenuCategory4 = session.Get<CsMenuCategory>(detail.LinkedMenu4);
                                }

                                if (detail.OptionEnabled5 && detail.LinkedMenu5 > 0)
                                {
                                    query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                                    query.AddParameter("@CategoryId", detail.LinkedMenu5, DbType.Int32);
                                    detail.LinkedMenuList5 = query.GetResults<CsMenuItem>().ToList();
                                    //detail.LinkedMenuCategory5 = session.Get<CsMenuCategory>(detail.LinkedMenu5);
                                }

                                if (detail.OptionEnabled6 && detail.LinkedMenu6 > 0)
                                {
                                    query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                                    query.AddParameter("@CategoryId", detail.LinkedMenu6, DbType.Int32);
                                    detail.LinkedMenuList6 = query.GetResults<CsMenuItem>().ToList();
                                    //detail.LinkedMenuCategory6 = session.Get<CsMenuCategory>(detail.LinkedMenu6);
                                }

                                if (detail.OptionEnabled7 && detail.LinkedMenu7 > 0)
                                {
                                    query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                                    query.AddParameter("@CategoryId", detail.LinkedMenu7, DbType.Int32);
                                    detail.LinkedMenuList7 = query.GetResults<CsMenuItem>().ToList();
                                    //detail.LinkedMenuCategory7 = session.Get<CsMenuCategory>(detail.LinkedMenu7);
                                }

                                if (detail.OptionEnabled8 && detail.LinkedMenu8 > 0)
                                {
                                    query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                                    query.AddParameter("@CategoryId", detail.LinkedMenu8, DbType.Int32);
                                    detail.LinkedMenuList8 = query.GetResults<CsMenuItem>().ToList();
                                    //detail.LinkedMenuCategory8 = session.Get<CsMenuCategory>(detail.LinkedMenu8);
                                }

                                if (detail.OptionEnabled9 && detail.LinkedMenu9 > 0)
                                {
                                    query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                                    query.AddParameter("@CategoryId", detail.LinkedMenu9, DbType.Int32);
                                    detail.LinkedMenuList9 = query.GetResults<CsMenuItem>().ToList();
                                    //detail.LinkedMenuCategory9 = session.Get<CsMenuCategory>(detail.LinkedMenu9);
                                }
                            }
                        }
                    }
                }

                return details;
            }
        }

        public CsDealDetail GetDealDetailById(int id, bool includeLinkedMenu)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsDealDetail>("WHERE [ID] = @id");
                query.AddParameter("@id", id, DbType.Int32);
                var detail = query.GetSingleResult<CsDealDetail>();
                if (detail != null && detail.ID > 0)
                {
                    if (includeLinkedMenu)
                    {
                        if (detail.OptionEnabled1 && detail.LinkedMenu1 > 0)
                        {
                            query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                            query.AddParameter("@CategoryId", detail.LinkedMenu1, DbType.Int32);
                            detail.LinkedMenuList1 = query.GetResults<CsMenuItem>().ToList();
                            //detail.LinkedMenuList1 = session.Get<CsMenuCategory>(detail.LinkedMenu1);
                        }

                        if (detail.OptionEnabled2 && detail.LinkedMenu2 > 0)
                        {
                            query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                            query.AddParameter("@CategoryId", detail.LinkedMenu2, DbType.Int32);
                            detail.LinkedMenuList2 = query.GetResults<CsMenuItem>().ToList();
                            //detail.LinkedMenuCategory2 = session.Get<CsMenuCategory>(detail.LinkedMenu2);
                        }

                        if (detail.OptionEnabled3 && detail.LinkedMenu3 > 0)
                        {
                            query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                            query.AddParameter("@CategoryId", detail.LinkedMenu3, DbType.Int32);
                            detail.LinkedMenuList3 = query.GetResults<CsMenuItem>().ToList();
                            //detail.LinkedMenuCategory3 = session.Get<CsMenuCategory>(detail.LinkedMenu3);
                        }

                        if (detail.OptionEnabled4 && detail.LinkedMenu4 > 0)
                        {
                            query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                            query.AddParameter("@CategoryId", detail.LinkedMenu4, DbType.Int32);
                            detail.LinkedMenuList4 = query.GetResults<CsMenuItem>().ToList();
                            //detail.LinkedMenuCategory4 = session.Get<CsMenuCategory>(detail.LinkedMenu4);
                        }

                        if (detail.OptionEnabled5 && detail.LinkedMenu5 > 0)
                        {
                            query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                            query.AddParameter("@CategoryId", detail.LinkedMenu5, DbType.Int32);
                            detail.LinkedMenuList5 = query.GetResults<CsMenuItem>().ToList();
                            //detail.LinkedMenuCategory5 = session.Get<CsMenuCategory>(detail.LinkedMenu5);
                        }

                        if (detail.OptionEnabled6 && detail.LinkedMenu6 > 0)
                        {
                            query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                            query.AddParameter("@CategoryId", detail.LinkedMenu6, DbType.Int32);
                            detail.LinkedMenuList6 = query.GetResults<CsMenuItem>().ToList();
                            //detail.LinkedMenuCategory6 = session.Get<CsMenuCategory>(detail.LinkedMenu6);
                        }

                        if (detail.OptionEnabled7 && detail.LinkedMenu7 > 0)
                        {
                            query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                            query.AddParameter("@CategoryId", detail.LinkedMenu7, DbType.Int32);
                            detail.LinkedMenuList7 = query.GetResults<CsMenuItem>().ToList();
                            //detail.LinkedMenuCategory7 = session.Get<CsMenuCategory>(detail.LinkedMenu7);
                        }

                        if (detail.OptionEnabled8 && detail.LinkedMenu8 > 0)
                        {
                            query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                            query.AddParameter("@CategoryId", detail.LinkedMenu8, DbType.Int32);
                            detail.LinkedMenuList8 = query.GetResults<CsMenuItem>().ToList();
                            //detail.LinkedMenuCategory8 = session.Get<CsMenuCategory>(detail.LinkedMenu8);
                        }

                        if (detail.OptionEnabled9 && detail.LinkedMenu9 > 0)
                        {
                            query = session.CreateQuery<CsMenuItem>(" WHERE [Category_Id] = @CategoryId AND [IsActive] = 1 ORDER BY [ItemPosition]");
                            query.AddParameter("@CategoryId", detail.LinkedMenu9, DbType.Int32);
                            detail.LinkedMenuList9 = query.GetResults<CsMenuItem>().ToList();
                            //detail.LinkedMenuCategory9 = session.Get<CsMenuCategory>(detail.LinkedMenu9);
                        }
                    }
                }
                return detail;
            }
        }

        public CsDealDetail SaveDealDetail(CsDealDetail detail)
        {
            if (detail == null)
                return null;

            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    var savedItem = detail.ID > 0 ? session.Update(detail) : session.Insert(detail);
                    tranx.Commit();
                    return savedItem;
                }
                catch (Exception)
                {
                    tranx.Rollback();
                    throw;
                }
            }
        }

        public bool DeleteDealDetail(CsDealDetail detail)
        {
            if (detail == null)
                return false;

            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var detailToDelete = session.Get<CsDealDetail>(detail.ID);
                if (detailToDelete != null && detailToDelete.ID > 0)
                {
                    var tranx = session.GetTransaction();
                    try
                    {
                        session.Delete(detailToDelete);
                        tranx.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        tranx.Rollback();
                        throw;
                    }
                }
            }
            return false;
        }

        public CsSubMenuItem FindSubMenuItemByName(int id, string name)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsSubMenuItem>(" WHERE UPPER([Name]) = @Name AND [Menu_Id] = @MenuId");
                query.AddParameter("@Name", name.ToUpper(), DbType.String);
                query.AddParameter("@MenuId", id, DbType.Int32);
                var result = query.GetResults<CsSubMenuItem>().FirstOrDefault();
                return result;
            }
        }

        public bool DeleteSubMenuItem(int subItemId)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var itemToDelete = session.Get<CsSubMenuItem>(subItemId);
                if (itemToDelete == null)
                    return false;

                var tranx = session.GetTransaction();
                try
                {
                    session.Delete<CsSubMenuItem>(itemToDelete);
                    tranx.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tranx.Rollback();
                    throw;
                }
            }   
        }
    }
}
