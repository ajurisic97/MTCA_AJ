using MTCA.Shared.DtoModels.Catalog.City;
using MTCA.Shared.DtoModels.Catalog.Country;
using MTCA.Shared.DtoModels.Catalog.Street;
using MTCA.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Shared.DtoModels.Catalog.Region;
public class RegionExtendedDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public RegionTypeEnum RegionType { get; set; }
    public string CustomRegionName { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? Latitude { get; set; }
    public RegionDto? Parent { get; set; }
    public List<RegionDto> Children { get; set; } = new List<RegionDto>();
    public List<StreetDto> Streets { get; set; } = new List<StreetDto>();
    public List<CityDto> Cities { get; set; } = new List<CityDto>();
    public List<CountryDto> Countries { get; set; } = new List<CountryDto>();

}
