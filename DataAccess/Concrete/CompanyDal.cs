using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete;
public class CompanyDal : ICompanyDal
{
    private readonly INoSqlHelper _noSqlHelper;

    public CompanyDal(INoSqlHelper noSqlHelper)
    {
        _noSqlHelper = noSqlHelper;
    }

    public async Task InsertAsync(CompanyModel model)
    {
        await _noSqlHelper.InsertAsync(model.Id, model);
    }

    public async Task UpdateAsync(CompanyModel model)
    {
        await _noSqlHelper.UpdateAsync(model.Id, model);
    }

    public async Task<IEnumerable<CompanyModel>> GetAllAsync()
    {
        string query = "SELECT c.* FROM Data._default.Company as c WHERE c.isDeleted = false ORDER BY c.companyId";
        var result = await _noSqlHelper.QueryAsync<CompanyModel>(query);
        return result;
    }

    public async Task<CompanyModel> GetByIdAsync(int companyId)
    {
        string query = $"SELECT c.* FROM Data._default.Company as c WHERE c.isDeleted = false AND c.companyId = {companyId}";
        var result = await _noSqlHelper.SingleOrDefaultAsync<CompanyModel>(query);

        return result;
    }

    public async Task DeleteByIdAsync(string id)
    {
        string query = $"UPDATE Data._default.Company as c SET c.isDeleted = true WHERE c.isDeleted = false AND c.companyId = {id}";
        await _noSqlHelper.ExecuteAsyncV2(query);
    }

    public async Task<CompanyModel> GetCompanyByEmail(string email)
    {
        string query = $"SELECT c.* FROM Data._default.Company as c WHERE c.isDeleted = false AND c.email = '{email}'";
        var asd = await _noSqlHelper.QueryAsync<CompanyModel>(query);
        var result = await _noSqlHelper.SingleOrDefaultAsync<CompanyModel>(query);
        return result;
    }
}
