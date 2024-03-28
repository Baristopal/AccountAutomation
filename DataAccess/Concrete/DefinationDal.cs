using Core.Utilities;
using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Data;

namespace DataAccess.Concrete;
public class DefinationDal : IDefinationDal
{
    private readonly IDbConnection _dbConnection;
    private readonly INoSqlHelper _noSqlHelper;

    public DefinationDal(INoSqlHelper noSqlHelper, IDbConnection dbConnection)
    {
        _noSqlHelper = noSqlHelper;
        _dbConnection = dbConnection;
    }

    public async Task<int> InsertAsync(object model)
    {
        var result = await _dbConnection.InsertAsync(model);
        return result;
    }

    public async Task<bool> UpdateAsync(object model)
    {
        Type type = model.GetType();
        var newModel = Activator.CreateInstance(type);
        return await _dbConnection.UpdateAsync(newModel);
    }

    public async Task<IEnumerable<T>> GetAllAsync<T>(int companyId)
    {
        Type type = typeof(T);
        var model = Activator.CreateInstance(type);
        var collection = model?.GetType()?.CustomAttributes?.FirstOrDefault()?.ConstructorArguments?.LastOrDefault().Value?.ToString();
        string query = $"SELECT * FROM {collection} WHERE IsDeleted = 0 AND CompanyId = {companyId} {(type.Name == "ExpenseTypeModel" ? " AND IsShowInExpenseListPage = 0" : "")} ORDER BY CreatedDate";
        var result = await _dbConnection.QueryAsync<T>(query);
        return result;
    }

    public async Task<IEnumerable<ExpenseTypeModel>> GetExpenseListTypes(int companyId)
    {
        string query = $"SELECT c.* FROM Data._default.ExpenseTypes as c WHERE c.isDeleted = false AND c.companyId = {companyId} AND c.isShowInExpenseListPage = true ORDER BY c.createdDate";
        var result = await _noSqlHelper.QueryAsync<ExpenseTypeModel>(query);
        return result;
    }

    public async Task<T> GetByIdAsync<T>(string id)
    {
        var result = await _noSqlHelper.GetByIdAsync<T>(id);
        return result;
    }

    public async Task<IEnumerable<ExpenseTypeModel>> GetAllStockExpenses(int companyId)
    {
        string query = $"SELECT * FROM ExpenseType WHERE IsDeleted = 0 AND IsStocked = 1 AND CompanyId = {companyId} ORDER BY CreatedDate";
        var result = await _dbConnection.QueryAsync<ExpenseTypeModel>(query);
        return result;  
    }

}
