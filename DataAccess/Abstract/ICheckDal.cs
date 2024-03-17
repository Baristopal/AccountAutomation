using Entities.Concrete;

namespace DataAccess.Abstract;

public interface ICheckDal
{
    Task Add(CheckModel model);
    Task Update(CheckModel model);
    Task<IEnumerable<CheckModel>> GetAll(int companyId);
    Task<CheckModel> GetById(string id);
    Task<decimal> GetChecksTotalAmount(string processType,int companyId);
}
