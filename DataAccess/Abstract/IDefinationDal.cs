using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IDefinationDal
{
    Task InsertAsync<T>(string documentId, T model);
    Task UpdateAsync<T>(string documentId, T model);
    Task<IEnumerable<T>> GetAllAsync<T>(int companyId);
    Task<T> GetByIdAsync<T>(string id);
    Task<IEnumerable<ExpenseTypeModel>> GetAllStockExpenses(int companyId);
}
