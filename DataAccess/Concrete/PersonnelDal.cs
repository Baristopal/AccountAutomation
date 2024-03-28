using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Data;

namespace DataAccess.Concrete;
public class PersonnelDal : IPersonnelDal
{
    private readonly IDbConnection _dbConnection;

    public PersonnelDal(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<PersonnelModel>> GetAll(int companyId)
    {
        string query = $"SELECT * FROM Personnel WHERE IsDeleted = 0 AND CompanyId = {companyId} ORDER BY CreatedDate DESC";
        var result = await _dbConnection.QueryAsync<PersonnelModel>(query);
        return result;
    }

    public async Task<PersonnelModel> GetById(string id)
    {
        var result = await _dbConnection.GetAsync<PersonnelModel>(id);
        return result;
    }

    public async Task Insert(PersonnelModel model)
    {
        await _dbConnection.InsertAsync(model);
    }

    public async Task Update(PersonnelModel model)
    {
        await _dbConnection.UpdateAsync(model);
    }

}
