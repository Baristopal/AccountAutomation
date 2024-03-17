using Business.Abstract;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Business.Concrete;
public class ProductTrackingManager : IProductTrackingService
{
    private readonly IProductTrackingDal _productTrackingDal;
    private readonly ILogger<ProductTrackingManager> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly int _companyId;
    public ProductTrackingManager(IProductTrackingDal productTrackingDal, ILogger<ProductTrackingManager> logger, IHttpContextAccessor httpContextAccessor)
    {
        _productTrackingDal = productTrackingDal;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _companyId = httpContextAccessor.GetCompanyId();
    }

    public async Task<BaseResponse<IEnumerable<ProductTrackingModel>>> GetAllProductTrackingWithStockName()
    {
        try
        {
            var result = await _productTrackingDal.GetAllWithStockName(_companyId);
            return new BaseResponse<IEnumerable<ProductTrackingModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<ProductTrackingModel>>(new List<ProductTrackingModel>(), false, ex.Message);
        }
    }

    public async Task<BaseResponse<ProductTrackingModel>> AddProductTracking(ProductTrackingModel model)
    {
        try
        {
            model.CompanyId = _companyId;
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
            var result = await _productTrackingDal.GetAll(_companyId);
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
