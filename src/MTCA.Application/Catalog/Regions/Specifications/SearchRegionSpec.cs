using Ardalis.Specification;
using MTCA.Domain.Models;
using MTCA.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.Regions.Specifications;
public class SearchRegionSpec : Specification<Region>
{
    public SearchRegionSpec(string? name,
    Guid? parentId,
    RegionTypeEnum? type,
    string? customRegionName,
    int page,
    int pageSize,
    bool counter)
    {
        if (counter)
        {
            Query
            .Where(x =>
            (parentId == null || x.ParentId == parentId)
                &&
            (type == null || x.RegionType == type)
            )
            .Search(x => x.Name, "%" + name + "%", name != null)
            .Search(x => x.CustomRegionName, "%" + customRegionName + "%", customRegionName != null);
        }
        else
        {
            Query
            .Where(x =>
                (parentId == null || x.ParentId == parentId)
                    &&
                (type == null || x.RegionType == type)
                )
            .Search(x => x.Name, "%" + name + "%", name != null)
            .Search(x => x.CustomRegionName, "%" + customRegionName + "%", customRegionName != null)
            .OrderByDescending(x => x.LastModifiedOn)
            .Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
