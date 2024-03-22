using Entities.Concrete;
using Entities.Dto;

namespace DataAccess.Abstract;

public interface IDataDal
{
    Task<int> Insert(DataModel model);
    Task<IEnumerable<DataModel>> GetAll(FinanceTrackingSearchDto searchKeys, int companyId);
    Task<bool> Update(DataModel model);
    Task<DataModel> GetById(string id);
    Task<IEnumerable<CaseModel>> GetCase(int companyId);
    Task<IEnumerable<DataModel>> GetAllDataWithStockExpenses(int companyId);
}
