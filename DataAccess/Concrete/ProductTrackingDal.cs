using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Data;

namespace DataAccess.Concrete;
public class ProductTrackingDal : IProductTrackingDal
{
    private readonly IDbConnection _dbConnection;

    public ProductTrackingDal(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<ProductTrackingModel>> GetAllWithStockName(int companyId)
    {
        string query = @$"SELECT p.* FROM ProductTracking as p WHERE p.isDeleted = 0 
                        AND p.stockName IN(select RAW e.name From Data._default.ExpenseTypes as e where e.isStocked=true) 
                        AND p.companyId = {companyId}
                        ORDER BY p.createdDate";
        var result = await _dbConnection.QueryAsync<ProductTrackingModel>(query);
        return result;
    }
    public async Task<IEnumerable<ProductTrackingModel>> GetAll(int companyId)
    {
        string query = $"SELECT p.* FROM ProductTracking as p WHERE p.IsDeleted = 0 AND p.CompanyId = {companyId} ORDER BY p.CreatedDate";
        var result = await _dbConnection.QueryAsync<ProductTrackingModel>(query);
        return result;
    }

    public async Task<ProductTrackingModel> GetById(string id)
    {
        var result = await _dbConnection.GetAsync<ProductTrackingModel>(id);
        return result;
    }

    public async Task Insert(ProductTrackingModel model)
    {
        await _dbConnection.InsertAsync(model);
    }

    public async Task Update(ProductTrackingModel model)
    {
        await _dbConnection.UpdateAsync(model);
    }
}
