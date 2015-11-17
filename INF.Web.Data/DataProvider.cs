using System.Configuration;
using System.Reflection;
using INF.Database;

namespace INF.Web.Data
{
    public class DataProvider
    {
        private static readonly DataProvider _DataProvider;
        private ISessionFactory _sessionFactory;

        private static string _ConnectionString;

        static DataProvider()
        {
            if (_DataProvider == null)
            {
                _DataProvider = new DataProvider();
            }
        }

        private DataProvider()
        {
        }

        private DataProvider(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public static DataProvider GetInstance()
        {
            return new DataProvider();
        }

        public static DataProvider GetInstance(string connectionString)
        {
            return new DataProvider(connectionString);
        }

        public ISessionFactory CreateSessionFactory()
        {
            if (_sessionFactory == null)
            {
                //if (string.IsNullOrEmpty(_ConnectionString) || _ConnectionString.Trim().Length==0)
                //    _ConnectionString = ConfigurationManager.ConnectionStrings["PizzaWebConnectionString"].ConnectionString;

                _ConnectionString = WebSettings.Instance.ConnectionString;
                var asm = Assembly.Load("INF.Web.Data");
                _sessionFactory = SessionFactory.Create(asm, _ConnectionString);
            }

            return _sessionFactory;
        }
    }
}