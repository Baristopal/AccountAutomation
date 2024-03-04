using Couchbase.Core.Exceptions.KeyValue;
using Couchbase.Extensions.DependencyInjection;
using Library.Entities.Attributes;
using System.Reflection;

namespace Core.Utilities.Helpers;
public class CouchbaseHelper : INoSqlHelper
{
    private readonly IClusterProvider _clusterProvider;
    private readonly IBucketProvider _bucketProvider;

    public CouchbaseHelper(IClusterProvider clusterProvider, IBucketProvider bucketProvider)
    {
        _bucketProvider = bucketProvider;
        _clusterProvider = clusterProvider;
    }

    public async Task<List<T>> QueryAsync<T>(string query)
    {
        var cluster = await _clusterProvider.GetClusterAsync();
        var result = await cluster.QueryAsync<T>(query);

        return await result.ToListAsync().ConfigureAwait(false);
    }

    public async Task<T> GetByIdAsync<T>(string dataId)
    {
        try
        {
            var config = ((typeof(T).GetCustomAttribute(typeof(NoSqlConfigAttribute))) as NoSqlConfigAttribute);

            var bucket = await _bucketProvider.GetBucketAsync(config.BucketName);
            var result = await bucket.Collection(config.CollectionName).GetAsync(dataId).ConfigureAwait(false);

            return result.ContentAs<T>();

        }
        catch (DocumentNotFoundException)
        {
            return default;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task InsertAsync<T>(string dataId, T data)
    {
        var config = ((typeof(T).GetCustomAttribute(typeof(NoSqlConfigAttribute))) as NoSqlConfigAttribute);

        var bucket = await _bucketProvider.GetBucketAsync(config.BucketName);
        _ = await bucket.Collection(config.CollectionName).InsertAsync(dataId, data).ConfigureAwait(false);
    }

    public async Task UpdateAsync<T>(string dataId, T data)
    {
        var config = ((typeof(T).GetCustomAttribute(typeof(NoSqlConfigAttribute))) as NoSqlConfigAttribute);

        var bucket = await _bucketProvider.GetBucketAsync(config.BucketName);
        _ = await bucket.Collection(config.CollectionName).ReplaceAsync(dataId, data).ConfigureAwait(false);
    }

    public async Task<bool> ExecuteAsyncV2(string query)
    {
        var _cluster = await _clusterProvider.GetClusterAsync();
        var result = await _cluster.QueryAsync<bool>(query);

        return result.MetaData.Metrics.MutationCount > 0;
    }

    public async Task<T> SingleOrDefaultAsync<T>(string query)
    {
        var cluster = await _clusterProvider.GetClusterAsync();
        var result = await cluster.QueryAsync<T>(query);

        return await result.FirstOrDefaultAsync().ConfigureAwait(false);
    }
}
