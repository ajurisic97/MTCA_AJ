namespace MTCA.API.Contracts.Identity.RolePermission;

/// <summary>
/// DeleteRolePermissionRequest
/// </summary>
/// <param name="RoleId"></param>
/// <param name="PermissionIds"></param>
public record DeleteRolePermissionRequest(
    Guid RoleId,
    List<int> PermissionIds);