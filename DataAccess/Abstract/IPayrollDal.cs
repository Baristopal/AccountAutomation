using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IPayrollDal
{
    Task InsertAsync(PayrollModel model);
    Task UpdateAsync(PayrollModel model);
    Task<IEnumerable<PayrollModel>> GetAllAsync();
    Task<PayrollModel> GetByIdAsync(string id);
    Task DeleteByIdAsync(string id);

}
