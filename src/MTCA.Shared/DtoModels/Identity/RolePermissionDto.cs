using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Shared.DtoModels.Identity;
public class RolePermissionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
}
