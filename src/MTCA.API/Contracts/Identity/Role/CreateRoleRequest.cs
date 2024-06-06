namespace MTCA.API.Contracts.Identity.Role;

/// <summary>
/// CreateRoleRequest
/// </summary>
/// <param name="Name"></param>
/// <param name="Description"></param>
public record CreateRoleRequest(
    string Name,
    string? Description);