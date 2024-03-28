using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Data;

namespace DataAccess.Concrete;
public class InstantDal : IInstantDal
{
    private readonly IDbConnection _dbConnection;

    public InstantDal(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task Add(InstantModel model)
    {
        await _dbConnection.InsertAsync(model);
    }

    public async Task Update(InstantModel model)
    {
        await _dbConnection.UpdateAsync(model);
    }

    public async Task<IEnumerable<InstantModel>> GetAll(int companyId)
    {
        string query = $"SELECT * FROM Instant WHERE IsDeleted = 0 AND CompanyId = {companyId} ORDER BY CreatedDate";
        var result = await _dbConnection.QueryAsync<InstantModel>(query);
        return result;
    }

    public async Task<InstantModel> GetById(string id)
    {
        var result = await _dbConnection.GetAsync<InstantModel>(id);
        return result;
    }
}
