using Microsoft.Extensions.Logging;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Errors;
using MTCA.Application.Commons.Interfaces;
using MTCA.Application.Commons.Models;
using MTCA.Shared.Errors;
using MTCA.Shared.DtoModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Multitenancy.Tenants.GetAll;
internal class GetAllTenantQueryHandler : IQueryHandler<GetAllTenantQuery, QueryResponse<TenantDto>>
{
    private readonly ICurrentTenantService _currentTenantService;

    public GetAllTenantQueryHandler(ICurrentTenantService currentTenantService)
    {
        _currentTenantService = currentTenantService;
    }

    public async Task<Result<QueryResponse<TenantDto>>> Handle(GetAllTenantQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _currentTenantService.GetAllAsync(cancellationToken);
            if(result == null)
            {
                return Result.Failure<QueryResponse<TenantDto>>(ApplicationErrors.CommonError.NoData);
            }
            var response = new QueryResponse<TenantDto>(result, 1, 1, result.Count);

            return response;
        }
        catch (Exception ex)
        {
            return Result.Failure<QueryResponse<TenantDto>>(new Error(
                "Error",
                ex.Message,
                Shared.Enums.LogTypeEnum.Error));
        }

    }
}
