using MTCA.Shared.Enums;

namespace MTCA.API.Contracts.Catalog.Region;
/// <summary>
/// CreateRegionRequest
/// </summary>
/// <param name="Name"></param>
/// <param name="ParentId"></param>
/// <param name="Type"></param>
/// <param name="CustomRegionName"></param>
/// <param name="Longitude"></param>
/// <param name="Latitude"></param>
public record CreateRegionRequest(
    string Name,
    Guid? ParentId,
    RegionTypeEnum Type,
    string CustomRegionName,
    decimal? Longitude,
    decimal? Latitude);