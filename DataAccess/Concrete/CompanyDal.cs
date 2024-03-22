using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Data;

namespace DataAccess.Concrete;
public class CompanyDal : ICompanyDal
{
    private readonly IDbConnection _dbConnection;

    public CompanyDal(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<int> InsertAsync(CompanyModel model)
    {
        return await _dbConnection.InsertAsync(model);
    }

    public async Task<bool> UpdateAsync(CompanyModel model)
    {
        return await _dbConnection.UpdateAsync(model);
    }

    public async Task<IEnumerable<CompanyModel>> GetAllAsync()
    {
        string query = "SELECT * FROM Company WHERE IsDeleted = 0 ORDER BY c.companyId";
        return await _dbConnection.QueryAsync<CompanyModel>(query);
    }

    public async Task<CompanyModel> GetByIdAsync(int companyId)
    {
        string query = "SELECT * FROM Company WHERE IsDeleted = 0 AND CompanyId = @CompanyId";
        return await _dbConnection.QuerySingleOrDefaultAsync<CompanyModel>(query, new { CompanyId = companyId });
    }

    public async Task<CompanyModel> GetCompanyByEmail(string email)
    {
        string query = "SELECT * FROM Company WHERE IsDeleted =0 AND Email = @Email";
        return await _dbConnection.QuerySingleOrDefaultAsync<CompanyModel>(query, new { Email = email });
    }
}
