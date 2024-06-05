using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using MTCA.Application.Commons.Cache;
using MTCA.Application.Commons.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Authentication;
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly ICacheService _cacheService;
    private readonly IPermissionService _permissionService;

    public PermissionAuthorizationHandler(ICacheService cacheService, IPermissionService permissionService)
    {
        _cacheService = cacheService;
        _permissionService = permissionService;
    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {

        string? memberId = context.User.Claims.FirstOrDefault(x => x.Type == CustomClaims.UserId)?.Value;
        if(!Guid.TryParse(memberId, out Guid parsedMemberId))
        {
            return;
        }
        string? apiKey = context.User.Claims.FirstOrDefault(x => x.Type == CustomClaims.ApiKey)?.Value;
        if (!Guid.TryParse(apiKey, out Guid parsedApiKey))
        {
            return;
        }
        

        var permissions = await _cacheService.GetAsync<HashSet<string>>(parsedMemberId.ToString());
        if (permissions == null || permissions.Count == 0)
        {
            var dbPermissions = await _permissionService.GetPermissionsAsync(parsedMemberId);
            await _cacheService.SetAsync(parsedMemberId.ToString(), dbPermissions);
            permissions = dbPermissions;
        }
        

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        #region Stari način
        //HashSet<string> permissions = context
        //    .User
        //    .Claims
        //    .Where(x => x.Type == CustomClaims.Permissions)
        //    .Select(x => x.Value)
        //    .ToHashSet();

        //if (permissions.Contains(requirement.Permission))
        //{
        //    context.Succeed(requirement);
        //}

        //return Task.CompletedTask;
        #endregion
    }
}
