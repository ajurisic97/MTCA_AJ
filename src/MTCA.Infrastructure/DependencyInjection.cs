using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MTCA.Application.Commons.Interfaces;
using MTCA.Infrastructure.Authentication;
using MTCA.Infrastructure.Authentication.Jwt;
using MTCA.Infrastructure.Authentication.Password;
using MTCA.Infrastructure.Caching;
using MTCA.Infrastructure.Logging;
using MTCA.Infrastructure.Multitenancy;
using MTCA.Infrastructure.Persistence;
using MTCA.Infrastructure.Persistence.Initialization;
using MTCA.Infrastructure.Persistence.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure;
public static class DependencyInjections
{

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config) =>
       builder.UseMiddleware<TenantResolver>();

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICurrentTenantService, CurrentTenantService>();

        #region Authorization
        services.AddAuthorization();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        #endregion

        #region JWT authentication
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        #endregion
        services.AddHttpContextAccessor();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddSingleton<CurrentUser>();
        services.AddSingleton<ICurrentUser>(x => x.GetRequiredService<CurrentUser>());
        services.AddSingleton<ICurrentApplicationUser>(x => x.GetRequiredService<CurrentUser>());

        //services.AddScoped<ICurrentUser, CurrentUser>();
        //services.AddScoped<ICurrentApplicationUser, CurrentUser>();
        services.AddCaching(configuration);
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(LoggerStartup.AddLogger(configuration)));

        services.AddPersistence(configuration);

        return services;

    }


    public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        // Create a new scope to retrieve scoped services
        using var scope = services.CreateScope();

        await scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>()
            .InitializeDatabasesAsync(cancellationToken);
    }

    #region Commented AddAndMigrateTenantDatabases
    //public static async Task<IServiceCollection> AddAndMigrateTenantDatabases(this IServiceCollection services, IConfiguration configuration)
    //{
    //    using IServiceScope scopeTenant = services.BuildServiceProvider().CreateScope();
    //    TenantDbContext tenantDbContext = scopeTenant.ServiceProvider.GetRequiredService<TenantDbContext>();

    //    if (tenantDbContext.Database.GetPendingMigrations().Any())
    //    {
    //        Console.ForegroundColor = ConsoleColor.Blue;
    //        Console.WriteLine("Applying BaseDb Migrations.");
    //        Console.ResetColor();
    //        tenantDbContext.Database.Migrate(); // apply migrations on baseDbContext
    //    }

    //    if (tenantDbContext.Tenants.Find(new object?[] { MultitenancyConstants.Root.Id }) is null)
    //    {
    //        var rootTenant = new ERPTenantInfo(
    //            MultitenancyConstants.Root.Id,
    //            MultitenancyConstants.Root.Name,
    //            null,
    //            MultitenancyConstants.Root.EmailAddress);
    //        tenantDbContext.Tenants.Add(rootTenant);
    //        tenantDbContext.SaveChanges();
    //    }


    //    List<ERPTenantInfo> tenantsInDb = tenantDbContext.Tenants.ToList();
    //    string defaultConnectionString = configuration.GetSection("ConnectionStrings:Database").Value;
    //    foreach (ERPTenantInfo tenant in tenantsInDb)
    //    {
    //        string connectionString = string.IsNullOrEmpty(tenant.ConnectionString) ? defaultConnectionString : tenant.ConnectionString;

    //        using IServiceScope scopeApplication = services.BuildServiceProvider().CreateScope();
    //        ApplicationDbContext dbContext = scopeApplication.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //        ICurrentTenantService tenantService = scopeTenant.ServiceProvider.GetRequiredService<ICurrentTenantService>();
    //        dbContext.Database.SetConnectionString(connectionString);
    //        if (dbContext.Database.GetPendingMigrations().Any())
    //        {
    //            Console.ForegroundColor = ConsoleColor.Blue;
    //            Console.WriteLine($"Applying Migrations for '{tenant.Id}' tenant.");
    //            Console.ResetColor();
    //            dbContext.Database.Migrate();
    //        }
    //        if (dbContext.Database.CanConnect())
    //        {
    //            tenantService.SetTenant(tenant.Id);
    //            var _unitOfWorkRepository = scopeApplication.ServiceProvider.GetService<IUnitOfWork>();
    //            var _manufacturerRepository = scopeApplication.ServiceProvider.GetService<IRepository<Manufacturer>>();

    //            var rootPath = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName + "/MTCA.Infrastructure";
    //            var jsonSettings = new JsonSerializerSettings
    //            {
    //                ContractResolver = new PrivateSetterContractResolver()
    //            };
    //            #region MeasurementUnit
    //            if (!(await _manufacturerRepository!.ListAsync()).Any())
    //            {
    //                List<Manufacturer> data = new List<Manufacturer>();
    //                using (StreamReader r = new StreamReader(rootPath + "/JsonFiles/Manufacturer.json"))
    //                {
    //                    var json = r.ReadToEnd();
    //                    data = JsonConvert.DeserializeObject<List<Manufacturer>>(json, jsonSettings)!;
    //                }
    //                data.ForEach(x => x.TenantId = tenant.Id);
    //                data.ForEach(x => _manufacturerRepository.AddAsync(x));
    //            }
    //            #endregion

    //            await _unitOfWorkRepository!.SaveAndCommitAsync();

    //        }
    //    }
    //    return services;
    //}
    #endregion

}
