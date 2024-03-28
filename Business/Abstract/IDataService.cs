using Entities.Concrete;
using Entities.Dto;
using Entities.Models;

namespace Business.Abstract;

public interface IDataService
{
    Task<BaseResponse<IEnumerable<DataModel>>> GetFinanceTranckings(FinanceTrackingSearchDto searchKeys);
    Task<BaseResponse<IEnumerable<DataModel>>> GetAllData();
    Task<BaseResponse<DataModel>> AddData(DataModel model);
    Task<BaseResponse<DataModel>> UpdateData(DataModel model);
    Task<BaseResponse<DataModel>> GetDataById(string id);
    Task<BaseResponse<IEnumerable<CaseModel>>> GetCase();
    Task<BaseResponse<IEnumerable<DataModel>>> GetAllDataWithStockExpenses();
    Task<BaseResponse<IEnumerable<DataModel>>> GetAllForInstant();
    Task<BaseResponse<IEnumerable<DataModel>>> GetAllNotPaidInvoices(int instantId);
}
