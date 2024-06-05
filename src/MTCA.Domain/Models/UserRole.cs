using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Domain.Models;

public class UserRole
{
    public Guid RoleId { get; private set; }
    public Guid UserId { get; private set; }

    public UserRole()
    {
    }

    private UserRole(Guid roleId, Guid userId)
    {
        RoleId = roleId;
        UserId = userId;
    }

    public static UserRole Assign(Guid roleId, Guid userId)
    {
        return new UserRole(roleId, userId);
    }

    public void Update(Guid roleId)
    {
        RoleId = roleId;
    }
}
