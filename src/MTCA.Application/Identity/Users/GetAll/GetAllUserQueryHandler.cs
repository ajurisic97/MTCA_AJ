using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Errors;
using MTCA.Application.Commons.Interfaces;
using MTCA.Application.Commons.Models;
using MTCA.Application.Identity.Users.Specifications;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCA.Shared.DtoModels.Identity;

namespace MTCA.Application.Identity.Users.GetAll;
internal class GetAllUserQueryHandler : IQueryHandler<GetAllUserQuery, QueryResponse<UserDto>>
{
    private readonly IRepository<User> _userRepository;
    private IMapper _mapper;

    public GetAllUserQueryHandler(IRepository<User> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<QueryResponse<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _userRepository.ListAsync(new UsersSearchSpec(request.PersonId, request.UserName,request.Page, request.PageSize, false), cancellationToken);

            if (data.Count == 0)
            {
                return Result.Failure<QueryResponse<UserDto>>(ApplicationErrors.CommonError.NoData);
            }

            var counter = await _userRepository.CountAsync(new UsersSearchSpec(request.PersonId, request.UserName, request.Page, request.PageSize, true), cancellationToken);

            var resultDto = _mapper.Map<List<User>, List<UserDto>>(data);

            var response = new QueryResponse<UserDto>(resultDto, request.Page, request.PageSize, counter);

            return response;

        }
        catch (Exception ex)
        {
            return Result.Failure<QueryResponse<UserDto>>(new Error(
                    "Error",
                    ex.Message, Shared.Enums.LogTypeEnum.Error));
        }

    }
}
