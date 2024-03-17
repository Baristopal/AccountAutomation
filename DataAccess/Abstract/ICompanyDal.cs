using Entities.Concrete;
using Entities.Models;

namespace DataAccess.Abstract;

public interface ICompanyDal
{
    Task InsertAsync(CompanyModel model);
    Task UpdateAsync(CompanyModel model);
    Task<IEnumerable<CompanyModel>> GetAllAsync();
    Task<CompanyModel> GetByIdAsync(int companyId);
    Task DeleteByIdAsync(string id);
    Task<CompanyModel> GetCompanyByEmail(string email);
}
