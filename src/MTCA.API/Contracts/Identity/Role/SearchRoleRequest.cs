namespace MTCA.API.Contracts.Identity.Role;

/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
public record SearchRoleRequest(
    Guid? Id) : BaseContract;