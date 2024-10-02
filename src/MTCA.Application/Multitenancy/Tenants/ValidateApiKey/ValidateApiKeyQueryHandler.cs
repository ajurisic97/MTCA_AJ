using Microsoft.Extensions.Logging;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Interfaces;
using MTCA.Application.Commons.Models;
using MTCA.Shared.Errors;
using MTCA.Shared.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Multitenancy.Tenants.ValidateApiKey;
internal class ValidateApiKeyQueryHandler : IQueryHandler<ValidateApiKeyQuery, QueryResponse<TenantValidationDto>>
{

    private readonly ICurrentTenantService _currentTenantService;

    public ValidateApiKeyQueryHandler(ICurrentTenantService currentTenantService)
    {
        _currentTenantService = currentTenantService;
    }
    public async Task<Result<QueryResponse<TenantValidationDto>>> Handle(ValidateApiKeyQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _currentTenantService.ValidateApiKey(request.ApiKey, cancellationToken);
            var response = new QueryResponse<TenantValidationDto>(new List<TenantValidationDto> { result});
            return response;
        }
        catch (Exception ex)
        {
            return Result.Failure<QueryResponse<TenantValidationDto>>(new Error(
                "Error",
                ex.Message,
                Shared.Enums.LogTypeEnum.Error));
        }

    }
}
