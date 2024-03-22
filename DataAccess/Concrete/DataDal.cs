using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System.Data;
using System.Text;

namespace DataAccess.Concrete;
public class DataDal : IDataDal
{
    private readonly IDbConnection _dbConnection;

    public DataDal(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<int> Insert(DataModel model)
    {
        return await _dbConnection.InsertAsync(model);
    }

    public async Task<IEnumerable<DataModel>> GetAll(FinanceTrackingSearchDto searchKeys, int companyId)
    {
        StringBuilder stringBuilder = new();

        stringBuilder.Append("SELECT d.* FROM Data._default.Data as d WHERE d.isDeleted = false");

        if (searchKeys.StartDate != null && searchKeys.EndDate != null)
        {
            stringBuilder.Append($" AND (d.maturityDate BETWEEN '{searchKeys.StartDate?.ToString("yyyy-MM-dd")}T00:00:00.0' AND '{searchKeys.EndDate?.ToString("yyyy-MM-dd")}T23:59:00.0')");
        }
        stringBuilder.Append($" AND d.companyId = {companyId}");

        stringBuilder.Append(" ORDER BY DATE_FORMAT_STR(d.createdDate, '1111-11-11T00:00:00Z')");

        string query = stringBuilder.ToString();

        //var result = await _noSqlHelper.QueryAsync<DataModel>(query);
        return new List<DataModel>();
    }

    public async Task<bool> Update(DataModel model)
    {
        return await _dbConnection.UpdateAsync(model);
    }

    public async Task<DataModel> GetById(string id)
    {
        string query = "SELECT * FROM WHERE IsDeleted=0 AND Id=@Id";
        return await _dbConnection.QuerySingleOrDefaultAsync<DataModel>(query, new { Id = id });
    }

    public async Task<IEnumerable<CaseModel>> GetCase(int companyId)
    {
        string query = @$"SELECT 
                        d.processDate
                        ,d.description
                        ,d.processType
                        ,d.tlTotalAmount
                        ,d.expenseType
                        ,d.currency
                        ,d.salesType
                        ,d.currencyTotalAmount
                        FROM Data AS d 
                        WHERE d.IsDeleted = 0
                        AND d.ProcessType IN['PAY','COLLECT']
                        AND d.CompanyId = @CompanyId
                        ORDER BY DATE_FORMAT_STR(d.createDate, '1111-11-11T00:00:00Z')";



        var result = await _dbConnection.QueryAsync<CaseModel>(query, new { CompanyId = companyId });
        return result;
    }

    public async Task<IEnumerable<DataModel>> GetAllDataWithStockExpenses(int companyId)
    {
        string sql = @"SELECT From Data WHERE IsDeleted = 0 AND CompanyId = @CompanyId AND ExpenseType IN[SELECT RAW Name From ExpenseType WHERE IsDeleted = 0 AND IsStocked = 1]";

        var result = await _dbConnection.QueryAsync<DataModel>(sql, new { CompanyId = companyId });
        return result;
    }
}
