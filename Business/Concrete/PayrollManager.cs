using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;

namespace Business.Concrete;
public class PayrollManager : IPayrollService
{
    private readonly IPayrollDal _payrollDal;
    public PayrollManager(IPayrollDal payrollDal)
    {
        _payrollDal = payrollDal;
    }

    public async Task<BaseResponse<PayrollModel>> InsertAsync(PayrollModel model)
    {
        await _payrollDal.InsertAsync(model);
        return new BaseResponse<PayrollModel>(model, true);
    }

    public async Task<BaseResponse<PayrollModel>> UpdateAsync(PayrollModel model)
    {
        await _payrollDal.UpdateAsync(model);
        return new BaseResponse<PayrollModel>(model, true);
    }

    public async Task<BaseResponse<IEnumerable<PayrollModel>>> GetAllAsync()
    {
        var result = await _payrollDal.GetAllAsync();
        return new BaseResponse<IEnumerable<PayrollModel>>(result, true);
    }

    public async Task<BaseResponse<PayrollModel>> GetByIdAsync(string id)
    {
        var result = await _payrollDal.GetByIdAsync(id);
        return new BaseResponse<PayrollModel>(result, true);
    }

    public async Task<BaseResponse> DeleteByIdAsync(string id)
    {
        await _payrollDal.DeleteByIdAsync(id);
        return new BaseResponse(true);
    }

}
