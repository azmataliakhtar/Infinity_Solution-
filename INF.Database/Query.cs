using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using INF.Database.Actions;
using INF.Database.Metadata;

namespace INF.Database
{
    public interface IQuery
    {
        void AddParameter(string name, object value, DbType dbType);
        TResult GetSingleResult<TResult>();
        IEnumerable<TResult> GetResults<TResult>();
        int ExecuteNonQuery();
    }

    public class Query : IQuery
    {
        private readonly SqlCommand _command;
        private readonly MetaDataStore _metaDataStore;
        private readonly EntityHydrater _hydrater;

        public Query(SqlCommand command, MetaDataStore metaDataStore, EntityHydrater hydrater)
        {
            this._command = command;
            this._metaDataStore = metaDataStore;
            this._hydrater = hydrater;
        }

        public void AddParameter(string name, object value, DbType dbType)
        {
            _command.CreateAndAddInputParameter(dbType, name, value);
        }

        public TResult GetSingleResult<TResult>()
        {
            var tableInfo = _metaDataStore.GetTableInfoFor<TResult>();

            if (tableInfo == null)
            {
                var scalar = (TResult)_command.ExecuteScalar();
                _command.Dispose();
                return scalar;
            }

            var result = _hydrater.HydrateEntity<TResult>(_command);
            _command.Dispose();
            return result;
        }

        public IEnumerable<TResult> GetResults<TResult>()
        {
            var tableInfo = _metaDataStore.GetTableInfoFor<TResult>();

            if (tableInfo == null)
            {
                var listOfValues = GetListOfValues<TResult>();
                _command.Dispose();
                return listOfValues;
            }

            var result = _hydrater.HydrateEntities<TResult>(_command);
            _command.Dispose();
            return result;
        }

        private IEnumerable<TResult> GetListOfValues<TResult>()
        {
            using (var reader = _command.ExecuteReader())
            {
                var list = new List<object>();
                while (reader.Read())
                {
                    list.Add(reader.GetValue(0));
                }
                return list.Cast<TResult>();
            }
        }

        public int ExecuteNonQuery()
        {
            var rowsAffected = _command.ExecuteNonQuery();
            _command.Dispose();
            return rowsAffected;
        }
    }
}