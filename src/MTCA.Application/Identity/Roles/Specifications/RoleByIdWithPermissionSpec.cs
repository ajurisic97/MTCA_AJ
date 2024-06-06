using Ardalis.Specification;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Roles.Specifications;
public class RoleByIdWithPermissionSpec : SingleResultSpecification<Role>
{
    public RoleByIdWithPermissionSpec(Guid id)
    {
        Query.Where(x => x.Id == id)
            .Include(x => x.Permissions);
    }
}


