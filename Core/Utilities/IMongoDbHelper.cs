using MongoDB.Driver;

namespace Core.Utilities;

public interface IMongoDbHelper
{
    Task<List<T>> GetAllAsync<T>(FilterDefinition<T> filter);
    Task<T> GetByIdAsync<T>(string id);
    Task InsertAsync<T>(T model);
    Task UpdateAsync<T>(T model);
}