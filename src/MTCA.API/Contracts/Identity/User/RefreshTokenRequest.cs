namespace MTCA.API.Contracts.Identity.User;

/// <summary>
/// 
/// </summary>
/// <param name="Token"></param>
/// <param name="RefreshToken"></param>
public record RefreshTokenRequest(
    string Token, 
    string RefreshToken);
