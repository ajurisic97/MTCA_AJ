using MTCA.Shared.Enums;
using MTCA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Domain.Models;
public class Region : AuditableEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public RegionTypeEnum RegionType { get; private set; }
    public string CustomRegionName { get; private set; }
    public decimal? Longitude { get; private set; }
    public decimal? Latitude { get; private set; }
    public Guid? ParentId { get; private set; }
    public virtual Region? Parent { get; private set; }
    public ICollection<Region> Children { get; private set; } = new List<Region>();
    public virtual ICollection<Street> Streets { get; private set; } = new List<Street>();
    public virtual ICollection<City> Cities { get; private set; } = new List<City>();
    public virtual ICollection<Country> Countries { get; private set; } = new List<Country>();

    public Region()
    {
        
    }
    private Region(Guid id, string name, RegionTypeEnum regionType, string customRegionName, decimal? longitude, decimal? latitude, Guid? parentId)
    {
        Id = id;
        Name = name;
        RegionType = regionType;
        CustomRegionName = customRegionName;
        Longitude = longitude;
        Latitude = latitude;
        ParentId = parentId;
    }
    public static Region Create(string name, RegionTypeEnum regionType, string customRegionName, decimal? longitude, decimal? latitude, Guid? parentId)
    {
        return new Region(Guid.NewGuid(), name, regionType, customRegionName, longitude, latitude, parentId);
    }
}
