using MTCA.Application.Catalog.Streets.Delete;
using MTCA.Application.Catalog.Streets.Specifications;
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

namespace MTCA.Application.Catalog.Streets.Delete;
internal class DeleteStreetCommandHandler : ICommandHandler<DeleteStreetCommand, CommandResponse<Guid>>
{
    private readonly IRepository<Street> _streetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStreetCommandHandler(IRepository<Street> streetRepository, IUnitOfWork unitOfWork)
    {
        _streetRepository = streetRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CommandResponse<Guid>>> Handle(DeleteStreetCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _streetRepository.SingleOrDefaultAsync(new GetByIdStreetSpec(request.Id), cancellationToken);
            if (result == null)
            {
                return Result.Failure<CommandResponse<Guid>>(ApplicationErrors.Street.StreetNotFound);
            }
            // check if any clients etc.

            await _streetRepository.DeleteAsync(result, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            var response = new CommandResponse<Guid>(new List<Guid>() { result.Id });

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
