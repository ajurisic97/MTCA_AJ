using MTCA.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Shared.DtoModels.Catalog.Region;
public class RegionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public RegionTypeEnum RegionType { get; set; }
    public string CustomRegionName { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? Latitude { get; set; }
    public Guid? ParentId { get; set; }
}
