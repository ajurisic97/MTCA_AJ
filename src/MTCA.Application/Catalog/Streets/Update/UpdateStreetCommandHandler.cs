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

namespace MTCA.Application.Catalog.Streets.Update;
internal class UpdateStreetCommandHandler : ICommandHandler<UpdateStreetCommand, CommandResponse<Guid>>
{
    private readonly IRepository<Street> _streetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStreetCommandHandler(IUnitOfWork unitOfWork, IRepository<Street> streetRepository)
    {
        _unitOfWork = unitOfWork;
        _streetRepository = streetRepository;
    }

    public async Task<Result<CommandResponse<Guid>>> Handle(UpdateStreetCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var street = await _streetRepository.GetByIdAsync(request.Id, cancellationToken);
            if (street == null)
            {
                return Result.Failure<CommandResponse<Guid>>(ApplicationErrors.Street.StreetNotFound);
            }
            street.Update(request.Name, request.RegionId, request.Longitude, request.Latitude);
            await _streetRepository.AddAsync(street, cancellationToken);

            await _unitOfWork.SaveChangesAsync();
            var response = new CommandResponse<Guid>([street.Id]);
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
