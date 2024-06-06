using Ardalis.Specification;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.People.Specifications;
public class PersonPopulatedSpec : SingleResultSpecification<Person>
{
    public PersonPopulatedSpec(Guid id)
    {
        Query.Where(x => x.Id == id)
            .Include(x => x.Users)
                .ThenInclude(x => x.Roles);
            
    }
}