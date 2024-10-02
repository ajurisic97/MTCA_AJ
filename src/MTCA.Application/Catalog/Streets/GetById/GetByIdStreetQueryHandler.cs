using AutoMapper;
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

namespace MTCA.Application.Catalog.Streets.GetById;
internal class GetByIdStreetQueryHandler : IQueryHandler<GetByIdStreetQuery, QueryResponse<StreetExtendedDto>>
{
    private readonly IRepository<Street> _streetRepository;
    private IMapper _mapper;


    public GetByIdStreetQueryHandler(IRepository<Street> streetRepository, IMapper mapper)
    {
        _streetRepository = streetRepository;
        _mapper = mapper;
    }

    public async Task<Result<QueryResponse<StreetExtendedDto>>> Handle(GetByIdStreetQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _streetRepository.SingleOrDefaultAsync(new GetByIdStreetSpec(request.Id), cancellationToken);

            if (result == null)
            {
                return Result.Failure<QueryResponse<StreetExtendedDto>>(ApplicationErrors.CommonError.NoData);

            }

            var resultDto = _mapper.Map<Street, StreetExtendedDto>(result);
            var response = new QueryResponse<StreetExtendedDto>(new List<StreetExtendedDto> { resultDto });

            return response;
        }
        catch (Exception ex)
        {
            return Result.Failure<QueryResponse<StreetExtendedDto>>(new Error(
                "Error",
                ex.Message, Shared.Enums.LogTypeEnum.Error));

        }
    }
}
