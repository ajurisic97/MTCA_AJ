using MTCA.Shared.DtoModels.Catalog.Country;
using MTCA.Shared.DtoModels.Catalog.Region;
using MTCA.Shared.DtoModels.Catalog.Street;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Shared.DtoModels.Catalog.City;
public class CityExtendedDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? Latitude { get; set; }
    public CountryDto Country { get; set; }
    public RegionDto? Region { get; set; }
    public List<StreetDto> Streets { get; set; } = new List<StreetDto>();

}
