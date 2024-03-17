using Entities.Concrete;
using Entities.Models;

namespace Business.Abstract;

public interface IPayrollService
{
    Task<BaseResponse<PayrollModel>> InsertAsync(PayrollModel model);
    Task<BaseResponse<PayrollModel>> UpdateAsync(PayrollModel model);
    Task<BaseResponse<IEnumerable<PayrollModel>>> GetAllAsync();
    Task<BaseResponse<PayrollModel>> GetByIdAsync(string id);
}
