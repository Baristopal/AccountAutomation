using Core.Utilities;
using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Data;

namespace DataAccess.Concrete;
public class CheckDal : ICheckDal
{
    private readonly IDbConnection _dbConnection;
    public CheckDal(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<int> Add(CheckModel model)
    {
        return await _dbConnection.InsertAsync(model);
    }

    public async Task<bool> Update(CheckModel model)
    {
        return await _dbConnection.UpdateAsync(model);
    }

    public async Task<IEnumerable<CheckModel>> GetAll(int companyId)
    {
        string query = "SELECT * FROM Cities WITH(NOLOCK) WHERE IsDeleted=0 AND CompanyId=@CompanyId";
        var p = new { CompanyId = companyId };

        return await _dbConnection.QueryAsync<CheckModel>(query, p, commandType: CommandType.Text);
    }

    public async Task<CheckModel> GetById(string id)
    {
        return await _dbConnection.QuerySingleOrDefaultAsync<CheckModel>("SELECT * FROM Check WHERE Id=@Id AND IsDeleted = 0", new { Id = id });
    }

    public async Task<decimal> GetChecksTotalAmount(string processType, int companyId)
    {
        string sql  = "SELECT RAW Price FROM Check WITH(NOLOCK) WHERE IsDeleted = 0 AND ProcessType = @ProcessType AND CompanyId=@CompanyId";
        var p = new { ProcessType = processType, CompanyId = companyId };
        var result = await _dbConnection.QuerySingleOrDefaultAsync<decimal>(sql, p, commandType: CommandType.Text);

        return result;
    }


}
