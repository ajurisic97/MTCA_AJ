using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Domain.Models;

public class Permission
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;

    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}
