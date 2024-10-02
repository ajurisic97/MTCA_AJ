using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MTCA.Application.Commons.Interfaces;
using MTCA.Domain.Repositories;
using MTCA.Shared.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Persistence;
internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;
    private readonly ICurrentTenantService _currentTenantService;

    public SqlConnectionFactory(IConfiguration configuration, ICurrentTenantService currentTenantService)
    {
        _configuration = configuration;
        _currentTenantService = currentTenantService;
    }

    public SqlConnection CreateConnection(string? conn = null)
    {
        var connectionString = _currentTenantService.TenantId == MultitenancyConstants.Root.Id 
            ? _configuration.GetSection("ConnectionStrings:Database").Value
            : _currentTenantService.ConnectionString;
        if(conn != null)
        {
            connectionString = conn;
        }

        return new SqlConnection(connectionString);
    }
}
