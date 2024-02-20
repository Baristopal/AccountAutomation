using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;
using Microsoft.Extensions.Logging;

namespace Business.Concrete;
public class PersonnelManager : IPersonnelService
{
    private readonly IPersonnelDal _personnelDal;
    private readonly ILogger<PersonnelManager> _logger;
    public PersonnelManager(IPersonnelDal personnelDal, ILogger<PersonnelManager> logger)
    {
        _personnelDal = personnelDal;
        _logger = logger;
    }
    public async Task<BaseResponse<PersonnelModel>> InsertAsync(PersonnelModel model)
    {
        try
        {
            await _personnelDal.InsertAsync(model);
            return new BaseResponse<PersonnelModel>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<PersonnelModel>(false, ex.Message);
        }
    }
    public async Task<BaseResponse<PersonnelModel>> UpdateAsync(PersonnelModel model)
    {
        try
        {
            await _personnelDal.UpdateAsync(model);
            return new BaseResponse<PersonnelModel>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<PersonnelModel>(false, ex.Message);
        }
    }
    public async Task<BaseResponse<IEnumerable<PersonnelModel>>> GetAllAsync()
    {
        try
        {
            var result = await _personnelDal.GetAllAsync();
            return new BaseResponse<IEnumerable<PersonnelModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<PersonnelModel>>(new List<PersonnelModel>(), false, ex.Message);
        }
    }
    public async Task<BaseResponse<PersonnelModel>> GetByIdAsync(string id)
    {
        try
        {
            var result = await _personnelDal.GetByIdAsync(id);
            return new BaseResponse<PersonnelModel>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<PersonnelModel>(new PersonnelModel(), false, ex.Message);
        }
    }
    public async Task<BaseResponse> DeleteByIdAsync(string id)
    {
        try
        {
            await _personnelDal.DeleteByIdAsync(id);
            return new BaseResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse(false, ex.Message);
        }
    }
}
