using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Entities.Models;
using Library.Core.Utilities.Helpers;

namespace Business.Concrete;
public class CompanyManager : ICompanyService
{
    private readonly ICompanyDal _companyDal;

    public CompanyManager(ICompanyDal companyDal)
    {
        _companyDal = companyDal;
    }

    public async Task<BaseResponse<CompanyModel>> AddCompany(CompanyModel model)
    {
        var result = await GetCompanyByEmail(model.Email);
        if (result.Success)
            return new BaseResponse<CompanyModel>(false, "Email already exists");

        HashingHelper.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

        model.PasswordHash = passwordHash;
        model.PasswordSalt = passwordSalt;

        await _companyDal.InsertAsync(model);
        return new BaseResponse<CompanyModel>(model, true);
    }

    public async Task<BaseResponse<CompanyModel>> UpdateCompany(CompanyModel model)
    {
        await _companyDal.UpdateAsync(model);
        return new BaseResponse<CompanyModel>(model, true);
    }

    public async Task<BaseResponse<IEnumerable<CompanyModel>>> GetAllCompanies()
    {
        var result = await _companyDal.GetAllAsync();
        return new BaseResponse<IEnumerable<CompanyModel>>(result, true);
    }

    public async Task<BaseResponse<CompanyModel>> GetCompanyById(string id)
    {
        var result = await _companyDal.GetByIdAsync(id);
        return new BaseResponse<CompanyModel>(result, true);
    }

    public async Task<BaseResponse<bool>> DeleteCompanyById(string id)
    {
        await _companyDal.DeleteByIdAsync(id);
        return new BaseResponse<bool>(true, true);
    }

    public async Task<BaseResponse<CompanyModel>> GetCompanyByEmail(string email)
    {
        var result = await _companyDal.GetCompanyByEmail(email);
        if (result == null)
            return new BaseResponse<CompanyModel>(false, "Email not found");

        return new BaseResponse<CompanyModel>(result, true);
    }

    public async Task<BaseResponse<CompanyModel>> LoginCompany(UserForLoginDto model)
    {
        var result = await GetCompanyByEmail(model.Email);
        if (!result.Success)
            return new BaseResponse<CompanyModel>(false, "Email not found");

        if (!HashingHelper.VerifyPasswordHash(model.Password, result.Data.PasswordHash, result.Data.PasswordSalt))
            return new BaseResponse<CompanyModel>(false, "Password is wrong");

        return new BaseResponse<CompanyModel>(result.Data, true);
    }
}
