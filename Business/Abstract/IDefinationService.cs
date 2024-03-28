using Entities.Concrete;
using Entities.Models;

namespace Business.Abstract;

public interface IDefinationService
{
    Task<BaseResponse<T>> Update<T>(T model);
    Task<BaseResponse<IEnumerable<T>>> GetAll<T>();
    Task<BaseResponse<T>> GetById<T>(string id);
    Task<BaseResponse<IEnumerable<ExpenseTypeModel>>> GetAllStockExpenses();
    Task<BaseResponse<IEnumerable<ExpenseTypeModel>>> GetExpenseListTypes();
    Task<BaseResponse<T>> Add<T>(T model);
}
