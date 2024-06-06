using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Shared.DtoModels.Identity;
public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; } = string.Empty;
}
