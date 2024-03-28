using Core.Utilities;
using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Data;

namespace DataAccess.Concrete;
public class PayrollDal : IPayrollDal
{

    private readonly IDbConnection _dbConnection;

    public PayrollDal(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task InsertAsync(PayrollModel model)
    {
        await _dbConnection.InsertAsync(model);
    }

    public async Task UpdateAsync(PayrollModel model)
    {
        await _dbConnection.UpdateAsync(model);
    }

    public async Task<IEnumerable<PayrollModel>> GetAllAsync(int companyId)
    {
        string query = $"SELECT * FROM Payroll WHERE  IsDeleted = 0 AND CompanyId = {companyId} ORDER BY CreatedDate DESC";
        var result = await _dbConnection.QueryAsync<PayrollModel>(query);
        return result;
    }

    public async Task<PayrollModel> GetByIdAsync(string id)
    {
        var result = await _dbConnection.GetAsync<PayrollModel>(id);
        return result;
    }
}
