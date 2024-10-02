using AutoMapper.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MTCA.Application.Commons.Interfaces;
using MTCA.Infrastructure.Authentication;
using MTCA.Infrastructure.Authentication.Jwt;
using MTCA.Infrastructure.Persistence.Interfaces;
using MTCA.Shared.Multitenancy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Multitenancy;
public class TenantResolver
{
    private readonly RequestDelegate _next;
    private readonly JwtOptions _options;
    private readonly ILogger<TenantResolver> _logger;

    public TenantResolver(RequestDelegate next, IOptions<JwtOptions> options, ILogger<TenantResolver> logger)
    {
        _next = next;
        _options = options.Value;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
    {
        _logger.LogInformation("Request starting {Method} {BaseRoute}{Path}{Query}", context.Request.Method, context.Request.Host, context.Request.Path, context.Request.QueryString);
        var stopwatch = Stopwatch.StartNew();
        try
        {
            
            #region Login
            context.Request.Headers.TryGetValue(MultitenancyConstants.TenantIdName, out var tenantFromHeader);
            if (!string.IsNullOrEmpty(tenantFromHeader))
            {
                if (!Guid.TryParse(tenantFromHeader, out Guid parsedMemberId))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    stopwatch.Stop();
                    var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                    _logger.LogInformation("Request finished {Method} {BaseRoute}{Path}{Query} with status {Status} in {TimeElaps} ms", context.Request.Method, context.Request.Host, context.Request.Path, context.Request.QueryString, StatusCodes.Status401Unauthorized, elapsedMilliseconds);


                    return;
                }
                await currentTenantService.SetTenantOnLogin(parsedMemberId!);
                if (!currentTenantService.IsActive)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    stopwatch.Stop();
                    var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                    _logger.LogInformation("Request finished {Method} {BaseRoute}{Path}{Query} with status {Status} in {TimeElaps} ms", context.Request.Method, context.Request.Host, context.Request.Path, context.Request.QueryString, StatusCodes.Status401Unauthorized, elapsedMilliseconds);
                    return;
                }
            }
            #endregion

            #region other requests
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")[1];
            if (token != null)
            {

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = _options.Issuer,
                    ValidAudience = _options.Audience
                };

                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                JwtSecurityToken validatedJwt = (JwtSecurityToken)validatedToken;

                string? tenant = validatedJwt.Claims.FirstOrDefault(x => x.Type == CustomClaims.Tenant)?.Value;
                string? apiKey = validatedJwt.Claims.FirstOrDefault(x => x.Type == CustomClaims.ApiKey)?.Value;
                if (!Guid.TryParse(apiKey, out Guid parsedApiKey))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    stopwatch.Stop();
                    var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                    _logger.LogInformation("Request finished {Method} {BaseRoute}{Path}{Query} with status {Status} in {TimeElaps} ms", context.Request.Method, context.Request.Host, context.Request.Path, context.Request.QueryString, StatusCodes.Status401Unauthorized, elapsedMilliseconds);
                    return;
                }
                if (tenant == MultitenancyConstants.Root.Id)
                {
                    context.Request.Headers.TryGetValue(MultitenancyConstants.ViewTenant, out var tenantApiKey);
                    if (!string.IsNullOrEmpty(tenantApiKey))
                    {
                        if (!Guid.TryParse(tenantApiKey, out Guid parsedMemberId))
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            stopwatch.Stop();
                            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                            _logger.LogInformation("Request finished {Method} {BaseRoute}{Path}{Query} with status {Status} in {TimeElaps} ms", context.Request.Method, context.Request.Host, context.Request.Path, context.Request.QueryString, StatusCodes.Status401Unauthorized, elapsedMilliseconds);
                            return;
                        }
                        await currentTenantService.SetTenantOnLogin(parsedMemberId!);

                    }
                    else
                    {
                        await currentTenantService.SetTenantOnLogin(parsedApiKey);

                    }

                }
                else
                {
                    await currentTenantService.SetTenantOnLogin(parsedApiKey);
                    if (!currentTenantService.IsActive)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        stopwatch.Stop();
                        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                        _logger.LogInformation("Request finished {Method} {BaseRoute}{Path}{Query} with status {Status} in {TimeElaps}  ms", context.Request.Method, context.Request.Host, context.Request.Path, context.Request.QueryString, StatusCodes.Status401Unauthorized, elapsedMilliseconds);
                        return;
                    }
                }


            }

            #endregion
        }
        catch (Exception)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            _logger.LogInformation("Request finished {Method} {BaseRoute}{Path}{Query} with status {Status} in {TimeElaps} ms", context.Request.Method, context.Request.Host, context.Request.Path, context.Request.QueryString, StatusCodes.Status401Unauthorized, elapsedMilliseconds);
            return;
        }

        await _next(context);
        stopwatch.Stop();
        var elapsedMilliseconds2 = stopwatch.ElapsedMilliseconds;
        _logger.LogInformation("Request finished {Method} {BaseRoute}{Path}{Query} with status {Status} in {TimeElaps} ms", context.Request.Method, context.Request.Host, context.Request.Path, context.Request.QueryString, context.Response.StatusCode, elapsedMilliseconds2);
    }
}
