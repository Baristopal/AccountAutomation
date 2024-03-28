using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Entities.Models;
using Library.Core.Utilities.Helpers;
using Microsoft.Extensions.Logging;

namespace Business.Concrete;
public class CompanyManager : ICompanyService
{
    private readonly ICompanyDal _companyDal;
    private readonly ILogger<CompanyManager> _logger;

    public CompanyManager(ICompanyDal companyDal, ILogger<CompanyManager> logger)
    {
        _companyDal = companyDal;
        _logger = logger;
    }

    public async Task<BaseResponse<CompanyModel>> AddCompany(CompanyModel model)
    {
        try
        {
            var result = await GetCompanyByEmail(model.Email);
            if (result.Success && result.Data is not null)
                return new BaseResponse<CompanyModel>(false, "Email already exists");

            HashingHelper.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

            model.PasswordHash = passwordHash;
            model.PasswordSalt = passwordSalt;

            await _companyDal.InsertAsync(model);
            return new BaseResponse<CompanyModel>(model, true);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<CompanyModel>(false, ex.Message);
        }

    }

    public async Task<BaseResponse<CompanyModel>> UpdateCompany(CompanyModel model)
    {
        try
        {
            await _companyDal.UpdateAsync(model);
            return new BaseResponse<CompanyModel>(model, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<CompanyModel>(null, false, ex.Message);
        }

    }

    public async Task<BaseResponse<IEnumerable<CompanyModel>>> GetAllCompanies()
    {
        try
        {
            var result = await _companyDal.GetAllAsync();
            return new BaseResponse<IEnumerable<CompanyModel>>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<IEnumerable<CompanyModel>>(new List<CompanyModel>(), false, ex.Message);
        }
    }

    public async Task<BaseResponse<CompanyModel>> GetCompanyById(int companyId)
    {
        try
        {
            var result = await _companyDal.GetByIdAsync(companyId);
            return new BaseResponse<CompanyModel>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<CompanyModel>(new CompanyModel(), false, ex.Message);
        }

    }

    public async Task<BaseResponse<CompanyModel>> GetCompanyByEmail(string email)
    {
        try
        {
            var result = await _companyDal.GetCompanyByEmail(email);
            return new BaseResponse<CompanyModel>(result, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<CompanyModel>(false, "Email not found");
        }
    }

    public async Task<BaseResponse<CompanyModel>> LoginCompany(UserForLoginDto model)
    {
        try
        {
            var result = await GetCompanyByEmail(model.Email);
            if (!result.Success)
                return new BaseResponse<CompanyModel>(false, "Email not found");

            if (!HashingHelper.VerifyPasswordHash(model.Password, result.Data.PasswordHash, result.Data.PasswordSalt))
                return new BaseResponse<CompanyModel>(false, "Password is wrong");

            return new BaseResponse<CompanyModel>(result.Data, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseResponse<CompanyModel>(false, ex.Message);
        }
    }
}
