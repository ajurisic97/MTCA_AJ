using MTCA.Application.Catalog.Regions.Create;
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

namespace MTCA.Application.Catalog.Regions.Update;
internal class UpdateRegionCommandHandler : ICommandHandler<UpdateRegionCommand, CommandResponse<Guid>>
{
    private readonly IRepository<Region> _regionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRegionCommandHandler(IUnitOfWork unitOfWork, IRepository<Region> regionRepository)
    {
        _unitOfWork = unitOfWork;
        _regionRepository = regionRepository;
    }

    public async Task<Result<CommandResponse<Guid>>> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var region = await _regionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (region == null)
            {
                return Result.Failure<CommandResponse<Guid>>(ApplicationErrors.Region.RegionNotFound);
            }
            region.Update(request.Name, request.CustomRegionName, request.Longitude, request.Latitude);
            await _regionRepository.AddAsync(region, cancellationToken);

            await _unitOfWork.SaveChangesAsync();
            var response = new CommandResponse<Guid>([region.Id]);
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
