using System.Reflection;
using INF.Database.Metadata;

namespace INF.Database
{
    public interface ISessionFactory
    {
        ISession CreateSession();
    }

    public class SessionFactory : ISessionFactory
    {
        private string _connectionString;
        private MetaDataStore _metaDataStore;

        public static ISessionFactory Create(Assembly assembly, string connectionString)
        {
            var sessionFactory = new SessionFactory { _connectionString = connectionString, _metaDataStore = new MetaDataStore() };
            sessionFactory._metaDataStore.BuildMetaDataFor(assembly);
            return sessionFactory;
        }

        private SessionFactory() { }

        public ISession CreateSession()
        {
            return new Session(_connectionString, _metaDataStore);
        }
    }
}