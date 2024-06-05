using JsonNet.ContractResolvers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MTCA.Application.Commons.Interfaces;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Infrastructure.Multitenancy;
using MTCA.Shared.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Persistence.Initialization;
internal class DatabaseInitializer : IDatabaseInitializer
{
    private readonly TenantDbContext _tenantDbContext;
    private readonly IConfiguration _configuration;
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(TenantDbContext tenantDbContext, IConfiguration configuration, ILogger<DatabaseInitializer> logger, IServiceProvider serviceProvider)
    {
        _tenantDbContext = tenantDbContext;
        _configuration = configuration;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task InitializeDatabasesAsync(CancellationToken cancellationToken)
    {
        await InitializeTenantDbAsync(cancellationToken);
        var a = await _tenantDbContext.Tenants.ToListAsync(cancellationToken);
        foreach (var tenant in await _tenantDbContext.Tenants.ToListAsync(cancellationToken))
        {
            await InitializeApplicationDbForTenantAsync(tenant, cancellationToken);

        }

    }

    public async Task InitializeApplicationDbForTenantAsync(ERPTenantInfo tenant, CancellationToken cancellationToken)
    {

        using var scope = _serviceProvider.CreateScope();
        await scope.ServiceProvider.GetRequiredService<ICurrentTenantService>()
            .SetTenant(tenant.ApiKey);

        await scope.ServiceProvider.GetRequiredService<ApplicationDbInitializer>()
            .InitializeAsync(cancellationToken);
    }

    private async Task InitializeTenantDbAsync(CancellationToken cancellationToken)
    {
        if (_tenantDbContext.Database.GetPendingMigrations().Any())
        {
            _logger.LogInformation("Applying Root Migrations.");
            await _tenantDbContext.Database.MigrateAsync(cancellationToken);
        }

        await SeedRootTenantAsync(cancellationToken);
    }

    private async Task SeedRootTenantAsync(CancellationToken cancellationToken)
    {
        if (await _tenantDbContext.Tenants.FindAsync(new object?[] { MultitenancyConstants.Root.Id }, cancellationToken: cancellationToken) is null)
        {
            var rootTenant = new ERPTenantInfo(
                MultitenancyConstants.Root.Id,
                MultitenancyConstants.Root.Name,
                null,
                MultitenancyConstants.Root.EmailAddress,
                Guid.Parse("b6081453-99b8-457f-b127-d640dbcfc155"));

            rootTenant.SetValidity(DateTime.UtcNow.AddYears(1));

            _tenantDbContext.Tenants.Add(rootTenant);

            await _tenantDbContext.SaveChangesAsync(cancellationToken);
        }
    }


}
