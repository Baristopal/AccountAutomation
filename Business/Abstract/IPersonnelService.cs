using Entities.Concrete;
using Entities.Models;

namespace Business.Abstract;

public interface IPersonnelService
{
    Task<BaseResponse<PersonnelModel>> InsertAsync(PersonnelModel model);
Task<BaseResponse<PersonnelModel>> UpdateAsync(PersonnelModel model);
Task<BaseResponse<IEnumerable<PersonnelModel>>> GetAllAsync();
Task<BaseResponse<PersonnelModel>> GetByIdAsync(string id);
    Task<BaseResponse> DeleteByIdAsync(string id);

}
