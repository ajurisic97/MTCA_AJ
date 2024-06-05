using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MTCA.Application.Commons.Interfaces;
using MTCA.Application.Commons.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.Errors;
using MTCA.Infrastructure.Persistence;
using MTCA.Infrastructure.Persistence.Initialization;
using MTCA.Shared.DtoModels;
using MTCA.Shared.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Multitenancy;
public class CurrentTenantService : ICurrentTenantService
{
    private readonly TenantDbContext _tenantDbContext;
    private readonly IConfiguration _configuration;
    private readonly ILogger<CurrentTenantService> _logger;
    private readonly IDatabaseInitializer _dbInitializer;
    private readonly IServiceProvider _serviceProvider;
    public CurrentTenantService(TenantDbContext tenantDbContext, IConfiguration configuration, IServiceProvider serviceProvider, ILogger<CurrentTenantService> logger, IDatabaseInitializer dbInitializer)
    {
        _tenantDbContext = tenantDbContext;
        _configuration = configuration;
        _serviceProvider = serviceProvider;
        _logger = logger;
        _dbInitializer = dbInitializer;
    }

    public string? TenantId { get; set; }
    public Guid ApiKey { get; set; }
    public string ConnectionString { get; set; }
    public bool IsActive { get; set; }

    public async Task<bool> ActivateAsync(Guid tenantApiKey, CancellationToken cancellationToken)
    {
        var tenant = await _tenantDbContext.Tenants.SingleOrDefaultAsync(x => x.ApiKey == tenantApiKey);
        if (tenant != null)
        {
            tenant.Activate();
            await _tenantDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<Result<CommandResponse<Guid>>> CreateTenant(CreateTenantRequest request, CancellationToken cancellationToken)
    {
        ERPTenantInfo newTenant = new ERPTenantInfo(request.Id, request.Name, request.ConnectionString, request.AdminEmail, Guid.NewGuid(), request.Issuer);
        try
        {
            await _tenantDbContext.Tenants.AddAsync(newTenant, cancellationToken);
            await _tenantDbContext.SaveChangesAsync();

            using IServiceScope scopeTenant = _serviceProvider.CreateScope();
            ApplicationDbContext dbContext = scopeTenant.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.SetConnectionString(request.ConnectionString);
            if ((await dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
            {
                _logger.LogInformation($"Applying ApplicationDB Migrations for New '{request.Id}' tenant.");
                await dbContext.Database.MigrateAsync();
            }

            await _dbInitializer.InitializeApplicationDbForTenantAsync(newTenant, cancellationToken);

            var response = new CommandResponse<Guid>(new List<Guid>() { newTenant.ApiKey });
            return response;
        }
        catch (Exception ex)
        {
            _tenantDbContext.Tenants.Remove(newTenant);
            _logger.LogError("CreateTenant error: {0}", ex.Message);
            return Result.Failure<CommandResponse<Guid>>(new Error(
                "Error",
                ex.Message,
                Shared.Enums.LogTypeEnum.Error));
        }
    }

    public async Task<bool> DeactivateAsync(Guid tenantApiKey, CancellationToken cancellationToken)
    {
        var tenant = await _tenantDbContext.Tenants.SingleOrDefaultAsync(x => x.ApiKey == tenantApiKey);
        if(tenant != null)
        {
            tenant.Deactivate();
            await _tenantDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<List<TenantDto>?> GetAllAsync(CancellationToken cancellationToken)
    {
        var tenants = await _tenantDbContext.Tenants.ToListAsync(cancellationToken);
        if (!tenants.Any())
        {
            return null;
        }
        var result = new List<TenantDto>();
        foreach (var tenant in tenants)
        {
            result.Add(new TenantDto
            {
                ValidUpto = tenant.ValidUpto,
                IsActive = tenant.IsActive,
                Identifier = tenant.Identifier,
                Id = tenant.Id,
                ConnectionString = tenant.ConnectionString,
                ApiKey = tenant.ApiKey,
                AdminEmail = tenant.AdminEmail,
                Name = tenant.Name,
            });
        }
        return result;
    }

    public async Task<bool> SetTenant(Guid apiKey)
    {
        var tenantInfo = await _tenantDbContext.Tenants.Where(x => x.ApiKey == apiKey).FirstOrDefaultAsync();
        if (tenantInfo != null)
        {
            TenantId = tenantInfo.Id;
            ApiKey = apiKey;
            IsActive = tenantInfo.IsActive;
            if (tenantInfo.Id == MultitenancyConstants.Root.Id && tenantInfo.ConnectionString == null)
            {
                ConnectionString = _configuration.GetSection("ConnectionStrings:Database").Value;
            }
            else
            {
                ConnectionString = tenantInfo.ConnectionString;
            }
            return true;
        }
        return false;
    }

    public async Task<TenantValidationDto> ValidateApiKey(Guid apiKey, CancellationToken cancellationToken)
    {
        var tenantInfo = await _tenantDbContext.Tenants.Where(x => x.ApiKey == apiKey).FirstOrDefaultAsync(cancellationToken);
        if(tenantInfo == null)
        {
            return new TenantValidationDto 
            { 
                IsActive = false,
                ValidApiKey = false
            };
        }
        return new TenantValidationDto
        {
            IsActive = tenantInfo.IsActive,
            ValidApiKey = true
        }
        ;
    }

    public async Task<bool> SetTenantOnLogin(Guid apiKey)
    {
        var tenantInfo = await _tenantDbContext.Tenants.Where(x => x.ApiKey == apiKey).FirstOrDefaultAsync();
        if (tenantInfo != null)
        {
            //if (!tenantInfo.IsActive)
            //{
            //    return false;
            //}
            TenantId = tenantInfo.Id;
            ApiKey = apiKey;
            IsActive = tenantInfo.IsActive;
            if (tenantInfo.Id == MultitenancyConstants.Root.Id && tenantInfo.ConnectionString == null)
            {
                ConnectionString = _configuration.GetSection("ConnectionStrings:Database").Value;
            }
            else
            {
                ConnectionString = tenantInfo.ConnectionString;
            }
            return true;
        }
        return false;
    }
}
