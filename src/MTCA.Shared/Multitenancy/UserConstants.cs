using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Shared.Multitenancy;
public class UserConstants
{
    public static class Admin
    {
        public const string DefaultId = "686c8a31-ec08-499e-972b-1647d84d5804";
        public const string DefaultPassword = "123Pa$$word!";
        public const string DefaultUsername = "Admin";
    }

    public static class SuperAdmin
    {
        public const string DefaultId = "686c8a31-ec08-499e-972b-1647d84d5805";
        public const string DefaultPassword = "1234Pa$$word!";
        public const string DefaultUsername = "Superadmin";
    }



}

