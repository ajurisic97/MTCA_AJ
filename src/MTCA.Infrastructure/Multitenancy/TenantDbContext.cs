using Microsoft.EntityFrameworkCore;
using MTCA.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Multitenancy;
public class TenantDbContext : DbContext
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options)
        : base(options)
    {

    }

    public DbSet<ERPTenantInfo> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ERPTenantInfo>().ToTable(TableNames.Tenants, SchemaNames.MultiTenancy);


    }
}
