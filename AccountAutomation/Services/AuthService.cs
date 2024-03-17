using Business.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Entities.Models;
using Library.Core.Utilities.Helpers;

namespace WebUI.Services;

public class AuthService
{
    private readonly ICompanyService _companyService;

    public AuthService(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    public async Task<BaseResponse<CompanyModel>> LoginCompany(UserForLoginDto model)
    {
        string message = "";

        try
        {
            var company = await _companyService.GetCompanyByEmail(model.Email);
            if (company.Success == true)
            {

                if (!HashingHelper.VerifyPasswordHash(model.Password, company.Data.PasswordHash, company.Data.PasswordSalt))
                    return new BaseResponse<CompanyModel>(false, "Password is wrong");


                return new BaseResponse<CompanyModel>(company.Data, true);
            }
            message = company.Message;


        }
        catch (Exception ex)
        {
            message = ex.Message;
        }


        return new BaseResponse<CompanyModel>(false, message);
    }
}
