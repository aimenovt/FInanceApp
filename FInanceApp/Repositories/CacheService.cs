using FInanceApp.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace FInanceApp.Repositories
{
    public class CacheService : ICacheService
    {
       private IDatabase _cacheDb;

        public CacheService()
        {
            var redis = ConnectionMultiplexer.Connect("redis_cache:6379"); //name of service of docker:port
            _cacheDb = redis.GetDatabase();
        }

        public T GetData<T>(string key)
        {
            var value = _cacheDb.StringGet(key);

            if (!string.IsNullOrEmpty(value))
            {
                var @object = JsonSerializer.Deserialize<T>(value);
                return @object;
            }

            return default;
        }

        public object RemoveData(string key)
        {
            var exist = _cacheDb.KeyExists(key);

            if (exist)
            {
                return _cacheDb.KeyDelete(key);
            }

            return false;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            return _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expiryTime);
        }
    }
}
