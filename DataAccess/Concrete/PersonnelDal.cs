using Core.Utilities;
using Couchbase.Extensions.DependencyInjection;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataAccess.Concrete;
public class PersonnelDal : IPersonnelDal
{
    private readonly IMongoDbHelper _mongoDbHelper;

    public PersonnelDal(IMongoDbHelper mongoDbHelper)
    {
        _mongoDbHelper = mongoDbHelper;
    }

    public async Task<List<PersonnelModel>> GetAll()
    {
        var filter = Builders<PersonnelModel>.Filter.Eq(p => p.IsDeleted, false);
        var result = await _mongoDbHelper.GetAllAsync(filter);
        return result;
    }

    public async Task<PersonnelModel> GetById(string id)
    {
        var result = await _mongoDbHelper.GetByIdAsync<PersonnelModel>(id);
        return result;
    }

    public async Task Insert(PersonnelModel model)
    {
        await _mongoDbHelper.InsertAsync(model);
    }

    public async Task Update(PersonnelModel model)
    {
        await _mongoDbHelper.UpdateAsync(model);
    }

}
