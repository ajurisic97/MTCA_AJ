using Microsoft.Extensions.Logging;
using MTCA.Application.Catalog.People.DeleteById;
using MTCA.Application.Catalog.People.Specifications;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Errors;
using MTCA.Application.Commons.Models;
using MTCA.Application.Identity.UserRoles.Specifications;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.People.DeleteById;
internal class DeleteByIdPersonCommandHandler : ICommandHandler<DeleteByIdPersonCommand, CommandResponse<Guid>>
{
    private readonly IRepository<Person> _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteByIdPersonCommandHandler(IRepository<Person> personRepository, IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CommandResponse<Guid>>> Handle(DeleteByIdPersonCommand request, CancellationToken cancellationToken)
    {
        _unitOfWork.BeginTransaction();

        try
        {
            var result = await _personRepository.SingleOrDefaultAsync(new PersonPopulatedSpec(request.Id), cancellationToken);
            if (result == null)
            {
                return Result.Failure<CommandResponse<Guid>>(ApplicationErrors.Person.PersonNotFound);
            }

            await _personRepository.DeleteAsync(result, cancellationToken);
            _unitOfWork.Commit();
            await _unitOfWork.SaveChangesAsync();
            var response = new CommandResponse<Guid>([result.Id]);

            return response;

        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            return Result.Failure<CommandResponse<Guid>>(new Error(
                "Error",
                ex.Message, Shared.Enums.LogTypeEnum.Error));
        }
    }
}
