using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;
using Microsoft.Extensions.Logging;

namespace Business.Concrete;
public class InstantManager : IInstantService
{
    private readonly IInstantDal _instantDal;
    private readonly ILogger<InstantManager> _logger;

    public InstantManager(IInstantDal instantDal, ILogger<InstantManager> logger)
    {
        _instantDal = instantDal;
        _logger = logger;
    }

    public async Task<BaseResponse<InstantModel>> AddInstant(InstantModel model)
    {
        try
        {
            await _instantDal.Add(model);
            return new BaseResponse<InstantModel>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<InstantModel>(model, false, ex.Message);
        }
    }

    public async Task<BaseResponse<InstantModel>> UpdateInstant(InstantModel model)
    {
        try
        {
            await _instantDal.Update(model);
            return new BaseResponse<InstantModel>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<InstantModel>(model, false, ex.Message);
        }
    }

    public async Task<BaseResponse<IEnumerable<InstantModel>>> GetAllInstant()
    {
        try
        {
            var result = await _instantDal.GetAll();
            return new BaseResponse<IEnumerable<InstantModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<InstantModel>>(new List<InstantModel>(), false, ex.Message);
        }
    }

    public async Task<BaseResponse<InstantModel>> GetInstantById(string id)
    {
        try
        {
            var result = await _instantDal.GetById(id);
            return new BaseResponse<InstantModel>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<InstantModel>(new InstantModel(), false, ex.Message);
        }
    }
}
