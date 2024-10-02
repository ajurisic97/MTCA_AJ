using AutoMapper;
using Microsoft.Extensions.Logging;
using MTCA.Application.Catalog.People.Specifications;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Errors;
using MTCA.Application.Commons.Models;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.Errors;
using MTCA.Shared.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCA.Shared.DtoModels.Catalog;

namespace MTCA.Application.Catalog.People.GetAll;

internal class GetAllPersonQueryHandler : IQueryHandler<GetAllPersonQuery, QueryResponse<PersonDto>>
{
    private readonly IRepository<Person> _personRepository;
    private IMapper _mapper;

    public GetAllPersonQueryHandler(IRepository<Person> personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<Result<QueryResponse<PersonDto>>> Handle(GetAllPersonQuery request, CancellationToken cancellationToken)
    {
        try
        {

            var peopleCount = await _personRepository.CountAsync(new PeopleBaseSpec(request.Id, request.FullName, request.Page, request.PageSize, true), cancellationToken);
            if (peopleCount == 0)
            {
                return Result.Failure<QueryResponse<PersonDto>>(ApplicationErrors.CommonError.NoData);
            }
            var people = await _personRepository.ListAsync(new PeopleBaseSpec(request.Id, request.FullName, request.Page, request.PageSize, false), cancellationToken);

            var resultDto = _mapper.Map<List<Person>, List<PersonDto>>(people);
            var result = new QueryResponse<PersonDto>(resultDto, request.Page, request.PageSize, peopleCount);

            return result;
        }
        catch (Exception ex)
        {
            return Result.Failure<QueryResponse<PersonDto>>(new Error(
                    "Error",
                    ex.Message, Shared.Enums.LogTypeEnum.Error));
        }
    }
}