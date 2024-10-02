using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Domain.Constants;
public static class TableNames
{
    #region Default
    public const string Permissions = nameof(Permissions);
    public const string People = nameof(People);
    public const string Roles = nameof(Roles);
    public const string RolePermissions = nameof(RolePermissions);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Tenants = nameof(Tenants);
    #endregion

    #region Address related tables
    public const string Cities = nameof(Cities);
    public const string Countries = nameof(Countries);
    public const string Streets = nameof(Streets);
    public const string Regions = nameof(Regions);
    #endregion
}

