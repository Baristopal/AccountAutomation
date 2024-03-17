using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IProductTrackingDal
{
    Task<IEnumerable<ProductTrackingModel>> GetAll(int companyId);
    Task<ProductTrackingModel> GetById(string id);
    Task Insert(ProductTrackingModel model);
    Task Update(ProductTrackingModel model);
    Task<IEnumerable<ProductTrackingModel>> GetAllWithStockName(int companyId);
}
