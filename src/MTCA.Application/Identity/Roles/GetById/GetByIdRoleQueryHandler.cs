using AutoMapper;
using Microsoft.Extensions.Logging;
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
using MTCA.Application.Identity.Roles.Specifications;
using MTCA.Shared.DtoModels.Identity;

namespace MTCA.Application.Identity.Roles.GetById;
internal class GetByIdRoleQueryHandler : IQueryHandler<GetByIdRoleQuery, QueryResponse<RolePermissionDto>>
{
    private readonly IRepository<Role> _roleRepository;
    private IMapper _mapper;

    public GetByIdRoleQueryHandler(IRepository<Role> roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<Result<QueryResponse<RolePermissionDto>>> Handle(GetByIdRoleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _roleRepository.GetByIdAsync(new RoleByIdWithPermissionSpec(request.RoleId), cancellationToken);
            if (data == null)
            {
                return Result.Failure<QueryResponse<RolePermissionDto>>(ApplicationErrors.Role.RoleNotFound);
            }

            var resultDto = _mapper.Map<List<Role>, List<RolePermissionDto>>([data]);
            var result = new QueryResponse<RolePermissionDto>(resultDto, 1, resultDto.Count, resultDto.Count);

            return result;
        }
        catch (Exception ex)
        {
            return Result.Failure<QueryResponse<RolePermissionDto>>(new Error(
                    "Error",
                    ex.Message, Shared.Enums.LogTypeEnum.Error));
        }

    }
}
