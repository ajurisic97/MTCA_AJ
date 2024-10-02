using MTCA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Domain.Models;
public class Street : AuditableEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal? Longitude { get; private set; }
    public decimal? Latitude { get; private set; }
    public Guid CityId { get; private set; }
    public virtual City City { get; private set; }
    public Guid? RegionId { get; private set; }
    public virtual Region? Region { get; private set; }

    public Street()
    {

    }
    private Street(Guid id, string name, decimal? longitude, decimal? latitude, Guid cityId, Guid? regionId)
    {
        Id = id;
        Name = name;
        Longitude = longitude;
        Latitude = latitude;
        CityId = cityId;
        RegionId = regionId;
    }
    public static Street Create(string name, decimal? longitude, decimal? latitude, Guid cityId, Guid? regionId)
    {
        return new Street(Guid.NewGuid(), name, longitude,latitude,cityId,regionId);
    }


    public void Update(string name, Guid? regionId, decimal? longitude, decimal? latitude)
    {
        Name = name;
        Longitude = longitude;
        Latitude = latitude;
        RegionId = regionId;
    }
}