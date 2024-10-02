using AutoMapper;
using Microsoft.Extensions.Logging;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Errors;
using MTCA.Application.Commons.Models;
using MTCA.Domain.Authorization;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCA.Shared.DtoModels.Identity;

namespace MTCA.Application.Identity.Permissions.GetAll;
internal class GetAllPermissionQueryHandler : IQueryHandler<GetAllPermissionQuery, QueryResponse<PermissionDto>>
{
    private readonly IRepository<Permission> _permissionRepository;
    private IMapper _mapper;

    public GetAllPermissionQueryHandler(IRepository<Permission> permissionRepository, IMapper mapper)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }
    public async Task<Result<QueryResponse<PermissionDto>>> Handle(GetAllPermissionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _permissionRepository.ListAsync(cancellationToken);
            if (data.Count == 0)
            {
                return Result.Failure<QueryResponse<PermissionDto>>(ApplicationErrors.CommonError.NoData);
            }
            var resultDto = _mapper.Map<List<Permission>, List<PermissionDto>>(data);

            var result = new QueryResponse<PermissionDto>(resultDto, 1, resultDto.Count, resultDto.Count);
            return result;

        }
        catch (Exception ex)
        {
            return Result.Failure<QueryResponse<PermissionDto>>(new Error(
                    "Error",
                    ex.Message, Shared.Enums.LogTypeEnum.Error));
        }


    }
}
