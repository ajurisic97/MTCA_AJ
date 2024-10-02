namespace MTCA.API.Contracts.Identity.User;

/// <summary>
/// UpdateUserRequest
/// </summary>
/// <param name="Id"></param>
/// <param name="Password"></param>
/// <param name="UserName"></param>
public record UpdateUserRequest(
    Guid Id,
    string? Password,
    string UserName);