using AutoMapper;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.Regions.GetAll;
internal class GetAllRegionQueryHandler : IQueryHandler<GetAllRegionQuery, QueryResponse<RegionDto>>
{
    private readonly IRepository<Region> _regionRepository;
    private IMapper _mapper;


    public GetAllRegionQueryHandler(IRepository<Region> regionRepository, IMapper mapper)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
    }

    public async Task<Result<QueryResponse<RegionDto>>> Handle(GetAllRegionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _regionRepository.ListAsync(new SearchRegionSpec(request.Name,request.ParentId,request.Type,request.CustomRegionName,request.Page,request.PageSize,false),cancellationToken);

            if (!result.Any())
            {
                return Result.Failure<QueryResponse<RegionDto>>(ApplicationErrors.CommonError.NoData);

            }

            var counter = await _regionRepository.CountAsync(new SearchRegionSpec(request.Name, request.ParentId, request.Type, request.CustomRegionName, request.Page, request.PageSize, true), cancellationToken);

            var resultDto = _mapper.Map<List<Region>, List<RegionDto>>(result);
            var response = new QueryResponse<RegionDto>(resultDto, request.Page, request.PageSize, counter);

            return response;
        }
        catch (Exception ex)
        {
            return Result.Failure<QueryResponse<RegionDto>>(new Error(
                "Error",
                ex.Message, Shared.Enums.LogTypeEnum.Error));

        }
    }
}
