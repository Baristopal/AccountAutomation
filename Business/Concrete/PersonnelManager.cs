using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;

namespace Business.Concrete;
public class PersonnelManager : IPersonnelService
{
    private readonly IPersonnelDal _personnelDal;
    public PersonnelManager(IPersonnelDal personnelDal)
    {
        _personnelDal = personnelDal;
    }
    public async Task<BaseResponse<PersonnelModel>> InsertAsync(PersonnelModel model)
    {
        await _personnelDal.InsertAsync(model);
        return new BaseResponse<PersonnelModel>(model,true);
    }
    public async Task<BaseResponse<PersonnelModel>> UpdateAsync(PersonnelModel model)
    {
        await _personnelDal.UpdateAsync(model);
        return  new BaseResponse<PersonnelModel>(model,true);
    }
    public async Task<BaseResponse<IEnumerable<PersonnelModel>>> GetAllAsync()
    {
        var result = await _personnelDal.GetAllAsync();
        return new BaseResponse<IEnumerable<PersonnelModel>>(result,true);
    }
    public async Task<BaseResponse<PersonnelModel>> GetByIdAsync(string id)
    {
        var result = await _personnelDal.GetByIdAsync(id);
        return new BaseResponse<PersonnelModel>(result,true);
    }
    public async Task<BaseResponse> DeleteByIdAsync(string id)
    {
        await _personnelDal.DeleteByIdAsync(id);
        return new BaseResponse(true);
    }
}
