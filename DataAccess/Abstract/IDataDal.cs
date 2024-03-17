using Entities.Concrete;
using Entities.Dto;

namespace DataAccess.Abstract;

public interface IDataDal
{
    Task Insert(DataModel model);
    Task<IEnumerable<DataModel>> GetAll(FinanceTrackingSearchDto searchKeys, int companyId);
    Task Update(DataModel model);
    Task<DataModel> GetById(string id);
    Task<IEnumerable<CaseModel>> GetCase(int companyId);
    Task<IEnumerable<DataModel>> GetAllDataWithStockExpenses(int companyId);
}
