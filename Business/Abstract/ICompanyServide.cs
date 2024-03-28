using Entities.Concrete;
using Entities.Dto;
using Entities.Models;

namespace Business.Abstract;

public interface ICompanyService
{
    Task<BaseResponse<CompanyModel>> AddCompany(CompanyModel model);
    Task<BaseResponse<CompanyModel>> UpdateCompany(CompanyModel model);
    Task<BaseResponse<IEnumerable<CompanyModel>>> GetAllCompanies();
    Task<BaseResponse<CompanyModel>> GetCompanyById(int companyId);
    Task<BaseResponse<CompanyModel>> LoginCompany(UserForLoginDto model);
    Task<BaseResponse<CompanyModel>> GetCompanyByEmail(string email);
}