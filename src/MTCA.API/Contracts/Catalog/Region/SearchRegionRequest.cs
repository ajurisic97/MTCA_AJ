using MTCA.Shared.Enums;

namespace MTCA.API.Contracts.Catalog.Region;
/// <summary>
/// SearchRegionRequest
/// </summary>
/// <param name="Name"></param>
/// <param name="ParentId"></param>
/// <param name="Type"></param>
/// <param name="CustomRegionName"></param>
public record SearchRegionRequest(
    string? Name,
    Guid? ParentId,
    RegionTypeEnum? Type,
    string? CustomRegionName) : BaseContract;
