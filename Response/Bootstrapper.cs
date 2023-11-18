using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Response
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddResponse(this IServiceCollection services)
        {
            return services;
        }
    }
}
