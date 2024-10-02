using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Models;
public class CacheTimeSetupInMinutes
{
    public double users { get; set; }
    public double userRoles { get; set; }
    public double roles { get; set; }
    public double roleUsers { get; set; }
    public double rolePermissions { get; set; }
}
