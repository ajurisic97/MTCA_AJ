using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Users.Create;
public record UserCreatedEvent : INotification
{
    public Guid Id { get; init; }
}
