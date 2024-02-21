using Entities.Concrete;
using Entities.Models;
using Library.Entities.Attributes;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;

namespace Core.Utilities.Helpers;
public class MongoDbHelper : IMongoDbHelper
{
    private readonly IMongoDatabase mongoDatabase;

    public MongoDbHelper(IOptions<MongoDbSettings> mongoDbSettigns)
    {
        var mongoClient = new MongoClient(mongoDbSettigns.Value.ConnectionString);
        mongoDatabase = mongoClient.GetDatabase(mongoDbSettigns.Value.DatabaseName);
    }

    public async Task<List<T>> GetAllAsync<T>(FilterDefinition<T> filter)
    {

        var config = typeof(T).GetCustomAttribute(typeof(NoSqlConfigAttribute)) as NoSqlConfigAttribute;
        var collection = mongoDatabase.GetCollection<T>(config.CollectionName);
        var result = await collection.Find(FilterDefinition<T>.Empty).ToListAsync();
        return result;
    }

    public async Task<T> GetByIdAsync<T>(string id)
    {
        var config = typeof(T).GetCustomAttribute(typeof(NoSqlConfigAttribute)) as NoSqlConfigAttribute;
        var collection = mongoDatabase.GetCollection<T>(config.CollectionName);

        var result = await collection.Find(Builders<T>.Filter.Eq("_id", id)).SingleOrDefaultAsync();
        return result;
    }

    public async Task InsertAsync<T>(T model)
    {
        var config = typeof(T).GetCustomAttribute(typeof(NoSqlConfigAttribute)) as NoSqlConfigAttribute;
        var collection = mongoDatabase.GetCollection<T>(config.CollectionName);
        await collection.InsertOneAsync(model);
    }

    public async Task UpdateAsync<T>(T model)
    {
        var config = typeof(T).GetCustomAttribute(typeof(NoSqlConfigAttribute)) as NoSqlConfigAttribute;
        var collection = mongoDatabase.GetCollection<T>(config.CollectionName);
        await collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", model.GetType().GetProperty("Id").GetValue(model)), model);
    }

}
