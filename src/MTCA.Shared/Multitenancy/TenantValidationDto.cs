using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Shared.Multitenancy;
public class TenantValidationDto
{
    public bool IsActive { get; set; }
    public bool ValidApiKey { get; set; }
}

