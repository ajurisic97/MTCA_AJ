using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MTCA.Domain.Repositories;
using MTCA.Infrastructure.Multitenancy;
using MTCA.Infrastructure.Persistence.Initialization;
using MTCA.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        //Read DbConnection and register it!
        var connectionString = configuration.GetSection("ConnectionStrings:Database").Value;
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDbContext<TenantDbContext>(options =>
        options.UseSqlServer(connectionString));

        services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        services.AddTransient<ApplicationDbInitializer>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddScoped(typeof(IRepository<>),typeof(ApplicationDbRepository<>));

        return services;
    }
}
