using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Multitenancy;
public class TenantDbContextFactory : IDesignTimeDbContextFactory<TenantDbContext>
{
    public TenantDbContext CreateDbContext(string[] args) // neccessary for EF migration designer to run on this context
    {

        // Build the configuration by reading from the appsettings.json file (requires Microsoft.Extensions.Configuration.Json Nuget Package)
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("Configuration/appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Retrieve the connection string from the configuration
        string connectionString = configuration.GetSection("ConnectionStrings:Database").Value;


        DbContextOptionsBuilder<TenantDbContext> optionsBuilder = new();
        _ = optionsBuilder.UseSqlServer(connectionString);
        return new TenantDbContext(optionsBuilder.Options);
    }
}
