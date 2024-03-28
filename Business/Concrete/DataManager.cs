using Business.Abstract;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Business.Concrete;
public class DataManager : IDataService
{
    private readonly IDataDal _dataDal;
    private readonly ILogger<DataManager> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly int _companyId;

    public DataManager(IDataDal dataDal, ILogger<DataManager> logger, IHttpContextAccessor httpContextAccessor)
    {
        _dataDal = dataDal;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _companyId = httpContextAccessor.GetCompanyId();
    }


    public async Task<BaseResponse<IEnumerable<DataModel>>> GetFinanceTranckings(FinanceTrackingSearchDto searchKeys)
    {
        try
        {
            var result = await _dataDal.GetAll(searchKeys, _companyId);
            return new BaseResponse<IEnumerable<DataModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<DataModel>>([], false, ex.Message);
        }
    }

    public async Task<BaseResponse<IEnumerable<DataModel>>> GetAllData()
    {
        try
        {
            var result = await _dataDal.GetAll(_companyId);
            return new BaseResponse<IEnumerable<DataModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<DataModel>>([], false, ex.Message);
        }
    }

    public async Task<BaseResponse<IEnumerable<DataModel>>> GetAllForInstant()
    {
        try
        {
            var result = await _dataDal.GetAllForInstant(_companyId);
            return new BaseResponse<IEnumerable<DataModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<DataModel>>([], false, ex.Message);
        }
    }

    public async Task<BaseResponse<DataModel>> AddData(DataModel model)
    {
        try
        {
            model.CompanyId = _companyId;
            await _dataDal.Insert(model);
            return new BaseResponse<DataModel>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<DataModel>(false, ex.Message);
        }
    }

    public async Task<BaseResponse<DataModel>> UpdateData(DataModel model)
    {
        try
        {
            await _dataDal.Update(model);
            return new BaseResponse<DataModel>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<DataModel>(false, ex.Message);
        }
    }

    public async Task<BaseResponse<DataModel>> GetDataById(string id)
    {
        try
        {
            var result = await _dataDal.GetById(id);
            return new BaseResponse<DataModel>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<DataModel>(new DataModel(), false, ex.Message);
        }
    }

    public async Task<BaseResponse<IEnumerable<CaseModel>>> GetCase()
    {
        try
        {
            var result = await _dataDal.GetCase(_companyId);
            return new BaseResponse<IEnumerable<CaseModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<CaseModel>>([], false, ex.Message);
        }
    }

    public async Task<BaseResponse<IEnumerable<DataModel>>> GetAllDataWithStockExpenses()
    {
        try
        {
            var result = await _dataDal.GetAllDataWithStockExpenses(_companyId);
            return new BaseResponse<IEnumerable<DataModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<DataModel>>([], false, ex.Message);
        }
    }

    public async Task<BaseResponse<IEnumerable<DataModel>>> GetAllNotPaidInvoices(int instantId)
    {
        try
        {
            var result = await _dataDal.GetAllNotPaidInvoices(_companyId, instantId);
            return new BaseResponse<IEnumerable<DataModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<DataModel>>([], false, ex.Message);
        }
    }
}
