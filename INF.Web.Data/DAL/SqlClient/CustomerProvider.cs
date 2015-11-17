using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class CustomerProvider : DataAccess
    {
        private static CustomerProvider _instance;

        static CustomerProvider()
        {
            _instance = new CustomerProvider();
        }

        CustomerProvider()
        {
        }

        public static CustomerProvider Instance
        {
            get { return _instance ?? (_instance = new CustomerProvider()); }
        }

        public CsCustomer GetCustomerByID(decimal id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                return session.Get<CsCustomer>(id);
            }
        }

        public CsCustomer GetCustomerByEmail(string email)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsCustomer>(" WHERE [Email] = '" + email + "'");
                return query.GetSingleResult<CsCustomer>();
            }
        }

        public CsCustomer GetCustomerByMobile(string mobile)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsCustomer>(" WHERE [Mobile] = '" + mobile + "'");
                return query.GetSingleResult<CsCustomer>();
            }
        }

        public CsCustomer GetCustomerByTelephone(string telephone)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsCustomer>(" WHERE [Telephone] = '" + telephone + "'");
                return query.GetSingleResult<CsCustomer>();
            }
        }

        public void UnBlockAllCustomer()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var allCustomers = session.FindAll<CsCustomer>();
                var tranx = session.GetTransaction();
                foreach (var customer in allCustomers)
                {
                    customer.IsActive = true;
                    session.Update(customer);
                }
                tranx.Commit();
            }
        }

        public CsCustomer SaveCustomer(CsCustomer customer)
        {
            CsCustomer savedCustomer = null;
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                savedCustomer = customer.ID == 0 ? session.Insert(customer) : session.Update(customer);
                tranx.Commit();
            }
            return savedCustomer;
        }

        public CsCustomerAddress SaveCustomerAddress(CsCustomerAddress address)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    var savedAddress = address.ID > 0 ? session.Update(address) : session.Insert(address);
                    tranx.Commit();
                    return savedAddress;
                }
                catch
                {
                    tranx.Rollback();
                    throw;
                }
            }
        }

        public CsCustomerAddress GetCustomerAddress(decimal addressId)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                return session.Get<CsCustomerAddress>(addressId);
            }
        }

        public IEnumerable<CsCustomerAddress> GetCustomerAddresses(decimal customerId)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsCustomerAddress>(" WHERE [Customer_Id] = " + customerId + " ORDER BY [PostCode]");
                return query.GetResults<CsCustomerAddress>();
            }
        }

        public bool DeleteCustomerAddress(decimal addressId)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    session.Delete(new CsCustomerAddress() {ID = addressId});
                    tranx.Commit();
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
                return true;
            }
        }

        public IEnumerable<CsCustomer> GetAllCustomers()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                return session.FindAll<CsCustomer>();
            }
        }
    }
}
