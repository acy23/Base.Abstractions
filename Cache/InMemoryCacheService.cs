using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        public InMemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Set<Titem>(string key, object value) where Titem : class 
        {
            try
            {
                var serializedValue = JsonConvert.SerializeObject(value);
                _cache.Set(key, serializedValue);
            }
            catch(System.Exception ex) 
            {
                throw ex;
            }
        }

        public object Get<TEntity>(string key) where TEntity : class
        {
            _cache.TryGetValue(key.ToString(), out var value);
            return value ?? string.Empty;
        }

        public void Remove<TEntity>(string key) where TEntity : class
        {
            try
            {
                _cache.Remove(key.ToString());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

    }
}
