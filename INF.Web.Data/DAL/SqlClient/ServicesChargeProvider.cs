using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class ServicesChargeProvider : DataAccess
    {
        private static ServicesChargeProvider _instance;

        ServicesChargeProvider()
        {
        }

        static ServicesChargeProvider()
        {
            _instance = new ServicesChargeProvider();
        }

        public static ServicesChargeProvider Instance
        {
            get { return _instance ?? (_instance = new ServicesChargeProvider()); }
        }

        public CsServicesCharge GetServicesCharge(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                return session.Get<CsServicesCharge>(id);
            }
        }

        public IEnumerable<CsServicesCharge> GetEnabledServicesCharge()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsServicesCharge>(" WHERE [IsActived] = 1 ORDER BY [Name]");
                return query.GetResults<CsServicesCharge>();
            }
        }

        public IEnumerable<CsServicesCharge> GetAllServicesCharge()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                return session.FindAll<CsServicesCharge>();
            }
        }

        public CsServicesCharge SaveServicesCharge(CsServicesCharge services)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    var savedServices = services.ID > 0 ? session.Update(services) : session.Insert(services);
                    tranx.Commit();
                    return savedServices;
                }
                catch
                {
                    tranx.Rollback();
                    throw;
                }
            }
        }
    }
}
