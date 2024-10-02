using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Serilog.Core;
using Serilog.Core.Enrichers;
using MTCA.Application.Commons.Interfaces;
using MTCA.Application.Commons.Models;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace MTCA.Application.Commons.Behaviors;
internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger;
    private ICurrentTenantService _currentTenantService;
    private ICurrentApplicationUser _currentApplicationUser;
    public RequestLoggingPipelineBehavior(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger, ICurrentTenantService currentTenantService, ICurrentApplicationUser currentApplicationUser)
    {
        _logger = logger;
        _currentTenantService = currentTenantService;
        _currentApplicationUser = currentApplicationUser;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var a = _currentApplicationUser.GetUserId();
        var username = _currentApplicationUser.Username;
        string requestName = typeof(TRequest).Name;
        using (LogContext.PushProperty("Parameters", request, true))
        {
            _logger.LogInformation("Processing request {RequestName} for tenant {Tenant} by user {Username}", requestName, _currentTenantService.TenantId, username);
        }
            
        TResponse result = await next();
        if (result.IsSuccess)
        {
            using (LogContext.PushProperty("Parameters", request, true))
            {
                _logger.LogInformation("Completed request {RequestName} for tenant {Tenant} by user {Username}", requestName, _currentTenantService.TenantId, username);
            }
           
        }
        else
        {
            if(result.Error.LogType == Shared.Enums.LogTypeEnum.Warning)
            {
                
                ILogEventEnricher[] enrichers =
                {
                    new PropertyEnricher("Warning", result.Error, true),
                    new PropertyEnricher("Parameters", request, true)
                };
                using (LogContext.Push(enrichers))
                {
                    _logger.LogWarning("Completed request {RequestName} for tenant {Tenant} by user {Username} with warning", requestName, _currentTenantService.TenantId, username);
                }
            }
            else
            {
                ILogEventEnricher[] enrichers2 =
                {
                    new PropertyEnricher("Error", result.Error, true),
                    new PropertyEnricher("Parameters", request, true)
                };
                using (LogContext.Push(enrichers2))
                {
                    _logger.LogError("Completed request {RequestName} for tenant {Tenant} by user {Username} with error", requestName, _currentTenantService.TenantId, username);
                }
            }
            
        }
        return result;
    }
}
