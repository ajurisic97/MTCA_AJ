using Microsoft.Extensions.Logging;
using MTCA.Application.Commons.Abstractions;
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

namespace MTCA.Application.Identity.Users.RefreshToken;
internal class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, CommandResponse<TokenResponse>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IRepository<User> _userRepository;
    private IUnitOfWork _unitOfWork;

    public RefreshTokenCommandHandler(IJwtProvider jwtProvider, IRepository<User> userRepository, IUnitOfWork unitOfWork)
    {
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<CommandResponse<TokenResponse>>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = _jwtProvider.GetUserIdFromToken(request.Token);
            if(userId == null)
            {
                return Result.Failure<CommandResponse<TokenResponse>>(Domain.Errors.DomainErrors.UserError.UserNotFound);

            }
            var user = await _userRepository.SingleOrDefaultAsync(new UserByIdWihtPerson((Guid)userId), cancellationToken);
            if (user == null)
            {
                return Result.Failure<CommandResponse<TokenResponse>>(Domain.Errors.DomainErrors.UserError.UserNotFound);

            }
            if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return Result.Failure<CommandResponse<TokenResponse>>(Domain.Errors.DomainErrors.UserError.RefreshToken);
                
            }
            var token = await _jwtProvider.GenerateTokenAndUpdateUserAsync(user);
            var response = new CommandResponse<TokenResponse>([token]);
            await _unitOfWork.SaveChangesAsync();
            return response;
        }
        catch (Exception ex)
        {
            return Result.Failure<CommandResponse<TokenResponse>>(new Error(
                    "Error",
                    ex.Message, Shared.Enums.LogTypeEnum.Error));
        }

    }
}
