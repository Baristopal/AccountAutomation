using Core.Utilities;
using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete;
public class PayrollDal : IPayrollDal
{

    private readonly INoSqlHelper _noSqlHelper;

    public PayrollDal(INoSqlHelper noSqlHelper)
    {
        _noSqlHelper = noSqlHelper;
    }

    public async Task InsertAsync(PayrollModel model)
    {
        await _noSqlHelper.InsertAsync(model.Id, model);
    }

    public async Task UpdateAsync(PayrollModel model)
    {
        await _noSqlHelper.UpdateAsync(model.Id, model);
    }

    public async Task<IEnumerable<PayrollModel>> GetAllAsync()
    {
        string query = "SELECT p.* FROM Data._default.Payroll as p WHERE p.isDeleted = false ORDER BY p.createdDate DESC";
        var result = await _noSqlHelper.QueryAsync<PayrollModel>(query);
        return result;
    }

    public async Task<PayrollModel> GetByIdAsync(string id)
    {
        var result = await _noSqlHelper.GetByIdAsync<PayrollModel>(id);
        return result;
    }

    public async Task DeleteByIdAsync(string id)
    {
        string query = $"UPDATE Data._default.Payroll as p SET p.isDeleted = true WHERE p.isDeleted = false AND p.id = {id}";
        await _noSqlHelper.ExecuteAsyncV2(query);
    }
}
