namespace MTCA.API.Contracts.Identity.User;

/// <summary>
/// SearchUserRequest
/// </summary>
/// <param name="PersonId"></param>
/// <param name="UserName"></param>
public record SearchUserRequest(
    Guid? PersonId,
    string? UserName) : BaseContract;