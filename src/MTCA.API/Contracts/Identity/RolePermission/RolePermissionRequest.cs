namespace MTCA.API.Contracts.Identity.RolePermission;

/// <summary>
/// RolePermissionRequest
/// </summary>
/// <param name="RoleId"></param>
/// <param name="PermissionIds"></param>
public record RolePermissionRequest(
    Guid RoleId,
    List<int> PermissionIds);