using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INF.Database.Metadata
{
    public class TableInfo : MetaData
    {
        public string Name { get; private set; }
        public Type EntityType { get; private set; }
        public ColumnInfo PrimaryKey { get; set; }
        public IEnumerable<ReferenceInfo> References { get { return _references.Values; } }
        public IEnumerable<ColumnInfo> Columns { get { return _columns.Values; } }

        private readonly Dictionary<string, ColumnInfo> _columns = new Dictionary<string, ColumnInfo>();
        private readonly Dictionary<string, ReferenceInfo> _references = new Dictionary<string, ReferenceInfo>();

        public TableInfo(MetaDataStore store, string name, Type entityType)
            : base(store)
        {
            Name = name;
            EntityType = entityType;
        }

        public void AddColumn(ColumnInfo column)
        {
            if (_columns.ContainsKey(column.Name))
            {
                throw new InvalidOperationException(string.Format("An item with key {0} has already been added", column.Name));
            }

            _columns.Add(column.Name, column);
        }

        public void AddReference(ReferenceInfo reference)
        {
            if (_references.ContainsKey(reference.Name))
            {
                throw new InvalidOperationException(string.Format("An item with key {0} has already been added", reference.Name));
            }

            _references.Add(reference.Name, reference);
        }

        public ColumnInfo GetColumn(string columnName)
        {
            if (!_columns.ContainsKey(columnName))
            {
                throw new InvalidOperationException(string.Format("The table '{0}' does not have a '{1}' column", Name, columnName));
            }

            return _columns[columnName];
        }

        public StringBuilder GetSelectStatementForAllFields()
        {
            StringBuilder builder = new StringBuilder("SELECT " + Escape(Name, PrimaryKey.Name) + ", ");

            AddReferenceColumnNames(builder);
            AddRegularColumnNames(builder);
            RemoveLastCommaAndSpaceIfThereAreAnyColumns(builder);
            builder.Append(" FROM " + Escape(Name));

            return builder;
        }

        public string GetInsertStatement()
        {
            var statement = GetInsertStatementWithoutReturningTheIdentityValue();
            //statement += "); SELECT SCOPE_IDENTITY();";
            return statement;
        }

        public string GetInsertStatementWithoutReturningTheIdentityValue()
        {
            StringBuilder builder = new StringBuilder("INSERT INTO " + Escape(Name) + " (");

            AddReferenceColumnNames(builder);
            AddRegularColumnNames(builder);
            RemoveLastCommaAndSpaceIfThereAreAnyColumns(builder);
            builder.Append(") VALUES (");
            AddReferenceColumnParameterNames(builder);
            AddRegularColumnParameterNames(builder);
            RemoveLastCommaAndSpaceIfThereAreAnyColumns(builder);
            builder.Append("); SELECT SCOPE_IDENTITY();");

            return builder.ToString();
        }

        public string GetUpdateStatement()
        {
            StringBuilder builder = new StringBuilder("UPDATE " + Escape(Name) + " SET ");

            AddReferenceColumnsNameWithParameterName(builder);
            AddRegularColumnsNameWithParameterName(builder);
            RemoveLastCommaAndSpaceIfThereAreAnyColumns(builder);
            AddWhereByIdClause(builder);
            builder.Append(";");

            return builder.ToString();
        }

        public string GetDeleteStatement()
        {
            StringBuilder builder = new StringBuilder("DELETE FROM " + Escape(Name) + " ");

            AddWhereByIdClause(builder);
            builder.Append(";");

            return builder.ToString();
        }

        public IEnumerable<AdoParameterInfo> GetParametersForInsert(object entity)
        {
            return GetParametersForAllReferenceAndRegularColumns(entity);
        }

        public IEnumerable<AdoParameterInfo> GetParametersForUpdate(object entity)
        {
            var parameters = GetParametersForAllReferenceAndRegularColumns(entity);
            parameters.Add(new AdoParameterInfo(PrimaryKey.Name, PrimaryKey.DbType, PrimaryKey.PropertyInfo.GetValue(entity, null)));
            return parameters;
        }

        public StringBuilder AddWhereByIdClause(StringBuilder query)
        {
            query.Append(" WHERE " + Escape(PrimaryKey.Name) + " = " + GetPrimaryKeyParameterName());
            return query;
        }

        public string GetPrimaryKeyParameterName()
        {
            return "@" + PrimaryKey.Name;
        }

        private List<AdoParameterInfo> GetParametersForAllReferenceAndRegularColumns(object entity)
        {
            var parameters = new List<AdoParameterInfo>();

            foreach (var referenceInfo in References)
            {
                var referencedEntity = referenceInfo.PropertyInfo.GetValue(entity, null);
                var referencePrimaryKeyProperty = MetaDataStore.GetTableInfoFor(referenceInfo.ReferenceType).PrimaryKey.PropertyInfo;

                if (referencedEntity == null)
                {
                    parameters.Add(new AdoParameterInfo(referenceInfo.Name, referenceInfo.DbType, null));
                }
                else
                {
                    parameters.Add(new AdoParameterInfo(referenceInfo.Name, referenceInfo.DbType, referencePrimaryKeyProperty.GetValue(referencedEntity, null)));
                }
            }

            foreach (var columnInfo in Columns)
            {
                parameters.Add(new AdoParameterInfo(columnInfo.Name, columnInfo.DbType, columnInfo.PropertyInfo.GetValue(entity, null)));
            }

            return parameters;
        }

        private void RemoveLastCommaAndSpaceIfThereAreAnyColumns(StringBuilder builder)
        {
            if ((References.Count() + Columns.Count()) > 0)
            {
                RemoveLastCharacters(builder, 2);
            }
        }

        private void AddReferenceColumnNames(StringBuilder builder)
        {
            foreach (var referenceInfo in References)
            {
                builder.Append(Escape(Name, referenceInfo.Name) + ", ");
            }
        }

        private void AddReferenceColumnParameterNames(StringBuilder builder)
        {
            foreach (var referenceInfo in References)
            {
                builder.Append("@" + referenceInfo.Name + ", ");
            }
        }

        private void AddReferenceColumnsNameWithParameterName(StringBuilder builder)
        {
            foreach (var referenceInfo in References)
            {
                builder.Append(Escape(referenceInfo.Name) + " = @" + referenceInfo.Name + ", ");
            }
        }

        private void AddRegularColumnNames(StringBuilder builder)
        {
            foreach (var columnInfo in Columns)
            {
                builder.Append(Escape(Name, columnInfo.Name) + ", ");
            }
        }

        private void AddRegularColumnParameterNames(StringBuilder builder)
        {
            foreach (var columnInfo in Columns)
            {
                builder.Append("@" + columnInfo.Name + ", ");
            }
        }

        private void AddRegularColumnsNameWithParameterName(StringBuilder builder)
        {
            foreach (var columnInfo in Columns)
            {
                builder.Append(Escape(columnInfo.Name) + " = @" + columnInfo.Name + ", ");
            }
        }

        private string Escape(string name)
        {
            return "[" + name + "]";
        }

        private string Escape(string tableName, string fieldName)
        {
            return string.Format("[{0}].[{1}]", tableName, fieldName);
        }

        private void RemoveLastCharacters(StringBuilder stringBuilder, int numberOfCharacters)
        {
            stringBuilder.Remove(stringBuilder.Length - numberOfCharacters, numberOfCharacters);
        }
    }
}