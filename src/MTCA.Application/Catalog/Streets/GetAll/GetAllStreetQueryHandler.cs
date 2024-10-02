using AutoMapper;
using MTCA.Application.Catalog.Streets.GetAll;
using MTCA.Application.Catalog.Streets.Specifications;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Errors;
using MTCA.Application.Commons.Models;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.DtoModels.Catalog.Street;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.Streets.GetAll;
internal class GetAllStreetQueryHandler : IQueryHandler<GetAllStreetQuery, QueryResponse<StreetDto>>
{
    private readonly IRepository<Street> _streetRepository;
    private IMapper _mapper;


    public GetAllStreetQueryHandler(IRepository<Street> streetRepository, IMapper mapper)
    {
        _streetRepository = streetRepository;
        _mapper = mapper;
    }

    public async Task<Result<QueryResponse<StreetDto>>> Handle(GetAllStreetQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _streetRepository.ListAsync(new SearchStreetSpec(request.Name,request.CityId,request.RegionId, request.Page, request.PageSize, false), cancellationToken);

            if (!result.Any())
            {
                return Result.Failure<QueryResponse<StreetDto>>(ApplicationErrors.CommonError.NoData);

            }

            var counter = await _streetRepository.CountAsync(new SearchStreetSpec(request.Name, request.CityId, request.RegionId, request.Page, request.PageSize, true), cancellationToken);

            var resultDto = _mapper.Map<List<Street>, List<StreetDto>>(result);
            var response = new QueryResponse<StreetDto>(resultDto, request.Page, request.PageSize, counter);

            return response;
        }
        catch (Exception ex)
        {
            return Result.Failure<QueryResponse<StreetDto>>(new Error(
                "Error",
                ex.Message, Shared.Enums.LogTypeEnum.Error));

        }
    }
}
