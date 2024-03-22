using Entities.Concrete;
using Entities.Models;

namespace DataAccess.Abstract;

public interface ICompanyDal
{
    Task<int> InsertAsync(CompanyModel model);
    Task<bool> UpdateAsync(CompanyModel model);
    Task<IEnumerable<CompanyModel>> GetAllAsync();
    Task<CompanyModel> GetByIdAsync(int companyId);
    Task<CompanyModel> GetCompanyByEmail(string email);
}
