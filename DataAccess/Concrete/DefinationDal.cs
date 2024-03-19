using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using Grpc.Core;

namespace DataAccess.Concrete;
public class DefinationDal : IDefinationDal
{
    private readonly INoSqlHelper _noSqlHelper;

    public DefinationDal(INoSqlHelper noSqlHelper)
    {
        _noSqlHelper = noSqlHelper;
    }

    public async Task InsertAsync<T>(string documentId, T model)
    {
        await _noSqlHelper.InsertAsync(documentId, model);
    }

    public async Task UpdateAsync<T>(string documentId, T model)
    {
        await _noSqlHelper.UpdateAsync(documentId, model);
    }

    public async Task<IEnumerable<T>> GetAllAsync<T>(int companyId)
    {
        Type type = typeof(T);
        var model = Activator.CreateInstance(type);
        var collection = model?.GetType()?.CustomAttributes?.FirstOrDefault()?.ConstructorArguments?.LastOrDefault().Value?.ToString();
        string query = $"SELECT c.* FROM Data._default.{collection} as c WHERE c.isDeleted = false AND c.companyId = {companyId} {(type.Name == "ExpenseTypeModel" ? " AND c.isShowInExpenseListPage = false" : "")} ORDER BY c.createdDate";
        var result = await _noSqlHelper.QueryAsync<T>(query);
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
        string query = $"SELECT c.* FROM Data._default.ExpenseTypes as c WHERE c.isDeleted = false AND c.isStocked = true AND c.companyId = {companyId} ORDER BY c.createdDate";
        var result = await _noSqlHelper.QueryAsync<ExpenseTypeModel>(query);
        return result;
    } 

}
