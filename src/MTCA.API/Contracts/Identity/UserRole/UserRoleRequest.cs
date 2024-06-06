namespace MTCA.API.Contracts.Identity.UserRole;

/// <summary>
/// UserRoleRequest
/// </summary>
/// <param name="UserId"></param>
/// <param name="RoleId"></param>
public record UserRoleRequest(
    Guid UserId,
    Guid RoleId);