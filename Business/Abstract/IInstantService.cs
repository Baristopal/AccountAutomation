using Entities.Concrete;
using Entities.Models;

namespace Business.Abstract;

public interface IInstantService
{
    Task<BaseResponse<InstantModel>> AddInstant(InstantModel model);
    Task<BaseResponse<InstantModel>> UpdateInstant(InstantModel model);
    Task<BaseResponse<IEnumerable<InstantModel>>> GetAllInstant();
    Task<BaseResponse<InstantModel>> GetInstantById(string id);
}