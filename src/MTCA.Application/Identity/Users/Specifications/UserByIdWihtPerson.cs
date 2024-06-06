using Ardalis.Specification;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Users.Specifications;
public class UserByIdWihtPerson : SingleResultSpecification<User>
{
    public UserByIdWihtPerson(Guid id)
    {
        Query.Where(x => x.Id == id)
            .Include(x => x.Person);
    }
}
