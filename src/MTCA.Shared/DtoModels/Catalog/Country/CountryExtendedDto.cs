using MTCA.Shared.DtoModels.Catalog.City;
using MTCA.Shared.DtoModels.Catalog.Region;
using MTCA.Shared.DtoModels.Catalog.Street;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Shared.DtoModels.Catalog.Country;
public class CountryExtendedDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? Latitude { get; set; }
    public RegionDto? Region { get; set; }
    public List<CityDto> Cities { get; set; } = new List<CityDto>();
}
