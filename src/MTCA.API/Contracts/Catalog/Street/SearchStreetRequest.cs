namespace MTCA.API.Contracts.Catalog.Street;
/// <summary>
/// 
/// </summary>
/// <param name="Name"></param>
/// <param name="CityId"></param>
/// <param name="RegionId"></param>
public record SearchStreetRequest(
    string? Name,
    Guid? CityId,
    Guid? RegionId) : BaseContract;