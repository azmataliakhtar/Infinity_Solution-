using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class RestaurantInfoProvider : DataAccess, IRestaurantInfo
    {
        private static RestaurantInfoProvider _instance;

        static RestaurantInfoProvider()
        {
            _instance = new RestaurantInfoProvider();
        }

        RestaurantInfoProvider()
        {
            
        }

        public static RestaurantInfoProvider CurrentInstance
        {
            get { return _instance ?? (_instance = new RestaurantInfoProvider()); }
        }

        public CsRestaurant GetRestaurantInfo()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var results = session.FindAll<CsRestaurant>();
                if(results != null)
                {
                    var enumerator = results.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        return enumerator.Current;
                    }
                }
                return null;
            }
        }

        public CsRestaurant GetRestaurantInfo(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                return session.Get<CsRestaurant>(id);
            }
        }

        public CsRestaurant SaveRestaurantInfo(CsRestaurant res)
        {
            CsRestaurant savedRestaurant = null;
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                savedRestaurant = res.ID == 0 ? session.Insert(res) : session.Update(res);
                tranx.Commit();
            }
            return savedRestaurant;
        }
    }
}