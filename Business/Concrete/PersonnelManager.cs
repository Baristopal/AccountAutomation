using Business.Abstract;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace Business.Concrete;
public class PersonnelManager : IPersonnelService
{
    private readonly IPersonnelDal _personnelDal;
    private readonly ILogger<PersonnelManager> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly int _companyId;
    public PersonnelManager(IPersonnelDal personnelDal, ILogger<PersonnelManager> logger, IHttpContextAccessor httpContextAccessor)
    {
        _personnelDal = personnelDal;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _companyId = httpContextAccessor.GetCompanyId();
    }
    public async Task<BaseResponse<PersonnelModel>> InsertAsync(PersonnelModel model)
    {
        try
        {
            model.CompanyId = _companyId;
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
            var result = await _personnelDal.GetAll(_companyId);
            return new BaseResponse<IEnumerable<PersonnelModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<PersonnelModel>>([], false, ex.Message);
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
            return new BaseResponse<PersonnelModel>(new PersonnelModel(), false, ex.Message);
        }
    }
}
