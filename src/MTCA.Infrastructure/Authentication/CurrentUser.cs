using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MTCA.Application.Commons.Interfaces;
using MTCA.Infrastructure.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Authentication;
public class CurrentUser : ICurrentUser, ICurrentApplicationUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Username { get; set; }

    public Guid GetUserId()
    {
        if(_httpContextAccessor.HttpContext != null)
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaims.UserId);
            if (user == null)
            {
                return Guid.Empty;
            }
            var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaims.Username);
            Username = username == null ? null : username.Value;
            return Guid.Parse(user.Value);

        }
        return Guid.Empty;
        
    }
}
