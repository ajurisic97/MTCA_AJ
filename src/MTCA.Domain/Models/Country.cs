using MTCA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Domain.Models;
public class Country : AuditableEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal? Longitude { get; private set; }
    public decimal? Latitude { get; private set; }
    public Guid? RegionId { get; private set; }
    public virtual Region? Region {  get; private set; }
    public ICollection<City> Cities { get; private set; } = new List<City>();

}
