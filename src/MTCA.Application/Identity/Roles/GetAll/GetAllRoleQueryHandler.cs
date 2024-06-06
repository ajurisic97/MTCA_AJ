using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Cache;
using MTCA.Application.Commons.Errors;
using MTCA.Application.Commons.Models;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCA.Shared.DtoModels.Identity;

namespace MTCA.Application.Identity.Roles.GetAll;
internal class GetAllRoleQueryHandler : IQueryHandler<GetAllRoleQuery, QueryResponse<RoleDto>>
{
    private readonly IRepository<Role> _roleRepository;
    private IMapper _mapper;

    public GetAllRoleQueryHandler(IRepository<Role> roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<Result<QueryResponse<RoleDto>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _roleRepository.ListAsync(cancellationToken);

            if (data.Count == 0)
            {
                return Result.Failure<QueryResponse<RoleDto>>(ApplicationErrors.CommonError.NoData);
            }

            var resultDto = _mapper.Map<List<Role>, List<RoleDto>>(data);
            var result = new QueryResponse<RoleDto>(resultDto, 1, resultDto.Count, resultDto.Count);

            return result;
        }
        catch (Exception ex)
        {
            return Result.Failure<QueryResponse<RoleDto>>(new Error(
                    "Error",
                    ex.Message, Shared.Enums.LogTypeEnum.Error));
        }


    }
}
