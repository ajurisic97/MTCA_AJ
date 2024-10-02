using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Roles.Create;
public record RoleCreatedEvent : INotification
{
    public Guid Id { get; init; }
}
