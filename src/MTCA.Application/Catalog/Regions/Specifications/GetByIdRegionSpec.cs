using Ardalis.Specification;
using MTCA.Domain.Models;
using MTCA.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.Regions.Specifications;
public class GetByIdRegionSpec : SingleResultSpecification<Region>
{
    public GetByIdRegionSpec(Guid id)
    {
        Query
            .Where(x => x.Id == id)
             .Include(x => x.Cities)
            .Include(x => x.Streets)
            .Include(x => x.Countries)
            .Include(x => x.Children)
            .Include(x => x.Parent);
    }
}
