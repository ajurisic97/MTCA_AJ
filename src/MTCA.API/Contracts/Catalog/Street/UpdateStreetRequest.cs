namespace MTCA.API.Contracts.Catalog.Street;
/// <summary>
/// UpdateStreetRequest
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="RegionId"></param>
/// <param name="Longitude"></param>
/// <param name="Latitude"></param>
public record UpdateStreetRequest(
    Guid Id,
    string Name,
    Guid? RegionId,
    decimal? Longitude,
    decimal? Latitude);
