using System.Text.Json;
using backend.Data;
using backend.interfaces;
using StackExchange.Redis;

namespace backend.services;
public class CacheService : ICacheService
{
    private IDatabase cacheDB;
    public CacheService(RedisContext redisContext)
    {
        var redis = ConnectionMultiplexer.Connect(redisContext.RedisURL);
        cacheDB = redis.GetDatabase();
    }

    public T GetData<T>(string key)
    {
        var value = cacheDB.StringGet(key);
        if (!string.IsNullOrEmpty(value))
            return JsonSerializer.Deserialize<T>(value);
        return default;
    }

    public object RemoveDate(string key)
    {
        var exists = cacheDB.KeyExists(key);
        if (exists)
            return cacheDB.KeyDelete(key);
        return false;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
        return cacheDB.StringSet(key, JsonSerializer.Serialize(value), expiryTime);
    }
}
