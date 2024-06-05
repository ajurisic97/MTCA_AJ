using Microsoft.Extensions.Logging;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Interfaces;
using MTCA.Application.Commons.Models;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Multitenancy.Tenants.Deactivate;
internal class DeactivateTenantCommandHandler : ICommandHandler<DeactivateTenantCommand, CommandResponse<bool>>
{

    private readonly ICurrentTenantService _currentTenantService;

    public DeactivateTenantCommandHandler(ICurrentTenantService currentTenantService)
    {
        _currentTenantService = currentTenantService;
    }
    public async Task<Result<CommandResponse<bool>>> Handle(DeactivateTenantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _currentTenantService.DeactivateAsync(request.ApiKey, cancellationToken);
            var response = new CommandResponse<bool>(new List<bool> { result });

            return response;

        }
        catch (Exception ex)
        {
            return Result.Failure<CommandResponse<bool>>(new Error(
                "Error",
                ex.Message,
                Shared.Enums.LogTypeEnum.Error));
        }
    }
}
