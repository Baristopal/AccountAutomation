using Entities.Concrete;
using Entities.Models;

namespace Business.Abstract;

public interface ICheckService
{
    Task<BaseResponse<CheckModel>> AddCheck(CheckModel model);
    Task<BaseResponse<CheckModel>> UpdateCheck(CheckModel model);
    Task<BaseResponse<IEnumerable<CheckModel>>> GetAllChecks();
    Task<BaseResponse<CheckModel>> GetCheckById(string id);
    Task<BaseResponse<decimal>> GetChecksTotalAmount(string processType);
}

