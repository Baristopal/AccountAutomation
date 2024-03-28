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
    private readonly IRepositoryBase _dapperRepository;
    private readonly ILogger<DefinationManager> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly int _companyId;
    public DefinationManager(IDefinationDal definationDal, ILogger<DefinationManager> logger, IHttpContextAccessor httpContextAccessor, IRepositoryBase dapperRepository)
    {
        _definationDal = definationDal;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _companyId = httpContextAccessor.GetCompanyId();
        _dapperRepository = dapperRepository;
    }

    public async Task<BaseResponse<T>> Add<T>(T model)
    {
        try
        {
            Type type = model.GetType();
            PropertyInfo propertyInfo = type?.GetProperty("CompanyId");
            propertyInfo.SetValue(model, _companyId);
            await _dapperRepository.InsertAsync(model);
            return new BaseResponse<T>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<T>(false, ex.Message);
        }
    }

    public async Task<BaseResponse<T>> Update<T>(T model)
    {
        try
        {
            await _dapperRepository.UpdateAsync(model);
            return new BaseResponse<T>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<T>(false, ex.Message);
        }

    }

    public async Task<BaseResponse<IEnumerable<ExpenseTypeModel>>> GetExpenseListTypes()
    {
        try
        {
            var result = await _definationDal.GetExpenseListTypes(_companyId);
            return new BaseResponse<IEnumerable<ExpenseTypeModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<ExpenseTypeModel>>([], false, ex.Message);
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
            return new BaseResponse<IEnumerable<T>>([], false, ex.Message);
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
