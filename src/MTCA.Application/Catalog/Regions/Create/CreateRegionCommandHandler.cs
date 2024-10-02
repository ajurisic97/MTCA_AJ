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

namespace MTCA.Application.Catalog.Regions.Create;
internal class CreateRegionCommandHandler : ICommandHandler<CreateRegionCommand, CommandResponse<Guid>>
{
    private readonly IRepository<Region> _regionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRegionCommandHandler(IUnitOfWork unitOfWork, IRepository<Region> regionRepository)
    {
        _unitOfWork = unitOfWork;
        _regionRepository = regionRepository;
    }

    public async Task<Result<CommandResponse<Guid>>> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var region = Region.Create(request.Name,request.Type,request.CustomRegionName,request.Longitude, request.Latitude,request.ParentId);


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
