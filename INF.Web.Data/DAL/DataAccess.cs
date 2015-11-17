using System.Web;
using System.Web.Caching;

namespace INF.Web.Data.DAL
{
    public class DataAccess
    {
        protected static string ConnectionString { get; set; }
        public int CacheDuration { get; set; }
        public bool EnableCaching { get; set; }

        public DataAccess()
        {
        }

        public DataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public DataProvider Provider
        {
            get { return DataProvider.GetInstance(ConnectionString); }
        }

        protected Cache Cache
        {
            get { return HttpContext.Current.Cache; }
        }
    }
}