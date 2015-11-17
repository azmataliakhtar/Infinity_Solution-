using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class DeliveryTimingProvider:DataAccess
    {
        private static DeliveryTimingProvider _instance;

        DeliveryTimingProvider() { }

        static DeliveryTimingProvider()
        {
            _instance = new DeliveryTimingProvider();
        }

        public static DeliveryTimingProvider Instance
        {
            get { return _instance ?? (_instance = new DeliveryTimingProvider()); }
        }

        public CsDeliveryTiming GetDeliveryTiming(int dayInWeek)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsDeliveryTiming>(" WHERE [Delivery_Day] = " + dayInWeek);
                return query.GetSingleResult<CsDeliveryTiming>();
            }
        }
    }
}
