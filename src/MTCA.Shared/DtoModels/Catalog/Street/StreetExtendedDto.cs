using MTCA.Shared.DtoModels.Catalog.City;
using MTCA.Shared.DtoModels.Catalog.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Shared.DtoModels.Catalog.Street;
public class StreetExtendedDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? Latitude { get; set; }
    public CityDto City { get; set; }
    public RegionDto? Region { get; set; }
}
