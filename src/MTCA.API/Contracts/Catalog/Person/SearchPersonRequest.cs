namespace MTCA.API.Contracts.Catalog.Person;

/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="FullName"></param>
public record SearchPersonRequest(
    Guid? Id,
    string? FullName) : BaseContract;