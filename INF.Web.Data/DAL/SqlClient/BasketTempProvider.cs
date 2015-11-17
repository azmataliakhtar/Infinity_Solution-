using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class BasketTempProvider : DataAccess
    {
        private static BasketTempProvider _instance;

        static BasketTempProvider()
        {
            _instance = new BasketTempProvider();
        }

        BasketTempProvider()
        {
        }

        public static BasketTempProvider Instance
        {
            get { return _instance ?? (_instance = new BasketTempProvider()); }
        }

        public IEnumerable<CsBasketTemp> FindCsBasketTemps(bool isIncludeDetails)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var basket = session.FindAll<CsBasketTemp>().AsQueryable();

                // Collect customer address
                foreach (var order in basket)
                {
                    var query = session.CreateQuery<CsCustomerAddress>("WHERE [Address_Id] = @AddressID");
                    query.AddParameter("@AddressID", order.AddressID,DbType.Int32);
                    var address = query.GetSingleResult<CsCustomerAddress>();
                    if (address != null)
                    {
                        order.CustomerAddress = "" + address.PostCode + "-" + address.Address + "-" + address.City;
                    }

                    var customerQuery = session.CreateQuery<CsCustomer>("WHERE [Customer_Id] = @CustomerID");
                    customerQuery.AddParameter("@CustomerID",order.CustomerID, DbType.Int32);
                    var customer = customerQuery.GetSingleResult<CsCustomer>();
                    if (customer != null)
                    {
                        order.CustomerName = customer.LastName + " " + customer.FirstName;
                    }
                }

                if (isIncludeDetails)
                {
                    foreach (var order in basket)
                    {
                        var query = session.CreateQuery<CsBasketItemTemp>(" WHERE [BasketID] = @BasketID");
                        query.AddParameter("@BasketID", order.ID, DbType.Int32);
                        var items = query.GetResults<CsBasketItemTemp>();
                        order.Items = new List<CsBasketItemTemp>(items);
                    }
                }
                
                return basket;
            }
        }

        public CsBasketTemp GetBasketTempByID(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var basket = session.Get<CsBasketTemp>(id);
                if (basket != null && basket.ID > 0)
                {
                    var query = session.CreateQuery<CsBasketItemTemp>(" WHERE [BasketID] = @BasketID");
                    query.AddParameter("@BasketID", basket.ID, DbType.Int32);
                    var items = query.GetResults<CsBasketItemTemp>();
                    basket.Items = new List<CsBasketItemTemp>(items);
                }
                return basket;
            }
        }

        public CsBasketTemp GetBasketTempByLoginUser(string loginUser)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsBasketTemp>(" WHERE [UserLogIn] = @UserLogIn");
                query.AddParameter("@UserLogIn", loginUser, DbType.String);

                var baskets = query.GetResults<CsBasketTemp>().OrderByDescending(b => b.OrderDate);
                var basket = baskets.FirstOrDefault();
                if (basket != null && basket.ID > 0)
                {
                    query = session.CreateQuery<CsBasketItemTemp>(" WHERE [BasketID] = @BasketID");
                    query.AddParameter("@BasketID", basket.ID, DbType.Int32);
                    var items = query.GetResults<CsBasketItemTemp>();
                    basket.Items = new List<CsBasketItemTemp>(items);
                }

                return basket;
            }
        }

        public void UpdateBasketTemp(CsBasketTemp temp,string status)
        {
            if (temp == null)
                return;

            using (var ss = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = ss.GetTransaction();
                try
                {
                    var query = ss.CreateQuery<CsBasketTemp>(" WHERE [ID] = @Id");
                    query.AddParameter("@Id", temp.ID, DbType.Int32);

                    var baskets = query.GetResults<CsBasketTemp>().OrderByDescending(b => b.OrderDate);
                    var basket = baskets.FirstOrDefault();
                    if (basket != null && basket.ID > 0)
                    {
                        basket.SagePayStatus = status;
                        ss.Update(basket);
                        tranx.Commit();
                    }
                }
                catch
                {
                    tranx.Rollback();
                    throw;
                }
            }
        }

        public void UpdateBasketTemp(CsBasketTemp temp)
        {
            if (temp == null)
                return;

            using (var ss = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = ss.GetTransaction();
                try
                {
                    ss.Update(temp);
                    tranx.Commit();
                }
                catch
                {
                    tranx.Rollback();
                    throw;
                }
            }
        }

        public void UpdateSagePayStatus(string loginUser, string status)
        {
            using (var ss = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = ss.GetTransaction();
                try
                {
                    var query = ss.CreateQuery<CsBasketTemp>(" WHERE [UserLogIn] = @UserLogIn");
                    query.AddParameter("@UserLogIn", loginUser, DbType.String);

                    var baskets = query.GetResults<CsBasketTemp>().OrderByDescending(b => b.OrderDate);
                    var basket = baskets.FirstOrDefault();
                    if (basket != null && basket.ID > 0)
                    {
                        basket.SagePayStatus = status;
                        ss.Update(basket);
                        tranx.Commit();
                    }
                }
                catch
                {
                    tranx.Rollback();
                    throw;
                }
            }
        }

        public CsBasketTemp SaveBasketTemp(CsBasketTemp basket)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    CsBasketTemp savedBasket = null;
                    if (basket.ID == 0)
                    {
                        savedBasket = session.Insert(basket);
                        if (basket.Items != null && basket.Items.Count > 0)
                        {
                            foreach (var item in basket.Items)
                            {
                                if (item == null) continue;
                                if (item.ID > 0)
                                {
                                    session.Update(item);
                                }
                                else
                                {
                                    item.BasketID = basket.ID;
                                    session.Insert(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        savedBasket = session.Update(basket);
                        if (basket.Items != null && basket.Items.Count > 0)
                        {
                            foreach (var item in basket.Items)
                            {
                                if (item == null) continue;
                                if (item.ID > 0)
                                {
                                    session.Update(item);
                                }
                                else
                                {
                                    item.BasketID = basket.ID;
                                    session.Insert(item);
                                }
                            }
                        }
                    }
                    tranx.Commit();
                    return savedBasket;
                }
                catch
                {
                    tranx.Rollback();
                    throw;
                }
                finally
                {
                    tranx = null;
                }
            }
        }

        public bool DeleteBasketTemp(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    var basketToDelete = session.Get<CsBasketTemp>(id);
                    if (basketToDelete != null)
                    {
                        var query = session.CreateQuery<CsBasketItemTemp>(" WHERE [BasketID] = @BasketID");
                        query.AddParameter("@BasketID", basketToDelete.ID, DbType.Int32);
                        var items = query.GetResults<CsBasketItemTemp>();
                        basketToDelete.Items = new List<CsBasketItemTemp>(items);

                        if (basketToDelete.Items != null && basketToDelete.Items.Count > 0)
                        {
                            foreach (var item in basketToDelete.Items)
                            {
                                if (item == null) continue;
                                session.Delete(item);
                            }
                        }
                        session.Delete(basketToDelete);
                        tranx.Commit();
                        return true;
                    }
                }
                catch
                {
                    tranx.Rollback();
                    throw;
                }
                finally
                {
                    tranx = null;
                }
                return false;
            }
        }
    }
}
