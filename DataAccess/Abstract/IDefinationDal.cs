using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IDefinationDal
{

    Task<bool> UpdateAsync(object model);
    Task<IEnumerable<T>> GetAllAsync<T>(int companyId);
    Task<T> GetByIdAsync<T>(string id);
    Task<IEnumerable<ExpenseTypeModel>> GetAllStockExpenses(int companyId);
    Task<IEnumerable<ExpenseTypeModel>> GetExpenseListTypes(int companyId);
    Task<int> InsertAsync(object model);
}
