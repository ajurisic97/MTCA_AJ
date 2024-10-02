using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Shared.Models;

public abstract class AuditableEntity : IAuditableEntity, ISoftDelete, IMustHaveTenant
{
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; private set; }
    public Guid LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public Guid? DeletedBy { get; set; }
    public string? TenantId { get; set; }

    protected AuditableEntity()
    {
        CreatedOn = DateTime.Now;
        LastModifiedOn = DateTime.Now;
    }
}
