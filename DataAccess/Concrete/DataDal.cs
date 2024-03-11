using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete;
public class DataDal : IDataDal
{
    private readonly INoSqlHelper _noSqlHelper;

    public DataDal(INoSqlHelper noSqlHelper)
    {
        _noSqlHelper = noSqlHelper;
    }

    public async Task Insert(DataModel model)
    {
        await _noSqlHelper.InsertAsync(model.Id, model);
    }

    public async Task<IEnumerable<DataModel>> GetAll()
    {
        string query = "SELECT d.* FROM Data._default.Data as d WHERE d.isDeleted = false ORDER BY DATE_FORMAT_STR(d.createdDate, '1111-11-11T00:00:00Z')";
        var result = await _noSqlHelper.QueryAsync<DataModel>(query);
        return result;
    }

    public async Task Update(DataModel model)
    {
        await _noSqlHelper.UpdateAsync(model.Id, model);
    }

    public async Task<DataModel> GetById(string id)
    {
        return await _noSqlHelper.GetByIdAsync<DataModel>(id);
    }

    public async Task<IEnumerable<CaseModel>> GetCase()
    {
        string query = @"SELECT 
                        d.processDate
                        ,d.description
                        ,d.processType
                        ,d.tlTotalAmount
                        ,d.expenseType
                        ,d.currency
                        ,d.salesType
                        ,d.currencyTotalAmount
                        FROM Data._default.Data AS d 
                        WHERE d.isDeleted = FALSE
                        AND d.processType IN['ÖDEME','TAHSİLAT']
                        ORDER BY DATE_FORMAT_STR(d.createDate, '1111-11-11T00:00:00Z')";

        var result = await _noSqlHelper.QueryAsync<CaseModel>(query);
        return result;
    }

    public async Task<IEnumerable<DataModel>> GetAllDataWithStockExpenses()
    {
        string query = @"select d.* From Data._default.Data as d where d.isDeleted=false 
                        AND d.expenseType IN(select RAW e.name From Data._default.ExpenseTypes as e where e.isStocked=true)";

        var result = await _noSqlHelper.QueryAsync<DataModel>(query);
        return result;
    }
}
