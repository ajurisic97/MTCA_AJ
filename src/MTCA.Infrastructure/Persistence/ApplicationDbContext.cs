using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MTCA.Application.Commons.Interfaces;
using MTCA.Domain.Models;
using MTCA.Shared.Models;
using MTCA.Infrastructure.Persistence.Configurations;
using MTCA.Infrastructure.Persistence.Extensions;
using System.Reflection.Metadata;
using static System.Formats.Asn1.AsnWriter;

namespace MTCA.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly ICurrentTenantService _currentTenantService;
    public string CurrentTenantId { get; set; }
    public string CurrentTenantConnectionString { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentTenantService currentTenantService)
        : base(options)
    {
        _currentTenantService = currentTenantService;
        CurrentTenantId = _currentTenantService.TenantId;
        CurrentTenantConnectionString = _currentTenantService.ConnectionString;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

        modelBuilder.AppendGlobalQueryFilter<ISoftDelete>(s => s.DeletedOn == null);
        modelBuilder.AppendGlobalQueryFilter<IMustHaveTenant>(s => s.TenantId == CurrentTenantId);

    }

    // On Configuring -- dynamic connection string, fires on every request
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string tenantConnectionString = CurrentTenantConnectionString;
        if (!string.IsNullOrEmpty(tenantConnectionString)) // use tenant db if one is specified
        {
            _ = optionsBuilder.UseSqlServer(tenantConnectionString);
        }
    }
}