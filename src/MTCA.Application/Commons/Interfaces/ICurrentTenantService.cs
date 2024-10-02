using MTCA.Application.Commons.Models;
using MTCA.Shared.Errors;
using MTCA.Shared.DtoModels;
using MTCA.Shared.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Interfaces;
public interface ICurrentTenantService
{
    string? TenantId { get; set; }
    Guid ApiKey { get; set; }
    string ConnectionString { get; set; }
    bool IsActive { get; set; }
    Task<bool> SetTenant(Guid tenant);
    Task<bool> SetTenantOnLogin(Guid tenant);
    Task<bool> ActivateAsync(Guid tenantApiKey, CancellationToken cancellationToken);
    Task<bool> DeactivateAsync(Guid tenantApiKey, CancellationToken cancellationToken);
    Task<TenantValidationDto> ValidateApiKey(Guid apiKey, CancellationToken cancellationToken);
    Task<Result<CommandResponse<Guid>>> CreateTenant(CreateTenantRequest request, CancellationToken cancellationToken);
    Task<List<TenantDto>?> GetAllAsync(CancellationToken cancellationToken);

}
