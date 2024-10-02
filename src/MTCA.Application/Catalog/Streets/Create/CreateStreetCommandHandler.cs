using MTCA.Application.Catalog.Streets.Create;
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

namespace MTCA.Application.Catalog.Streets.Create;
internal class CreateStreetCommandHandler : ICommandHandler<CreateStreetCommand, CommandResponse<Guid>>
{
    private readonly IRepository<Street> _streetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStreetCommandHandler(IUnitOfWork unitOfWork, IRepository<Street> streetRepository)
    {
        _unitOfWork = unitOfWork;
        _streetRepository = streetRepository;
    }

    public async Task<Result<CommandResponse<Guid>>> Handle(CreateStreetCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var street = Street.Create(request.Name,request.Longitude,request.Latitude,request.CityId,request.RegionId);

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
