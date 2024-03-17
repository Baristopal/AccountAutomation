using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete;
public class InstantDal : IInstantDal
{
    private readonly INoSqlHelper _noSqlHelper;

    public InstantDal(INoSqlHelper noSqlHelper)
    {
        _noSqlHelper = noSqlHelper;
    }

    public async Task Add(InstantModel model)
    {
        await _noSqlHelper.InsertAsync(model.Id, model);
    }

    public async Task Update(InstantModel model)
    {
        await _noSqlHelper.UpdateAsync(model.Id, model);
    }

    public async Task<IEnumerable<InstantModel>> GetAll(int companyId)
    {
        string query = $"SELECT c.* FROM Data._default.Instant as c WHERE c.isDeleted = false AND c.companyId = {companyId} ORDER BY c.createdDate";
        var result = await _noSqlHelper.QueryAsync<InstantModel>(query);
        return result;
    }

    public async Task<InstantModel> GetById(string id)
    {
        var result = await _noSqlHelper.GetByIdAsync<InstantModel>(id);
        return result;
    }
}
