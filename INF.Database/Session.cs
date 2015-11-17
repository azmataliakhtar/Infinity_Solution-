using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using INF.Database.Actions;
using INF.Database.Metadata;

namespace INF.Database
{
    public interface ISession : IDisposable
    {
        void Commit();
        void Rollback();

        IQuery CreateQuery(string sql);
        IQuery CreateQuery<TEntity>(string whereClause);

        TEntity Get<TEntity>(object id);
        IEnumerable<TEntity> FindAll<TEntity>();

        TEntity Insert<TEntity>(TEntity entity);
        void BulkInsert<TEntity>(IEnumerable<TEntity> entities);
        TEntity Update<TEntity>(TEntity entity);
        void Delete<TEntity>(TEntity entity);

        TableInfo GetTableInfoFor<TEntity>();

        void ClearCache();
        void RemoveFromCache(object entity);
        void RemoveAllInstancesFromCache<TEntity>();

        SqlConnection GetConnection();
        SqlTransaction GetTransaction();
    }

    public class Session : ISession
    {
        private readonly string _connectionString;
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        private readonly MetaDataStore _metaDataStore;
        private readonly EntityHydrater _hydrater;
        private readonly SessionLevelCache _sessionLevelCache;

        public Session(string connectionString, MetaDataStore metaDataStore)
        {
            this._connectionString = connectionString;
            this._metaDataStore = metaDataStore;
            _sessionLevelCache = new SessionLevelCache();
            _hydrater = new EntityHydrater(metaDataStore, this, _sessionLevelCache);
        }

        private void InitializeConnection()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public SqlConnection GetConnection()
        {
            if (_connection == null)
            {
                InitializeConnection();
            }

            return _connection;
        }

        public SqlTransaction GetTransaction()
        {
            if (_transaction == null)
            {
                InitializeConnection();
            }

            return _transaction;
        }

        public IQuery CreateQuery(string sql)
        {
            var command = GetConnection().CreateCommand();
            command.Transaction = GetTransaction();
            command.CommandText = sql;
            return new Query(command, _metaDataStore, _hydrater);
        }

        public IQuery CreateQuery<TEntity>(string whereClause)
        {
            return CreateQuery(_metaDataStore.GetTableInfoFor<TEntity>().GetSelectStatementForAllFields() + " " + whereClause);
        }

        public TableInfo GetTableInfoFor<TEntity>()
        {
            return _metaDataStore.GetTableInfoFor<TEntity>();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            if (_transaction != null) _transaction.Dispose();
            if (_connection != null) _connection.Dispose();
        }

        private TAction CreateAction<TAction>()
            where TAction : DatabaseAction
        {
            return (TAction)Activator.CreateInstance(typeof(TAction), GetConnection(), GetTransaction(),
                _metaDataStore, _hydrater, _sessionLevelCache);
        }

        public TEntity Get<TEntity>(object id)
        {
            return CreateAction<GetByIdAction>().Get<TEntity>(id);
        }

        public IEnumerable<TEntity> FindAll<TEntity>()
        {
            return CreateAction<FindAllAction>().FindAll<TEntity>();
        }

        public TEntity Insert<TEntity>(TEntity entity)
        {
            return CreateAction<InsertAction>().Insert(entity);
        }

        public void BulkInsert<TEntity>(IEnumerable<TEntity> entities)
        {
            CreateAction<BulkInsertAction>().Insert(entities, 50, 200);
        }

        public TEntity Update<TEntity>(TEntity entity)
        {
            return CreateAction<UpdateAction>().Update(entity);
        }

        public void Delete<TEntity>(TEntity entity)
        {
            CreateAction<DeleteAction>().Delete(entity);
        }

        public void InitializeProxy(object proxy, Type targetType)
        {
            CreateAction<InitializeProxyAction>().InitializeProxy(proxy, targetType);
        }

        public void ClearCache()
        {
            _sessionLevelCache.ClearAll();
        }

        public void RemoveFromCache(object entity)
        {
            _sessionLevelCache.Remove(entity);
        }

        public void RemoveAllInstancesFromCache<TEntity>()
        {
            _sessionLevelCache.RemoveAllInstancesOf(typeof(TEntity));
        }
    }
}