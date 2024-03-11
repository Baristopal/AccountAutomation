using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;
using Microsoft.Extensions.Logging;

namespace Business.Concrete;
public class CheckManager : ICheckService
{
    private readonly ICheckDal _checkDal;
    private readonly ILogger<CheckManager> _logger;

    public CheckManager(ICheckDal checkDal, ILogger<CheckManager> logger)
    {
        _checkDal = checkDal;
        _logger = logger;
    }

    public async Task<BaseResponse<CheckModel>> AddCheck(CheckModel model)
    {
        try
        {
            await _checkDal.Add(model);
            return new BaseResponse<CheckModel>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<CheckModel>(model, false, ex.Message);
        }
    }

    public async Task<BaseResponse<CheckModel>> UpdateCheck(CheckModel model)
    {
        try
        {
            await _checkDal.Update(model);
            return new BaseResponse<CheckModel>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<CheckModel>(model, false, ex.Message);
        }
    }

    public async Task<BaseResponse<IEnumerable<CheckModel>>> GetAllChecks()
    {
        try
        {
            var result = await _checkDal.GetAll();
            return new BaseResponse<IEnumerable<CheckModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<CheckModel>>(new List<CheckModel>(), false, ex.Message);
        }
    }

    public async Task<BaseResponse<CheckModel>> GetCheckById(string id)
    {
        try
        {
            var result = await _checkDal.GetById(id);
            return new BaseResponse<CheckModel>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<CheckModel>(new CheckModel(), false, ex.Message);
        }
    }
}

