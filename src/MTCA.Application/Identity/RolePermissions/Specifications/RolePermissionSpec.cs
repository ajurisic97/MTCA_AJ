using Ardalis.Specification;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.RolePermissions.Specifications;
public class RolePermissionSpec : Specification<RolePermission>
{
    public RolePermissionSpec(Guid roleId, List<int> permissionIds)
    {
        Query.Where(x => x.RoleId == roleId && permissionIds.Contains(x.PermissionId));
    }
}
