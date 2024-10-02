using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Domain.Models;

public class RolePermission
{
    public Guid RoleId { get; set; }
    public int PermissionId { get; set; }

    public static RolePermission Create(Guid roleId, int PermissionId)
    {
        return new RolePermission
        {
            RoleId = roleId,
            PermissionId = PermissionId
        };
    }


}
