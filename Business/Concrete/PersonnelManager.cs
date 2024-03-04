using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Serilog;


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
            await _personnelDal.Insert(model);
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
            await _personnelDal.Update(model);
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
            var result = await _personnelDal.GetAll();
            return new BaseResponse<IEnumerable<PersonnelModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<PersonnelModel>>(null, false, ex.Message);
        }
    }
    public async Task<BaseResponse<PersonnelModel>> GetByIdAsync(string id)
    {
        try
        {
            var result = await _personnelDal.GetById(id);
            return new BaseResponse<PersonnelModel>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<PersonnelModel>(null, false, ex.Message);
        }
    }
}
