using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache
{
    public class RedisCacheOptions
    {
        public RedisCacheOptions() { }

        public RedisCacheOptions(string key, bool isRedis) 
        { 
            IsRedis = isRedis;
            RedisConnKey = key;
        }

        public bool IsRedis { get; private set; }
        public string RedisConnKey { get; private set; } = string.Empty;

    }
}
