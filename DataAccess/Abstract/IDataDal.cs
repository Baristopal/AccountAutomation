using Entities.Concrete;
using Entities.Dto;

namespace DataAccess.Abstract;

public interface IDataDal
{
    Task Insert(DataModel model);
    Task<IEnumerable<DataModel>> GetAll(FinanceTrackingSearchDto searchKeys);
    Task Update(DataModel model);
    Task<DataModel> GetById(string id);
    Task<IEnumerable<CaseModel>> GetCase();
    Task<IEnumerable<DataModel>> GetAllDataWithStockExpenses();
}
