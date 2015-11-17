using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using INF.Database.Actions;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class MenuToppingProvider : DataAccess
    {
        private static MenuToppingProvider _Instance;

        MenuToppingProvider()
        {
        }

        static MenuToppingProvider()
        {
            _Instance = new MenuToppingProvider();
        }

        [Obsolete("This funtion is obsolete. Please use the replacement one, GetInstance(string connectionString).")]
        public static MenuToppingProvider Instance
        {
            get { return _Instance ?? (_Instance = new MenuToppingProvider()); }
        }

        public static MenuToppingProvider GetInstance(string connectionString)
        {
            ConnectionString = connectionString;
            return _Instance ?? (_Instance = new MenuToppingProvider() );
        }

        public IEnumerable<CsToppingCategory> GetToppingCategories()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var allMenuOptions = session.FindAll<CsToppingCategory>();
                return allMenuOptions.OrderBy(c => c.Position).ThenBy(c => c.Name);
            }
        }

        public CsToppingCategory GetToppingCategory(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsToppingCategory>(" WHERE [ID] = @ID");
                query.AddParameter("@ID", id, DbType.Int32);
                return query.GetSingleResult<CsToppingCategory>();
            }
        }

        public IEnumerable<CsMenuTopping> GetMenuToppings(int categoryId)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsMenuTopping>(" WHERE [CategoryID] = @CategoryID");
                query.AddParameter("@CategoryID", categoryId, DbType.Int32);
                var toppings = query.GetResults<CsMenuTopping>().OrderBy(t => t.Position).ThenBy(t => t.Name);
                return toppings;
            }
        }

        public IEnumerable<CsMenuTopping> GetAllMenuToppings()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var toppings = session.FindAll<CsMenuTopping>().OrderBy(t => t.Position).ThenBy(t => t.Name);
                return toppings;
            }
        }

        public CsMenuTopping GetMenuTopping(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsMenuTopping>(" WHERE [Topping_Id] = @ID");
                query.AddParameter("@ID", id, DbType.Int32);
                return query.GetSingleResult<CsMenuTopping>();
            }
        }

        public CsToppingCategory SaveToppingCategory(CsToppingCategory toppingCategory)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    var cat = toppingCategory.ID == 0 ? session.Insert(toppingCategory) : session.Update(toppingCategory);
                    tranx.Commit();
                    return cat;
                }
                catch (Exception)
                {
                    tranx.Rollback();
                    throw;
                }
            }
        }

        public bool DeleteToppingCategory(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var catToDel = session.Get<CsToppingCategory>(id);
                if (catToDel == null)
                    return false;

                var tranx = session.GetTransaction();
                try
                {
                    session.Delete(catToDel);
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

        public CsMenuTopping SaveMenuTopping(CsMenuTopping menuTopping)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    var cat = menuTopping.ID == 0 ? session.Insert(menuTopping) : session.Update(menuTopping);
                    tranx.Commit();
                    return cat;
                }
                catch (Exception)
                {
                    tranx.Rollback();
                    throw;
                }
            }
        }

        public bool DeleteMenuTopping(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var catToDel = session.Get<CsMenuTopping>(id);
                if (catToDel == null)
                    return false;

                var tranx = session.GetTransaction();
                try
                {
                    session.Delete(catToDel);
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
