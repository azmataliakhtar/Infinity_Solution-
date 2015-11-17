using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class RestaurantTimingProvider : DataAccess
    {
        private static RestaurantTimingProvider _instance;

        RestaurantTimingProvider()
        {
        }

        static RestaurantTimingProvider()
        {
            _instance = new RestaurantTimingProvider();
        }

        public static RestaurantTimingProvider Instance
        {
            get { return _instance ?? (_instance = new RestaurantTimingProvider()); }
        }

        public CsRestaurantTiming GetRestaurantTiming(int dayInWeek)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsRestaurantTiming>(" WHERE [DAY] = " + dayInWeek);
                return query.GetSingleResult<CsRestaurantTiming>();
            }
        }

        public CsRestaurantTiming SaveRestaurantTiming(CsRestaurantTiming timing)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    CsRestaurantTiming savedTiming = timing != null && timing.ID > 0
                                                       ? session.Update(timing)
                                                       : session.Insert(timing);
                    return savedTiming;
                }
                catch (Exception ex)
                {
                    tranx.Rollback();
                    throw;
                }
                finally
                {
                    tranx.Commit();
                }
            }
        }
    }
}
