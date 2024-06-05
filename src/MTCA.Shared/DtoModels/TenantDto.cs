using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Shared.DtoModels;
public class TenantDto
{
    public string Id { get; set; }
    public string Identifier { get; set; }
    public string Name { get; set; }
    public string ConnectionString { get; set; }
    public string AdminEmail { get; set; }
    public bool IsActive { get; set; }
    public DateTime ValidUpto { get; set; }
    public Guid ApiKey { get; set; }
}

