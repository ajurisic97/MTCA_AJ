using Ardalis.Specification;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Roles.Specifications;
public class RoleByNameSpec : SingleResultSpecification<Role>
{
    public RoleByNameSpec(string name)
    {
        Query.Where(x => x.Name == name);
    }
}

