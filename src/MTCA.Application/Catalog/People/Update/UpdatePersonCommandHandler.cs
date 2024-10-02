using MediatR;
using Microsoft.Extensions.Logging;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Errors;
using MTCA.Application.Commons.Models;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.People.Update;


internal class UpdatePersonCommandHandler : ICommandHandler<UpdatePersonCommand, CommandResponse<Guid>>
{
    private readonly IRepository<Person> _personRepository;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePersonCommandHandler(IPublisher publisher, IUnitOfWork unitOfWork, IRepository<Person> personRepository)
    {
        _publisher = publisher;
        _unitOfWork = unitOfWork;
        _personRepository = personRepository;
    }

    public async Task<Result<CommandResponse<Guid>>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _personRepository.GetByIdAsync(request.Id, cancellationToken);
            if (data == null)
            {
                return Result.Failure<CommandResponse<Guid>>(ApplicationErrors.Person.PersonNotFound);
            }

            data.Update(
                request.FirstName,
                request.LastName);

            await _unitOfWork.SaveChangesAsync();
            var personUpdatedEvent = new PersonUpdatedEvent
            {
                Id = data.Id
            };
            await _publisher.Publish(personUpdatedEvent, cancellationToken);
            var response = new CommandResponse<Guid>([data.Id]);

            return response;
        }
        catch (Exception ex)
        {
            return Result.Failure<CommandResponse<Guid>>(new Error(
                "Error",
                ex.Message, Shared.Enums.LogTypeEnum.Error));
        }

    }
}
