using Business.Abstract;
using DataAccess.Abstract;
using Entities.Models;
using Microsoft.Extensions.Logging;

namespace Business.Concrete;
public class DefinationManager : IDefinationService
{
    private readonly IDefinationDal _definationDal;
    private readonly ILogger<DefinationManager> _logger;
    public DefinationManager(IDefinationDal definationDal, ILogger<DefinationManager> logger)
    {
        _definationDal = definationDal;
        _logger = logger;
    }

    public async Task<BaseResponse<T>> Add<T>(string documentId, T model)
    {
        try
        {
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
            var result = await _definationDal.GetAllAsync<T>();
            return new BaseResponse<IEnumerable<T>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<T>>(false, ex.Message);
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

}
