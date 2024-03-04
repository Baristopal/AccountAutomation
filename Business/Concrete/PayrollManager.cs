using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Business.Concrete;
public class PayrollManager : IPayrollService
{
    private readonly IPayrollDal _payrollDal;
    private readonly ILogger<PayrollManager> _logger;
    public PayrollManager(IPayrollDal payrollDal, ILogger<PayrollManager> logger)
    {
        _payrollDal = payrollDal;
        _logger = logger;
    }

    public async Task<BaseResponse<PayrollModel>> InsertAsync(PayrollModel model)
    {
        try
        {
            await _payrollDal.InsertAsync(model);
            return new BaseResponse<PayrollModel>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<PayrollModel>(false, ex.Message);
        }

    }

    public async Task<BaseResponse<PayrollModel>> UpdateAsync(PayrollModel model)
    {
        try
        {
            await _payrollDal.UpdateAsync(model);
            return new BaseResponse<PayrollModel>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<PayrollModel>(false, ex.Message);
        }
    }

    public async Task<BaseResponse<IEnumerable<PayrollModel>>> GetAllAsync()
    {
        try
        {
            var result = await _payrollDal.GetAllAsync();
            return new BaseResponse<IEnumerable<PayrollModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<PayrollModel>>(null, false, ex.Message);
        }
    }

    public async Task<BaseResponse<PayrollModel>> GetByIdAsync(string id)
    {
        try
        {
            var result = await _payrollDal.GetByIdAsync(id);
            return new BaseResponse<PayrollModel>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<PayrollModel>(null, false, ex.Message);
        }
    }

    public async Task<BaseResponse> DeleteByIdAsync(string id)
    {
        try
        {
            await _payrollDal.DeleteByIdAsync(id);
            return new BaseResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse(false, ex.Message);
        }
    }

}
