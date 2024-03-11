using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete;
public class ProductTrackingDal: IProductTrackingDal
{
    private readonly INoSqlHelper _noSqlHelper;

    public ProductTrackingDal(INoSqlHelper noSqlHelper)
    {
        _noSqlHelper = noSqlHelper;
    }

    public async Task<IEnumerable<ProductTrackingModel>> GetAllWithStockName()
    {
        string query = @"SELECT p.* FROM Data._default.ProductTracking as p WHERE p.isDeleted = false 
                        AND p.stockName IN(select RAW e.name From Data._default.ExpenseTypes as e where e.isStocked=true) 
                        ORDER BY p.createdDate";
        var result = await _noSqlHelper.QueryAsync<ProductTrackingModel>(query);
        return result;
    }
    public async Task<IEnumerable<ProductTrackingModel>> GetAll()
    {
        string query = "SELECT p.* FROM Data._default.ProductTracking as p WHERE p.isDeleted = false ORDER BY p.createdDate";
        var result = await _noSqlHelper.QueryAsync<ProductTrackingModel>(query);
        return result;
    }

    public async Task<ProductTrackingModel> GetById(string id)
    {
        var result = await _noSqlHelper.GetByIdAsync<ProductTrackingModel>(id);
        return result;
    }

    public async Task Insert(ProductTrackingModel model)
    {
        await _noSqlHelper.InsertAsync(model.Id, model);
    }

    public async Task Update(ProductTrackingModel model)
    {
        await _noSqlHelper.UpdateAsync(model.Id, model);
    }
}
