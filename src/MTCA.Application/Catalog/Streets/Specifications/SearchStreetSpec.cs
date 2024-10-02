using Ardalis.Specification;
using MTCA.Domain.Models;
using MTCA.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.Streets.Specifications;
public class SearchStreetSpec : Specification<Street>
{
    public SearchStreetSpec(string? name,
    Guid? cityId,
    Guid? regionId,
    int page,
    int pageSize,
    bool counter)
    {
        if (counter)
        {
            Query
            .Where(x=> 
                (cityId == null || x.CityId == cityId) && (regionId == null || x.RegionId == regionId)
            )
            .Search(x => x.Name, "%" + name + "%", name != null);
        }
        else
        {
            Query
            .Search(x => x.Name, "%" + name + "%", name != null)
            .OrderByDescending(x => x.LastModifiedOn)
            .Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
