namespace MTCA.API.Contracts.Identity.User;

/// <summary>
/// LoginRequest
/// </summary>
/// <param name="Username"></param>
/// <param name="Password"></param>
public sealed record LoginRequest(
    string Username,
    string Password);
