using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache
{
    public class RedisCacheService : ICacheService
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly RedisCacheOptions _redisCacheOptions;
        private readonly ICacheService _inMemoryCacheService;
        private readonly IMemoryCache _memoryCache;

        public RedisCacheService(RedisCacheOptions redisCacheOptions, IMemoryCache memoryCache)
        {
            _redisCacheOptions = redisCacheOptions;
            _memoryCache = memoryCache;

            string redisConnectionString = _redisCacheOptions.RedisConnKey;
            if (!string.IsNullOrWhiteSpace(redisConnectionString))
            {
                _redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);
            }

            _inMemoryCacheService = new InMemoryCacheService(_memoryCache);
        }

        public void Set<TItem>(string key, object value) where TItem : class
        {
            var db = _redisConnection.GetDatabase();
            var serializedValue = JsonConvert.SerializeObject(value);
            db.StringSet(key, serializedValue);

            _inMemoryCacheService.Set<TItem>(key, value);
        }

        public object Get<TEntity>(string key) where TEntity : class
        {
            var inMemoryValue = _inMemoryCacheService.Get<TEntity>(key);
            if (inMemoryValue != null)
            {
                return inMemoryValue;
            }

            var db = _redisConnection.GetDatabase();
            var cachedValue = db.StringGet(key);
            if (cachedValue.HasValue)
            {
                var deserializedValue = JsonConvert.DeserializeObject<TEntity>(cachedValue);

                _inMemoryCacheService.Set<TEntity>(key, deserializedValue);
                return deserializedValue;
            }

            return null;
        }

        public void Remove<TEntity>(string key) where TEntity : class
        {
            var db = _redisConnection.GetDatabase();
            db.KeyDelete(key);

            _inMemoryCacheService.Remove<TEntity>(key);
        }
    }
}
