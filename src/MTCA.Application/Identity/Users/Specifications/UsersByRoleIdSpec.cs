using Ardalis.Specification;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Users.Specifications;
public class UsersByRoleIdSpec : Specification<User>
{
    public UsersByRoleIdSpec(Guid roleId)
    {
        Query
            .Where(x => x.Roles.Any(y => y.Id == roleId));
    }
}
