using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Business.Concrete;
public class PayrollManager : IPayrollService
{
    private readonly IPayrollDal _payrollDal;
    private readonly ILogger<PayrollManager> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly int _companyId;
    public PayrollManager(IPayrollDal payrollDal, ILogger<PayrollManager> logger, IHttpContextAccessor httpContextAccessor)
    {
        _payrollDal = payrollDal;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BaseResponse<PayrollModel>> InsertAsync(PayrollModel model)
    {
        try
        {
            model.CompanyId = _companyId;
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
            var result = await _payrollDal.GetAllAsync(_companyId);
            return new BaseResponse<IEnumerable<PayrollModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<PayrollModel>>([], false, ex.Message);
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
            return new BaseResponse<PayrollModel>(new PayrollModel(), false, ex.Message);
        }
    }


}
