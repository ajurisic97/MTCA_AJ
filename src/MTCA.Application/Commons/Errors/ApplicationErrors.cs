using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Errors;
internal class ApplicationErrors
{

    public static class CommonError
    {
        public static readonly Error NoData = new Error(
            "NoData",
            "There are no data to fetch for specified entity!");
    }

    public static class GuiPermission
    {
        public static readonly Error GuiPermissionNotFound = new Error(
                "NotFound",
                "The GuiPermission with specified Id was not found!");
    }

    public static class Person
    {
        public static readonly Error PersonNotFound = new Error(
                "NotFound",
                "The Person with specified Id was not found!");
        public static readonly Error PersonCannotBeDeleted = new Error(
                "CannotBeDeleted",
                "The Person is in use in one or more of other tables!");
    }

    public static class Role
    {
        public static readonly Error RoleNotFound = new Error(
                "NotFound",
                "The Role with specified Id was not found!");

        public static readonly Error RoleCannotBeDeleted = new Error(
                "CannotBeDeleted",
                "The Role is in one or more of other tables(UserRoles, GuiPermissionRoles, PermissionRoles) !");
    }

    public static class User
    {
        public static readonly Error UserNotFound = new Error(
                "NotFound",
                "The User with specified Id was not found!");

        public static readonly Error UserAlreadyExist = new Error(
                "AlreadyExists",
                "The User with specified username already exists!");
    }

    public static class UserRole
    {
        public static readonly Error UserRoleNotFound = new Error(
                "NotFound",
                "The UserRole with specified UserId and RoleId was not found!");
    }

    public static class PermissionRole
    {
        public static readonly Error PermissionRoleNotFound = new Error(
                "NotFound",
                "The PermissionRole with specified PermissionId and RoleId was not found!");
    }

    public static class GuiPermissionRole
    {
        public static readonly Error GuiPermissionRoleNotFound = new Error(
                "NotFound",
                "The GuiPermissionRole with specified GuiPermissionId and RoleId was not found!");
    }

    public static class Region
    {
        public static readonly Error RegionNotFound = new Error(
                "NotFound",
                "The Region with specified Id was not found!");

        public static readonly Error RegionCannotBeDeleted = new Error(
                "CannotBeDeleted",
                "The Region is in use in one or more of other tables !");
    }

}
