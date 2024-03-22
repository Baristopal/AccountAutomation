using Dapper;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;
using Utilities.Extensions;


namespace DataAccess.Concrete.Repository;

public class DapperRepositoryBase
{
    private readonly IDbConnection _dbConnection;

    public DapperRepositoryBase(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<int> DeleteByIdAsync<T>(int id)
    {
        return await _dbConnection.ExecuteAsync($"UPDATE {typeof(T).GetTableName()} SET IsDeleted = 1 WHERE Id=@Id", new { Id = id });
    }

    public async Task<IEnumerable<T>> GetAllAsync<T>()
    {
        return await _dbConnection.QueryAsync<T>($"SELECT * FROM {typeof(T).GetTableName() ?? typeof(T).Name}");
    }

    public async Task<T> GetByIdAsync<T>(int id)
    {
        return await _dbConnection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {typeof(T).GetTableName()} WHERE Id=@Id", new { Id = id });
    }

    public async Task<int> InsertAsync<T>(T t)
    {
        var insertQuery = GenerateInsertQuery<T>();

        return await _dbConnection.ExecuteScalarAsync<int>(insertQuery, t);
    }

    public async Task<int> UpdateAsync<T>(T t)
    {
        var updateQuery = GenerateUpdateQuery(t);
        return await _dbConnection.ExecuteAsync(updateQuery, t);
    }

    public int WithTransactionInsert<T>(T t, out IDbTransaction _transaction, IDbTransaction transaction = null, bool IsTransactionComplated = false)
    {
        var insertQuery = GenerateInsertQuery<T>();
        IDbConnection dbConnection;

        if (transaction == null) dbConnection = _dbConnection; else dbConnection = transaction.Connection;

        try
        {
            if (transaction == null)
            {
                if (dbConnection.State == ConnectionState.Closed) dbConnection.Open();
                transaction = dbConnection.BeginTransaction();

            }
            _transaction = transaction;
            int id = dbConnection.ExecuteScalar<int>(insertQuery, param: t, commandType: CommandType.Text, transaction: transaction);

            if (IsTransactionComplated) transaction.Commit();
            return id;

        }
        catch
        {
            transaction.Rollback();
            transaction.Dispose();
            if (dbConnection.State == ConnectionState.Open) dbConnection.Close();
            dbConnection.Dispose();
            _transaction = null;
            return 0;
        }
    }


    private class PropertyContainer
    {
        private readonly Dictionary<string, object> _ids;
        private readonly Dictionary<string, object> _values;

        #region Properties

        internal IEnumerable<string> IdNames
        {
            get { return _ids.Keys; }
        }

        internal IEnumerable<string> ValueNames
        {
            get { return _values.Keys; }
        }

        internal IEnumerable<string> AllNames
        {
            get { return _ids.Keys.Union(_values.Keys); }
        }

        internal IDictionary<string, object> IdPairs
        {
            get { return _ids; }
        }

        internal IDictionary<string, object> ValuePairs
        {
            get { return _values; }
        }

        internal IEnumerable<KeyValuePair<string, object>> AllPairs
        {
            get { return _ids.Concat(_values); }
        }

        #endregion

        #region Constructor

        internal PropertyContainer()
        {
            _ids = new Dictionary<string, object>();
            _values = new Dictionary<string, object>();
        }

        #endregion

        #region Methods

        internal void AddId(string name, object value)
        {
            _ids.Add(name, value);
        }

        internal void AddValue(string name, object value)
        {
            _values.Add(name, value);
        }

        #endregion
    }

    private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
    {


        return (from prop in listOfProperties
                let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                select prop.Name).ToList();
    }

    private static IEnumerable<PropertyInfo> GetProperties<T>() => typeof(T).GetProperties();
    private static string GenerateInsertQuery<T>()
    {
        var insertQuery = new StringBuilder($"INSERT INTO {typeof(T).GetTableName() ?? typeof(T).Name} ");

        insertQuery.Append('(');

        var properties = GenerateListOfProperties(GetProperties<T>().Where(w => w.Name != "Id"));
        properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

        insertQuery
            .Remove(insertQuery.Length - 1, 1)
            .Append(") OUTPUT inserted.Id VALUES (");

        properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

        insertQuery
            .Remove(insertQuery.Length - 1, 1)
            .Append(')');

        return insertQuery.ToString();
    }
    private static string GenerateUpdateQuery<T>(T t)
    {
        var updateQuery = new StringBuilder($"UPDATE {typeof(T).GetTableName() ?? typeof(T).Name} SET ");
        var properties = GenerateListOfProperties(GetProperties<T>());

        properties.ForEach(property =>
        {
            if (!property.Equals("Id") && typeof(T).GetProperty(property).GetValue(t, null) != null)
            {
                updateQuery.Append($"{property}=@{property},");
            }
        });

        updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
        updateQuery.Append(" WHERE Id=@Id");

        return updateQuery.ToString();
    }
}

