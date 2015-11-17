using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Castle.DynamicProxy;
using INF.Database.Metadata;

namespace INF.Database
{
    public class EntityHydrater
    {
        private readonly MetaDataStore _metaDataStore;
        private readonly SessionLevelCache _sessionLevelCache;
        private readonly ProxyGenerator _proxyGenerator;
        private readonly Session _session;

        public EntityHydrater(MetaDataStore metaDataStore, Session session, SessionLevelCache sessionLevelCache)
        {
            this._metaDataStore = metaDataStore;
            this._sessionLevelCache = sessionLevelCache;
            this._session = session;
            _proxyGenerator = new ProxyGenerator();
        }

        public TEntity HydrateEntity<TEntity>(SqlCommand command)
        {
            IDictionary<string, object> values;

            using (var reader = command.ExecuteReader())
            {
                if (!reader.HasRows) return default(TEntity);
                reader.Read();
                values = GetValuesFromCurrentRow(reader);
            }

            return CreateEntityFromValues<TEntity>(values);
        }

        public IEnumerable<TEntity> HydrateEntities<TEntity>(SqlCommand command)
        {
            var rows = new List<IDictionary<string, object>>();
            var entities = new List<TEntity>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    rows.Add(GetValuesFromCurrentRow(reader));
                }
            }

            foreach (var row in rows)
            {
                entities.Add(CreateEntityFromValues<TEntity>(row));
            }

            return entities;
        }

        public void UpdateEntity(Type type, object entity, SqlCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                var tableInfo = _metaDataStore.GetTableInfoFor(type);
                Hydrate(tableInfo, entity, GetValuesFromCurrentRow(reader));
            }
        }
        
        private IDictionary<string, object> GetValuesFromCurrentRow(SqlDataReader dataReader)
        {
            var values = new Dictionary<string, object>();

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                values.Add(dataReader.GetName(i), dataReader.GetValue(i));
            }

            return values;
        }

        private TEntity CreateEntityFromValues<TEntity>(IDictionary<string, object> values)
        {
            var tableInfo = _metaDataStore.GetTableInfoFor<TEntity>();

            var cachedEntity = _sessionLevelCache.TryToFind(typeof(TEntity), values[tableInfo.PrimaryKey.Name]);
            if (cachedEntity != null) return (TEntity)cachedEntity;

            var entity = Activator.CreateInstance<TEntity>();
            Hydrate(tableInfo, entity, values);
            _sessionLevelCache.Store(typeof(TEntity), values[tableInfo.PrimaryKey.Name], entity);
            return entity;
        }

        private void Hydrate<TEntity>(TableInfo tableInfo, TEntity entity, IDictionary<string, object> values)
        {
            tableInfo.PrimaryKey.PropertyInfo.SetValue(entity, values[tableInfo.PrimaryKey.Name], null);
            SetRegularColumns(tableInfo, entity, values);
            SetReferenceProperties(tableInfo, entity, values);
        }

        private void SetRegularColumns<TEntity>(TableInfo tableInfo, TEntity entity, IDictionary<string, object> values)
        {
            foreach (var columnInfo in tableInfo.Columns)
            {
                if (columnInfo.PropertyInfo.CanWrite)
                {
                    object value = values[columnInfo.Name];
                    if (value is DBNull) value = null;
                    columnInfo.PropertyInfo.SetValue(entity, value, null);
                }
            }
        }

        private void SetReferenceProperties<TEntity>(TableInfo tableInfo, TEntity entity, IDictionary<string, object> values)
        {
            foreach (var referenceInfo in tableInfo.References)
            {
                if (referenceInfo.PropertyInfo.CanWrite)
                {
                    object foreignKeyValue = values[referenceInfo.Name];

                    if (foreignKeyValue is DBNull)
                    {
                        referenceInfo.PropertyInfo.SetValue(entity, null, null);
                    }
                    else
                    {
                        var referencedEntity = _sessionLevelCache.TryToFind(referenceInfo.ReferenceType, foreignKeyValue) ??
                                               CreateProxy(tableInfo, referenceInfo, foreignKeyValue);

                        referenceInfo.PropertyInfo.SetValue(entity, referencedEntity, null);
                    }
                }
            }
        }

        private object CreateProxy(TableInfo tableInfo, ReferenceInfo referenceInfo, object foreignKeyValue)
        {
            var proxy = _proxyGenerator.CreateClassProxy(referenceInfo.ReferenceType,
                new[] { new LazyLoadingInterceptor(tableInfo, _session) });
            var referencePrimaryKey = _metaDataStore.GetTableInfoFor(referenceInfo.ReferenceType).PrimaryKey;
            referencePrimaryKey.PropertyInfo.SetValue(proxy, foreignKeyValue, null);
            return proxy;
        }
    }
}