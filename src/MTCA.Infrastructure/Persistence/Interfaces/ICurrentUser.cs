using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Persistence.Interfaces;
public interface ICurrentUser
{
    Guid GetUserId();
    string? Username { get; set; }

}
