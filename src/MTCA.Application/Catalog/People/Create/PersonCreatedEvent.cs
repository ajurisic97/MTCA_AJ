using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.People.Create;
public record PersonCreatedEvent : INotification
{
    public Guid Id { get; init; }
}
