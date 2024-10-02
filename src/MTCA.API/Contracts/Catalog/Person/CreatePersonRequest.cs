namespace MTCA.API.Contracts.Catalog.Person;
/// <summary>
/// 
/// </summary>
/// <param name="FirstName"></param>
/// <param name="LastName"></param>
public record CreatePersonRequest(
    string FirstName,
    string LastName);
