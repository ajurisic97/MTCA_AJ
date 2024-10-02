﻿namespace MTCA.API.Contracts.Catalog.Street;
/// <summary>
/// 
/// </summary>
/// <param name="Name"></param>
/// <param name="Longitude"></param>
/// <param name="Latitude"></param>
/// <param name="CityId"></param>
/// <param name="RegionId"></param>
public record CreateStreetRequest(
    string Name,
    decimal? Longitude,
    decimal? Latitude,
    Guid CityId,
    Guid? RegionId);
