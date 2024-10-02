namespace MTCA.API.Contracts.Catalog.Region;
/// <summary>
/// UpdateRegionRequest
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="CustomRegionName"></param>
/// <param name="Longitude"></param>
/// <param name="Latitude"></param>
public record UpdateRegionRequest(
    Guid Id,
    string Name,
    string CustomRegionName,
    decimal? Longitude,
    decimal? Latitude);