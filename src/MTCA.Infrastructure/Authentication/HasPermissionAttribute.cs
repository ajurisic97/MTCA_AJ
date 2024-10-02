using Microsoft.AspNetCore.Authorization;
using MTCA.Domain.Authorization;
using MTCA.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Authentication;
public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string action, string resource)
        : base(policy: PermissionData.NameFor(action, resource))
    {

    }
}
