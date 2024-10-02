using Ardalis.Specification;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.People.Specifications;
public class PersonByUserIdSpec : SingleResultSpecification<Person>
{
    public PersonByUserIdSpec(Guid userId)
    {
        Query.Where(x => x.Users.Any(x => x.Id == userId));
    }
}
