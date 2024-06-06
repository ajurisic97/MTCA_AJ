using Ardalis.Specification;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.UserRoles.Specifications;
public class UserRoleUniqueSpec : SingleResultSpecification<UserRole>
{
    public UserRoleUniqueSpec(Guid userId, Guid roleId)
    {
        Query.Where(x => x.UserId == userId
                    && x.RoleId == roleId);
    }
}