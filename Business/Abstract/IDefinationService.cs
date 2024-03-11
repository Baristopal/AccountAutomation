using Entities.Concrete;
using Entities.Models;

namespace Business.Abstract;

public interface IDefinationService
{
    Task<BaseResponse<T>> Add<T>(string documentId, T model);
    Task<BaseResponse<T>> Update<T>(string documentId, T model);
    Task<BaseResponse<IEnumerable<T>>> GetAll<T>();
    Task<BaseResponse<T>> GetById<T>(string id);
    Task<BaseResponse<IEnumerable<ExpenseTypeModel>>> GetAllStockExpenses();
}
