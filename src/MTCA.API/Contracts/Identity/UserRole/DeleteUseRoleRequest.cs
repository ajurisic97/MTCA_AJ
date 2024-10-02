namespace MTCA.API.Contracts.Identity.UserRole;

/// <summary>
/// UpdateUseRoleRequest
/// </summary>
/// <param name="UserId"></param>
/// <param name="RoleId"></param>
public record DeleteUseRoleRequest(
    Guid UserId,
    Guid RoleId);