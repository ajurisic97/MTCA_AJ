using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Domain.Authorization;

public static class ActionCatalog
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
}
public static class ResourceCatalog
{
    public const string People = nameof(People);
    public const string Roles = nameof(Roles);
    public const string RolePermissions = nameof(RolePermissions);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Tenants = nameof(Tenants);

}

public static class Permissions
{

    public static readonly PermissionData[] _all = new PermissionData[]
    {
        new(0, ActionCatalog.View, ResourceCatalog.People),
        new(0, ActionCatalog.Search, ResourceCatalog.People),
        new(0, ActionCatalog.Create, ResourceCatalog.People),
        new(0, ActionCatalog.Update, ResourceCatalog.People),
        new(0, ActionCatalog.Delete, ResourceCatalog.People),

        new(0, ActionCatalog.View, ResourceCatalog.Users),
        new(0, ActionCatalog.Search, ResourceCatalog.Users),
        new(0, ActionCatalog.Create, ResourceCatalog.Users),
        new(0, ActionCatalog.Update, ResourceCatalog.Users),
        new(0, ActionCatalog.Delete, ResourceCatalog.Users),

        new(0, ActionCatalog.View, ResourceCatalog.Roles),
        new(0, ActionCatalog.Search, ResourceCatalog.Roles),
        new(0, ActionCatalog.Create, ResourceCatalog.Roles),
        new(0, ActionCatalog.Update, ResourceCatalog.Roles),
        new(0, ActionCatalog.Delete, ResourceCatalog.Roles),

        new(0, ActionCatalog.View, ResourceCatalog.RolePermissions),
        new(0, ActionCatalog.Search, ResourceCatalog.RolePermissions),
        new(0, ActionCatalog.Create, ResourceCatalog.RolePermissions),
        new(0, ActionCatalog.Update, ResourceCatalog.RolePermissions),
        new(0, ActionCatalog.Delete, ResourceCatalog.RolePermissions),

        new(0, ActionCatalog.View, ResourceCatalog.UserRoles),
        new(0, ActionCatalog.Search, ResourceCatalog.UserRoles),
        new(0, ActionCatalog.Create, ResourceCatalog.UserRoles),
        new(0, ActionCatalog.Update, ResourceCatalog.UserRoles),
        new(0, ActionCatalog.Delete, ResourceCatalog.UserRoles),

        new(0, ActionCatalog.View, ResourceCatalog.Tenants),
        new(0, ActionCatalog.Search, ResourceCatalog.Tenants),
        new(0, ActionCatalog.Create, ResourceCatalog.Tenants),
        new(0, ActionCatalog.Update, ResourceCatalog.Tenants),
        new(0, ActionCatalog.Delete, ResourceCatalog.Tenants),
    };
}

public record PermissionData(int PermissionId, string Action, string Resource)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}
