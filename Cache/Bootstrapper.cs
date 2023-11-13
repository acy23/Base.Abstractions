using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddCache(this IServiceCollection services, RedisCacheOptions options)
        {
            services.AddMemoryCache();
            services.AddSingleton<InMemoryCacheService>();

            bool useRedisCache = options.IsRedis;

            if (useRedisCache)
            {
                services.AddSingleton(options);
                services.AddSingleton<ICacheService, RedisCacheService>();
            }
            else
            {
                services.AddSingleton<ICacheService, InMemoryCacheService>();
            }

            return services;
        }

    }
}
