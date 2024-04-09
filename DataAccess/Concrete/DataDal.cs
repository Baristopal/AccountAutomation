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
        StringBuilder builder = new StringBuilder();
        builder.Append($"SELECT * FROM Data WITH(NOLOCK) WHERE IsDeleted = 0 AND CompanyId = {companyId}");
        builder.Append($" AND MaturityDate BETWEEN '{searchKeys.StartDate?.ToString("yyyy-MM-dd 00:00:00")}' AND '{searchKeys.EndDate?.ToString("yyyy-MM-dd 23:59:59")}'");

        builder.Append(" ORDER BY CreatedDate DESC");

        var query = builder.ToString();

        var result = await _dbConnection.QueryAsync<DataModel>(query);
        return result;
    }

    public async Task<IEnumerable<DataModel>> GetAll(int companyId)
    {
        string query = "SELECT * FROM Data WITH(NOLOCK) WHERE IsDeleted = 0 AND CompanyId=@CompanyId ORDER BY CreatedDate";
        var p = new
        {
            CompanyId = companyId
        };
        var result = await _dbConnection.QueryAsync<DataModel>(query, p, commandType: CommandType.Text);
        return result;
    }

    public async Task<IEnumerable<DataModel>> GetAllForInstant(int companyId)
    {
        string query = "SELECT * FROM Data WITH(NOLOCK) WHERE IsDeleted = 0 AND CompanyId=@CompanyId AND ExpenseType is not null AND InstantName is not null ORDER BY CreatedDate";
        var p = new
        {
            CompanyId = companyId
        };

        var result = await _dbConnection.QueryAsync<DataModel>(query, p, commandType: CommandType.Text);
        return result;
    }

    public async Task<bool> Update(DataModel model)
    {
        return await _dbConnection.UpdateAsync(model);
    }

    public async Task<DataModel> GetById(string id)
    {
        string query = "SELECT * FROM Data WHERE IsDeleted=0 AND Id=@Id";
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
                        AND d.ProcessType IN('PAY','COLLECT')
                        AND d.CompanyId = @CompanyId
                        ORDER BY d.CreatedDate";

        var result = await _dbConnection.QueryAsync<CaseModel>(query, new { CompanyId = companyId });
        return result;
    }

    public async Task<IEnumerable<DataModel>> GetAllDataWithStockExpenses(int companyId)
    {
        string sql = @$"SELECT * From Data WHERE IsDeleted = 0 AND CompanyId = {companyId} AND ExpenseType IN(SELECT Name From ExpenseType WHERE IsDeleted = 0 AND IsStocked = 1)";

        var result = await _dbConnection.QueryAsync<DataModel>(sql);
        return result;
    }

    public async Task<IEnumerable<DataModel>> GetAllNotPaidInvoices(int companyId, int instantId)
    {
        string sqlQuery = @$"SELECT * FROM Data WHERE IsDeleted = 0 AND CompanyId = {companyId} 
                            AND InstantId = {instantId}
                            AND ProcessType IN('BUY', 'SELL') 
                            AND Status IN ('NOT_PAID', 'PARTIALLY_PAID')
                            ORDER BY MaturityDate";

        var result = await _dbConnection.QueryAsync<DataModel>(sqlQuery);
        return result;
    }
}
