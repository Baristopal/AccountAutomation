using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;
using Microsoft.Extensions.Logging;

namespace Business.Concrete;
public class ProductTrackingManager : IProductTrackingService
{
    private readonly IProductTrackingDal _productTrackingDal;
    private readonly ILogger<ProductTrackingManager> _logger;
    public ProductTrackingManager(IProductTrackingDal productTrackingDal, ILogger<ProductTrackingManager> logger)
    {
        _productTrackingDal = productTrackingDal;
        _logger = logger;
    }

    public async Task<BaseResponse<ProductTrackingModel>> AddProductTracking(ProductTrackingModel model)
    {
        try
        {
            await _productTrackingDal.Insert(model);
            return new BaseResponse<ProductTrackingModel>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<ProductTrackingModel>(false, ex.Message);
        }
    }

    public async Task<BaseResponse<IEnumerable<ProductTrackingModel>>> GetAllProductTracking()
    {
        try
        {
            var result = await _productTrackingDal.GetAll();
            return new BaseResponse<IEnumerable<ProductTrackingModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<ProductTrackingModel>>(new List<ProductTrackingModel>(), false, ex.Message);
        }
    }

    public async Task<BaseResponse<ProductTrackingModel>> UpdateProductTracking(ProductTrackingModel model)
    {
        try
        {
            await _productTrackingDal.Update(model);
            return new BaseResponse<ProductTrackingModel>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<ProductTrackingModel>(false, ex.Message);
        }
    }

    public async Task<BaseResponse<ProductTrackingModel>> GetProductTrackingById(string id)
    {
        try
        {
            var result = await _productTrackingDal.GetById(id);
            return new BaseResponse<ProductTrackingModel>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<ProductTrackingModel>(false, ex.Message);
        }
    }
}

public interface IProductTrackingService
{
    Task<BaseResponse<ProductTrackingModel>> AddProductTracking(ProductTrackingModel model);
    Task<BaseResponse<IEnumerable<ProductTrackingModel>>> GetAllProductTracking();
    Task<BaseResponse<ProductTrackingModel>> UpdateProductTracking(ProductTrackingModel model);
    Task<BaseResponse<ProductTrackingModel>> GetProductTrackingById(string id);
}