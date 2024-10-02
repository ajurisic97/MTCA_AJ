﻿using Microsoft.Extensions.Logging;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Interfaces;
using MTCA.Application.Commons.Models;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Multitenancy.Tenants.Activate;
internal class ActivateTenantCommandHandler : ICommandHandler<ActivateTenantCommand, CommandResponse<bool>>
{
    private readonly ICurrentTenantService _currentTenantService;

    public ActivateTenantCommandHandler(ICurrentTenantService currentTenantService)
    {
        _currentTenantService = currentTenantService;
    }
    public async Task<Result<CommandResponse<bool>>> Handle(ActivateTenantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _currentTenantService.ActivateAsync(request.ApiKey, cancellationToken);
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
