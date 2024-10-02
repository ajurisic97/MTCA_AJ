using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Shared.Models;

public interface IAuditableEntity : ISoftDelete, IMustHaveTenant
{
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; }
    public Guid LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }

}

