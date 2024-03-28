using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
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
        var newModel = new CompanyDto
        {
            Name = model.Name,
            Email = model.Email,
            PasswordHash = model.PasswordHash,
            PasswordSalt = model.PasswordSalt,
            CreatedDate = model.CreatedDate,
            CreatedName = model.CreatedName,
            UpdatedDate = model.UpdatedDate,
            UpdatedName = model.UpdatedName,
            IsDeleted = model.IsDeleted,
            Id = model.Id,
            IsActive = model.IsActive,
        };

        var companyId = await _dbConnection.QueryFirstOrDefaultAsync<int>("SELECT ISNULL(MAX(CompanyId),0) FROM Company");
        if (companyId == 0)
            newModel.CompanyId = 1001;

        return await _dbConnection.InsertAsync(newModel);
    }

    public async Task<bool> UpdateAsync(CompanyModel model)
    {
        return await _dbConnection.UpdateAsync(model);
    }

    public async Task<IEnumerable<CompanyModel>> GetAllAsync()
    {
        string query = "SELECT * FROM Company WHERE IsDeleted = 0 ORDER BY CompanyId";
        return await _dbConnection.QueryAsync<CompanyModel>(query);
    }

    public async Task<CompanyModel> GetByIdAsync(int companyId)
    {
        string query = "SELECT * FROM Company WHERE IsDeleted = 0 AND CompanyId = @CompanyId";
        return await _dbConnection.QuerySingleOrDefaultAsync<CompanyModel>(query, new { CompanyId = companyId });
    }

    public async Task<CompanyModel> GetCompanyByEmail(string email)
    {
        string query = $"SELECT * FROM Company WHERE IsDeleted =0 AND Email = '{email}'";
        var result =  await _dbConnection.QuerySingleOrDefaultAsync<CompanyModel>(query);
        return result;  
    }
}
