using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity;
public record TokenResponse(
    string Token,
    string RefreshToken,
    DateTime RefreshTokenExpiryTime,
    DateTime AccessTokenExpiryTime,
    string? FullName,
    string? Tenant);
