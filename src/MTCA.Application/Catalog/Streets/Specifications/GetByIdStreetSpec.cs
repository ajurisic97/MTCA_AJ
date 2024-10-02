using Ardalis.Specification;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.Streets.Specifications;
public class GetByIdStreetSpec : SingleResultSpecification<Street>
{
    public GetByIdStreetSpec(Guid id)
    {
        Query
            .Where(x => x.Id == id)
             .Include(x => x.City)
            .Include(x => x.Region);

    }
}
