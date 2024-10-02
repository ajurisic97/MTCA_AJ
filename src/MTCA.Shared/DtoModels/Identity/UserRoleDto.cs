using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Shared.DtoModels.Identity;
public class UserRoleDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
}
