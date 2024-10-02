using Ardalis.Specification;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Users.Specifications;
public class UserByUsernameWithPersonSpec : SingleResultSpecification<User>
{
    public UserByUsernameWithPersonSpec(string username)
    {
        Query.Where(x => x.Username == username)
            .Include(x => x.Person)
            .Include(x => x.Roles);
    }
}
