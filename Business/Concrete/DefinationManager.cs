using Business.Abstract;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Business.Concrete;
public class DefinationManager : IDefinationService
{
    private readonly IDefinationDal _definationDal;
    private readonly ILogger<DefinationManager> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly int _companyId;
    public DefinationManager(IDefinationDal definationDal, ILogger<DefinationManager> logger, IHttpContextAccessor httpContextAccessor)
    {
        _definationDal = definationDal;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _companyId = httpContextAccessor.GetCompanyId();
    }

    public async Task<BaseResponse<T>> Add<T>(string documentId, T model)
    {
        try
        {
            Type type = typeof(T);
            PropertyInfo propertyInfo = type?.GetProperty("CompanyId");
            propertyInfo.SetValue(model, _companyId);
            await _definationDal.InsertAsync<T>(documentId, model);
            return new BaseResponse<T>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<T>(false, ex.Message);
        }
    }

    public async Task<BaseResponse<T>> Update<T>(string documentId, T model)
    {
        try
        {
            await _definationDal.UpdateAsync<T>(documentId, model);
            return new BaseResponse<T>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<T>(false, ex.Message);
        }

    }

    public async Task<BaseResponse<IEnumerable<T>>> GetAll<T>()
    {
        try
        {
            var result = await _definationDal.GetAllAsync<T>(_companyId);
            return new BaseResponse<IEnumerable<T>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<T>>(new List<T>(),false, ex.Message);
        }
    }

    public async Task<BaseResponse<T>> GetById<T>(string id)
    {
        try
        {
            var result = await _definationDal.GetByIdAsync<T>(id);
            return new BaseResponse<T>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<T>(false, ex.Message);
        }

    }

    public async Task<BaseResponse<IEnumerable<ExpenseTypeModel>>> GetAllStockExpenses()
    {
        try
        {
            var result = await _definationDal.GetAllStockExpenses(_companyId);
            return new BaseResponse<IEnumerable<ExpenseTypeModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<ExpenseTypeModel>>(false, ex.Message);
        }
    }
}
