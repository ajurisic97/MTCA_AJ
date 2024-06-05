using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Domain.Errors;
public static class DomainErrors
{
    public static class UserError
    {
        public static readonly Error UserAlreadyExists = new(
                "AlreadyExists",
                "The specified Username is already in use");

        public static readonly Error InvalidCredentials = new(
                "InvalidCredentials",
                "The provided credentials are invalid");

        public static readonly Error UserNotFound = new Error(
                "NotFound",
                "The User was not found!");

        public static readonly Error RefreshToken = new Error(
                "InvalidRefreshToken",
                "Invalid Refresh Token!");

    }

    public static class UserRoleError
    {
        public static readonly Error UserRoleAlreadyExists = new(
                "AlreadyExists",
                "The specified Role for User is already in use");
    }

    public static class RoleError
    {
        public static readonly Error RoleNameAlreadyExists = new Error(
                "AlreadyExists",
                "The Role with specified Name already in use");
    }

    public static class PersonError
    {
        public static readonly Error PersonAlreadyExists = new Error(
                "AlreadyExists",
                "The Person with specified FirstName and LastName already in use");
    }

}
