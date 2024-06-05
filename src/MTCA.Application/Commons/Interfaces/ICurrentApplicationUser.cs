using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Interfaces;
public interface ICurrentApplicationUser
{
    Guid GetUserId();
    string? Username { get; set; }

}