using AutoMapper;
using MTCA.Application.Catalog.Regions.GetAll;
using MTCA.Application.Catalog.Regions.Specifications;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Errors;
using MTCA.Application.Commons.Models;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.DtoModels.Catalog.Region;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.Regions.GetById;
internal class GetByIdRegionQueryHandler : IQueryHandler<GetByIdRegionQuery, QueryResponse<RegionExtendedDto>>
{
    private readonly IRepository<Region> _regionRepository;
    private IMapper _mapper;


    public GetByIdRegionQueryHandler(IRepository<Region> regionRepository, IMapper mapper)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
    }

    public async Task<Result<QueryResponse<RegionExtendedDto>>> Handle(GetByIdRegionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _regionRepository.SingleOrDefaultAsync(new GetByIdRegionSpec(request.Id), cancellationToken);

            if (result == null)
            {
                return Result.Failure<QueryResponse<RegionExtendedDto>>(ApplicationErrors.CommonError.NoData);

            }

            var resultDto = _mapper.Map<Region, RegionExtendedDto>(result);
            var response = new QueryResponse<RegionExtendedDto>(new List<RegionExtendedDto> { resultDto },1, 1, 1);

            return response;
        }
        catch (Exception ex)
        {
            return Result.Failure<QueryResponse<RegionExtendedDto>>(new Error(
                "Error",
                ex.Message, Shared.Enums.LogTypeEnum.Error));

        }
    }
}
