using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete;
public class PersonnelDal(IClusterProvider clusterProvider) : IPersonnelDal
{
    public async Task InsertAsync(PersonnelModel model)
    {
        var cluster = await clusterProvider.GetClusterAsync();
        var bucket = await cluster.BucketAsync("AccountAutomation");
        var staffId =(await cluster.QueryAsync<int>("SELECT RAW MAX(p.staffId) from AccountAutomation._default.Personnel as p where p.isDeleted = false")).SingleOrDefaultAsync().Result;
        model.StaffId = staffId + 1;
        _ = await bucket.Collection("Personnel").InsertAsync(model.Id, model);
    }

    public async Task UpdateAsync(PersonnelModel model)
    {
        var cluster = await clusterProvider.GetClusterAsync();
        var bucket = await cluster.BucketAsync("AccountAutomation");
        _ = await bucket.Collection("Personnel").ReplaceAsync(model.Id, model);
    }

    public async Task<IEnumerable<PersonnelModel>> GetAllAsync()
    {
        var cluster = await clusterProvider.GetClusterAsync();
        var result = await cluster.QueryAsync<PersonnelModel>("SELECT p.* FROM AccountAutomation._default.Personnel as p WHERE p.isDeleted = false ORDER BY p.staffId");
        return await result.ToListAsync();
    }

    public async Task<PersonnelModel> GetByIdAsync(string id)
    {
        var cluster = await clusterProvider.GetClusterAsync();
        var bucket = await cluster.BucketAsync("AccountAutomation");
        var result = await bucket.Collection("Personnel").GetAsync(id);
        return result.ContentAs<PersonnelModel>()!;
    }

    public async Task DeleteByIdAsync(string id)
    {
        var cluster = await clusterProvider.GetClusterAsync();
        await cluster.QueryAsync<bool>($"UPDATE AccountAutomation._default.Personnel as p SET p.isDeleted = true WHERE p.isDeleted = false AND p.id = {id}");
    }
}
