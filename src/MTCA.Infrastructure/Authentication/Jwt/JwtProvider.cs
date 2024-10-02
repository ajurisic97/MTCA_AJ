using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MTCA.Application.Commons.Cache;
using MTCA.Application.Commons.Interfaces;
using MTCA.Application.Identity;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Authentication.Jwt;
internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly IPermissionService _permissionService;
    private readonly ICurrentTenantService _currentTenantService;
    private readonly ICacheService _cacheService;

    public JwtProvider(IOptions<JwtOptions> options, IPermissionService permissionService, ICacheService cacheService, ICurrentTenantService currentTenantService)
    {
        _options = options.Value;
        _permissionService = permissionService;
        _cacheService = cacheService;
        _currentTenantService = currentTenantService;
    }

    public async Task<string> GenerateJwt(User user, DateTime expiryTime)
    {
        var claims = new List<Claim>
        {
            //new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            //new(JwtRegisteredClaimNames.Name, user.Username),
            //new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new(CustomClaims.FirstName,user.Person != null ? user.Person.FirstName : string.Empty),
            new(CustomClaims.LastName,user.Person != null ? user.Person.LastName : string.Empty),
            new(CustomClaims.Username,user.Username),
            new(CustomClaims.UserId,user.Id.ToString()),
            new(CustomClaims.Tenant,_currentTenantService.TenantId.ToString()),
            new(CustomClaims.ApiKey,_currentTenantService.ApiKey.ToString()),
            new(CustomClaims.PersonId,user.Person != null ? user.Person.Id.ToString() : Guid.Empty.ToString()),
        };


        var permissions = await _cacheService.GetAsync<HashSet<string>>(user.ToString());
        if (permissions == null || permissions.Count == 0)
        {
            var dbPermissions = await _permissionService.GetPermissionsAsync(user.Id);
            await _cacheService.SetAsync(user.Id.ToString(), dbPermissions);
            permissions = dbPermissions;
        }

        //var permissions = await _cacheService.GetOrSetAsync(user.Id.ToString(),
        //    () => _permissionService.GetPermissionsAsync(user.Id));


        foreach (var role in user.Roles)
        {
            claims.Add(new(CustomClaims.Roles, role.Name));
        }

        //foreach (string permission in permissions)
        //{
        //    claims.Add(new(CustomClaims.Permissions, permission));
        //}

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            expiryTime,
            signingCredentials);

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }

    public async Task<TokenResponse> GenerateTokenAndUpdateUserAsync(User user)
    {
        var accessExpiryTime = DateTime.Now.AddHours(23);
        var token = await GenerateJwt(user, accessExpiryTime);
        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiryTime = DateTime.Now.AddHours(24);
        user.SetRefreshToken(refreshToken, refreshTokenExpiryTime);

        return new TokenResponse(token, refreshToken, refreshTokenExpiryTime, accessExpiryTime, user.Person != null ? $"{user.Person.FirstName} {user.Person.LastName}" : null, _currentTenantService.TenantId);

    }

    private static string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public Guid? GetUserIdFromToken(string token)
    {
        var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var userId = decodedToken.Claims.Where(x => x.Type == CustomClaims.UserId).Select(x => x.Value).FirstOrDefault();
        if (userId == null)
            return null;
        return Guid.Parse(userId);


    }

}
