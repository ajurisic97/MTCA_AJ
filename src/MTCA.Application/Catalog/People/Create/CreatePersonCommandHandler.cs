using Ardalis.Specification;
using Microsoft.Extensions.Logging;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.People.Create;

internal class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand, CommandResponse<Guid>>
{
    private readonly IRepository<Person> _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePersonCommandHandler(IUnitOfWork unitOfWork, IRepository<Person> personRepository)
    {
        _unitOfWork = unitOfWork;
        _personRepository = personRepository;
    }

    public async Task<Result<CommandResponse<Guid>>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var person = Person.Create(
                request.FirstName,
                request.LastName);

            await _personRepository.AddAsync(person, cancellationToken);

            await _unitOfWork.SaveChangesAsync();
            var response = new CommandResponse<Guid>([person.Id]);
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
