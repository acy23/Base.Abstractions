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

        public RedisCacheService()
        {
            string redisConnectionString = "your_redis_connection_string";
            _redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);
        }

        public void Set<TItem>(string key, object value) where TItem : class
        {
            var db = _redisConnection.GetDatabase();
            var serializedValue = JsonConvert.SerializeObject(value);
            db.StringSet(key, serializedValue);
        }

        public object Get<TEntity>(string key) where TEntity : class
        {
            var db = _redisConnection.GetDatabase();
            var cachedValue = db.StringGet(key);
            if (cachedValue.HasValue)
            {
                return JsonConvert.DeserializeObject<TEntity>(cachedValue);
            }
            return null;
        }

        public void Remove<TEntity>(string key) where TEntity : class
        {
            var db = _redisConnection.GetDatabase();
            db.KeyDelete(key);
        }
    }
}
