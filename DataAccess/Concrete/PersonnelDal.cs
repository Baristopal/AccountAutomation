using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete;
public class PersonnelDal : IPersonnelDal
{
    private readonly INoSqlHelper _nosqlHelper;

    public PersonnelDal(INoSqlHelper noSqlHelper)
    {
        _nosqlHelper = noSqlHelper;
    }

    public async Task<IEnumerable<PersonnelModel>> GetAll(int companyId)
    {
        string query = $"SELECT p.* FROM Data._default.Personnel as p WHERE p.isDeleted = false AND p.companyId = {companyId} ORDER BY p.createdDate DESC";
        var result = await _nosqlHelper.QueryAsync<PersonnelModel>(query);
        return result;
    }

    public async Task<PersonnelModel> GetById(string id)
    {
        var result = await _nosqlHelper.GetByIdAsync<PersonnelModel>(id);
        return result;
    }

    public async Task Insert(PersonnelModel model)
    {
        await _nosqlHelper.InsertAsync(model.Id, model);
    }

    public async Task Update(PersonnelModel model)
    {
        await _nosqlHelper.UpdateAsync(model.Id, model);
    }

}
