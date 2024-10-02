using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Shared.Multitenancy;
public class RoleConstants
{
    public static class Admin
    {
        public const string Id = "7C9E8F12-EDE7-4868-8011-8363108E2D80";
        public const string Name = "admin";
        public const string Description = "Zadana vrijednost za role";
    }

    public static class SuperAdmin
    {
        public const string Id = "223B9037-A123-4113-9504-B289D7B2B974";
        public const string Name = "superadmin";
        public const string Description = "Zadana vrijednost za role";
    }
}
