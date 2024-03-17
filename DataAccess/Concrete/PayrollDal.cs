using Core.Utilities;
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

    public async Task<IEnumerable<PayrollModel>> GetAllAsync(int companyId)
    {
        string query = $"SELECT p.* FROM Data._default.Payroll as p WHERE p.isDeleted = false AND p.companyId = {companyId} ORDER BY p.createdDate DESC";
        var result = await _noSqlHelper.QueryAsync<PayrollModel>(query);
        return result;
    }

    public async Task<PayrollModel> GetByIdAsync(string id)
    {
        var result = await _noSqlHelper.GetByIdAsync<PayrollModel>(id);
        return result;
    }
}
