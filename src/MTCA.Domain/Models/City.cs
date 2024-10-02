using MTCA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Domain.Models;
public class City : AuditableEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal? Longitude { get; private set; }
    public decimal? Latitude { get; private set; }
    public Guid CountryId { get; private set; }
    public Guid? RegionId { get; private set; }
    public virtual Country Country { get; private set; }
    public virtual Region? Region { get; private set; }
    public ICollection<Street> Streets { get; private set; } = new List<Street>();

    public City()
    {

    }
    private City(Guid id, string name, decimal? longitude, decimal? latitude, Guid countryId, Guid? regionId)
    {
        Id = id;
        Name = name;
        Longitude = longitude;
        Latitude = latitude;
        CountryId = countryId;
        RegionId = regionId;
    }
    public static City Create(string name, decimal? longitude, decimal? latitude, Guid countryId, Guid? regionId)
    {
        return new City(Guid.NewGuid(), name, longitude, latitude, countryId, regionId);
    }
}
