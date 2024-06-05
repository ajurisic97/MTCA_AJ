using MTCA.Application.Identity;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Interfaces;
public interface IJwtProvider
{
    Task<TokenResponse> GenerateTokenAndUpdateUserAsync(User user);
    Task<string> GenerateJwt(User user, DateTime expiryTime);
    Guid? GetUserIdFromToken(string token);

}
