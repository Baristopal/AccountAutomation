using Entities.Concrete;

namespace DataAccess.Abstract;

public interface ICheckDal
{
    Task<int> Add(CheckModel model);
    Task<bool> Update(CheckModel model);
    Task<IEnumerable<CheckModel>> GetAll(int companyId);
    Task<CheckModel> GetById(string id);
    Task<decimal> GetChecksTotalAmount(string processType,int companyId);
}
