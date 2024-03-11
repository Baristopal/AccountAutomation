using Entities.Concrete;
using Entities.Models;

namespace Business.Abstract;

public interface IProductTrackingService
{
    Task<BaseResponse<ProductTrackingModel>> AddProductTracking(ProductTrackingModel model);
    Task<BaseResponse<IEnumerable<ProductTrackingModel>>> GetAllProductTracking();
    Task<BaseResponse<ProductTrackingModel>> UpdateProductTracking(ProductTrackingModel model);
    Task<BaseResponse<ProductTrackingModel>> GetProductTrackingById(string id);
    Task<BaseResponse<IEnumerable<ProductTrackingModel>>> GetAllProductTrackingWithStockName();
}