using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class MenuCategoryProvider : DataAccess
    {
        private static MenuCategoryProvider _instance;

        private MenuCategoryProvider()
        {
        }

        static MenuCategoryProvider()
        {
            _instance = new MenuCategoryProvider();
        }

        //public static MenuCategoryProvider Instance
        //{
        //    get { return _instance ?? (_instance = new MenuCategoryProvider()); }
        //}

        public static MenuCategoryProvider GetInstance(string connectionString)
        {
            ConnectionString = connectionString;
            return _instance ?? (_instance = new MenuCategoryProvider());
        }

        public IEnumerable<CsMenuCategory> GetAllMenuCategories()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                return session.FindAll<CsMenuCategory>();
            }
        }

        public IEnumerable<CsMenuCategory> GetNormalMenuCategories()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var allCategories = session.FindAll<CsMenuCategory>().AsQueryable();
                if (allCategories.Any())
                {
                    var validCategories = allCategories.Where(c => c.IsActive && !c.IsAvailableForDeal && !c.IsDeal)
                                                       .OrderBy(c => c.ItemPosition).AsEnumerable();
                    return validCategories;

                }
                return null;
            }
        }

        public IEnumerable<CsMenuCategory> GetDealsMenuCategories()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var allCategories = session.FindAll<CsMenuCategory>().AsQueryable();
                if (allCategories.Any())
                {
                    var validCategories = allCategories.Where(c => c.IsActive && !c.IsAvailableForDeal && c.IsDeal)
                                                       .OrderBy(c => c.ItemPosition).AsEnumerable();
                    return validCategories;

                }
                return null;
            }
        }

        public CsMenuCategory GetMenuCategoryByID(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                return session.Get<CsMenuCategory>(id);
            }
        }

        public CsMenuCategory SaveMenuCategory(CsMenuCategory menu)
        {
            CsMenuCategory savedMenuCategory = null;
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                savedMenuCategory = menu.ID == 0 ? session.Insert(menu) : session.Update(menu);
                tranx.Commit();
            }
            return savedMenuCategory;
        }

        public bool DeleteMenuCategory(CsMenuCategory menu)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                session.Delete(menu);
                tranx.Commit();
                return true;
            }
        }
    }
}
