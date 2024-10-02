using MTCA.Shared.DtoModels.Catalog.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Shared.DtoModels.Catalog.City;
public class CityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? Latitude { get; set; }
    public Guid CountryId { get; set; }
    public Guid? RegionId { get; set; }
}
