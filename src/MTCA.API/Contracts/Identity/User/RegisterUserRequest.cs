namespace MTCA.API.Contracts.Identity.User;

/// <summary>
/// RegisterUserRequest
/// </summary>
/// <param name="UserName"></param>
/// <param name="Password"></param>
/// <param name="PersonId"></param>
public sealed record RegisterUserRequest(
    string UserName,
    string Password,
    Guid? PersonId);
