using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Users.Events;
public record IdentityChangeEvent : INotification
{
    public List<string> UserIds { get; set; }

}
