using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MTCA.Application.Commons.Cache;
using MTCA.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Caching;
public static class Startup
{
    public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration config)
    {
        var settings = config.GetSection(nameof(CacheSettings)).Get<CacheSettings>();
        services.AddMemoryCache();
        services.AddTransient<ICacheService, LocalCacheService>();

        services.Configure<CacheTimeSetupInMinutes>(config.GetSection(nameof(CacheTimeSetupInMinutes)));
        return services;
    }
}
