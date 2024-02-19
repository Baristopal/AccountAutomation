using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete;
public class PayrollDal(IClusterProvider clusterProvider) : IPayrollDal
{
    public async Task InsertAsync(PayrollModel model)
    {
        var cluster = await clusterProvider.GetClusterAsync();
        var bucket = await cluster.BucketAsync("AccountAutomation");
        _ = await bucket.Collection("Payroll").InsertAsync(model.Id, model);
    }

    public async Task UpdateAsync(PayrollModel model)
    {
        var cluster = await clusterProvider.GetClusterAsync();
        var bucket = await cluster.BucketAsync("AccountAutomation");
        _ = await bucket.Collection("Payroll").ReplaceAsync(model.Id, model);
    }

    public async Task<IEnumerable<PayrollModel>> GetAllAsync()
    {
        var cluster = await clusterProvider.GetClusterAsync();
        var result = await cluster.QueryAsync<PayrollModel>("SELECT p.* FROM AccountAutomation._default.Payroll as p WHERE p.isDeleted = false ORDER BY p.createdDate DESC");
        return await result.ToListAsync();
    }

    public async Task<PayrollModel> GetByIdAsync(string id)
    {
        var cluster = await clusterProvider.GetClusterAsync();
        var bucket = await cluster.BucketAsync("AccountAutomation");
        var result = await bucket.Collection("Payroll").GetAsync(id);
        return result.ContentAs<PayrollModel>()!;
    }

    public async Task DeleteByIdAsync(string id)
    {
        var cluster = await clusterProvider.GetClusterAsync();
        await cluster.QueryAsync<bool>($"UPDATE AccountAutomation._default.Payroll as p SET p.isDeleted = true WHERE p.isDeleted = false AND p.id = {id}");
    }
}
