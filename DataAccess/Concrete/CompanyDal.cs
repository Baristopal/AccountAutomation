using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;

namespace DataAccess.Concrete;
public class CompanyDal(IClusterProvider clusterProvider) : ICompanyDal
{
    public async Task InsertAsync(CompanyModel model)
    {
        var cluster = await clusterProvider.GetClusterAsync();
        var bucket = await cluster.BucketAsync("AccountAutomation");
        var companyId = (await cluster.QueryAsync<int>("SELECT RAW IFNULL(MAX(c.companyId),1000) from AccountAutomation._default.Company as c where c.isDeleted = false")).SingleOrDefaultAsync().Result;
        model.CompanyId = companyId + 1;
        _ = await bucket.Collection("Company").InsertAsync(model.Id, model);
    }

    public async Task UpdateAsync(CompanyModel model)
    {
        var cluster = await clusterProvider.GetClusterAsync();
        var bucket = await cluster.BucketAsync("AccountAutomation");
        _ = await bucket.Collection("Company").ReplaceAsync(model.Id, model);
    }

    public async Task<IEnumerable<CompanyModel>> GetAllAsync()
    {
        var cluster = await clusterProvider.GetClusterAsync();
        var result = await cluster.QueryAsync<CompanyModel>("SELECT c.* FROM AccountAutomation._default.Company as c WHERE c.isDeleted = false ORDER BY c.companyId");
        return await result.ToListAsync();
    }

    public async Task<CompanyModel> GetByIdAsync(string id)
    {
        var cluster = await clusterProvider.GetClusterAsync();
        var bucket = await cluster.BucketAsync("AccountAutomation");
        var result = await bucket.Collection("Company").GetAsync(id);
        return result.ContentAs<CompanyModel>()!;
    }

    public async Task DeleteByIdAsync(string id)
    {
        var cluster = await clusterProvider.GetClusterAsync();
        await cluster.QueryAsync<bool>($"UPDATE AccountAutomation._default.Company as c SET c.isDeleted = true WHERE c.isDeleted = false AND c.companyId = {id}");
    }

    public async Task<CompanyModel> GetCompanyByEmail(string email)
    {
        var cluster = await clusterProvider.GetClusterAsync();
        var result = await cluster.QueryAsync<CompanyModel>($"SELECT c.* FROM AccountAutomation._default.Company as c WHERE c.isDeleted = false AND c.email = '{email}'");
        return await result.FirstOrDefaultAsync();
    }
}
