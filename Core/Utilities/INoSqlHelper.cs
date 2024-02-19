namespace Core.Utilities;

public interface INoSqlHelper
{
    Task<List<T>> QueryAsync<T>(string query);
    Task<T> GetByIdAsync<T>(string dataId);
    Task InsertAsync<T>(string dataId, T data);
    Task UpdateAsync<T>(string dataId, T data);
    Task<bool> ExecuteAsyncV2(string query);
}
