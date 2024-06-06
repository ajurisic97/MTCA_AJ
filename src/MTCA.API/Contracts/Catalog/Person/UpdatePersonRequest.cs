namespace MTCA.API.Contracts.Catalog.Person;

/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="FirstName"></param>
/// <param name="LastName"></param>
public record UpdatePersonRequest(
    Guid Id,
    string FirstName,
    string LastName);
