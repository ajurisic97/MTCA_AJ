

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.People.Update;
public class PersonUpdatedEvent : INotification
{
    public Guid Id { get; init; }
}
