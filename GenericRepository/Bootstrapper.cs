using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Threading.Tasks;

namespace GenericRepository
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddGenericRepository(this IServiceCollection services) 
        {
            services.AddScoped<IRepository, Repository>();
            return services;
        }
    }
}
