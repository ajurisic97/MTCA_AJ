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
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCA.Shared.DtoModels.Identity;

namespace MTCA.Application.Identity.Users.GetById;
internal class GetByIdUserQueryHandler : IQueryHandler<GetByIdUserQuery, QueryResponse<UserDto>>
{
    private readonly IRepository<User> _userRepository;
    private IMapper _mapper;

    public GetByIdUserQueryHandler(IRepository<User> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<QueryResponse<UserDto>>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var dbUser = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (dbUser == null)
            {
                return Result.Failure<QueryResponse<UserDto>>(ApplicationErrors.User.UserNotFound);
            }
            var resultDto = _mapper.Map<List<User>, List<UserDto>>([dbUser]);

            var response = new QueryResponse<UserDto>(resultDto, 1, 1, 1);

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
