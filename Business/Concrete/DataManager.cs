using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Business.Concrete;
public class DataManager : IDataService
{
    private readonly IDataDal _dataDal;
    private readonly ILogger<DataManager> _logger;

    public DataManager(IDataDal dataDal, ILogger<DataManager> logger)
    {
        _dataDal = dataDal;
        _logger = logger;
    }

    public async Task<BaseResponse<IEnumerable<DataModel>>> GetAllData()
    {
        try
        {
            var result = await _dataDal.GetAll();
            return new BaseResponse<IEnumerable<DataModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<DataModel>>(new List<DataModel>(), false, ex.Message);
        }
    }

    public async Task<BaseResponse<DataModel>> AddData(DataModel model)
    {
        try
        {
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
            return new BaseResponse<DataModel>(null, false, ex.Message);
        }
    }

    public async Task<BaseResponse<IEnumerable<CaseModel>>> GetCase()
    {
        try
        {
            var result = await _dataDal.GetCase();
            return new BaseResponse<IEnumerable<CaseModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<CaseModel>>(null, false, ex.Message);
        }
    }

    public async Task<BaseResponse<IEnumerable<DataModel>>> GetAllDataWithStockExpenses()
    {
        try
        {
            var result = await _dataDal.GetAllDataWithStockExpenses();
            return new BaseResponse<IEnumerable<DataModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<DataModel>>(new List<DataModel>(), false, ex.Message);
        }
    }
}
