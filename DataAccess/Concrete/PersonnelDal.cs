using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using MongoDB.Driver;

namespace DataAccess.Concrete;
public class PersonnelDal : IPersonnelDal
{
    private readonly INoSqlHelper _nosqlHelper;

    public PersonnelDal(INoSqlHelper noSqlHelper)
    {
        _nosqlHelper = noSqlHelper;
    }

    public async Task<IEnumerable<PersonnelModel>> GetAll()
    {
        string query = "SELECT p.* FROM Data._default.Personnel as p WHERE p.isDeleted = false ORDER BY p.createdDate DESC";
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
        await _nosqlHelper.InsertAsync(model.Id,model);
    }

    public async Task Update(PersonnelModel model)
    {
        await _nosqlHelper.UpdateAsync(model.Id,model);
    }

}
