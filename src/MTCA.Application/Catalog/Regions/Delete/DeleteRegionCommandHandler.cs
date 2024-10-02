using MTCA.Application.Catalog.Regions.Specifications;
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

namespace MTCA.Application.Catalog.Regions.Delete;
internal class DeleteRegionCommandHandler : ICommandHandler<DeleteRegionCommand, CommandResponse<Guid>>
{
    private readonly IRepository<Region> _regionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRegionCommandHandler(IRepository<Region> regionRepository, IUnitOfWork unitOfWork)
    {
        _regionRepository = regionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CommandResponse<Guid>>> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _regionRepository.SingleOrDefaultAsync(new GetByIdRegionSpec(request.Id), cancellationToken);
            if (result == null)
            {
                return Result.Failure<CommandResponse<Guid>>(ApplicationErrors.Region.RegionNotFound);
            }
            //if (result.Children.Count != 0 //ITD)
            //{
            //    return Result.Failure<CommandResponse<Guid>>(ApplicationErrors.Region.RegionCannotBeDeleted);
            //}

            await _regionRepository.DeleteAsync(result, cancellationToken);
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
