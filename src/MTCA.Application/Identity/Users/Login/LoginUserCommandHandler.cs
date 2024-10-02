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

namespace MTCA.Application.Identity.Users.Login;
internal class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, CommandResponse<TokenResponse>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IRepository<User> _userRepository;
    private IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;


    public LoginUserCommandHandler(IJwtProvider jwtProvider, IPasswordHasher passwordHasher, IRepository<User> userRepository, IUnitOfWork unitOfWork)
    {
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CommandResponse<TokenResponse>>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository.SingleOrDefaultAsync(new UserByUsernameWithPersonSpec(request.Username), cancellationToken);
            if(user == null)
            {
                return Result.Failure<CommandResponse<TokenResponse>>(Domain.Errors.DomainErrors.UserError.UserNotFound);

            }
            var verified = _passwordHasher.Verify(user.Password, request.Password);
            if (!verified)
            {
                return Result.Failure<CommandResponse<TokenResponse>>(
                Domain.Errors.DomainErrors.UserError.InvalidCredentials);
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
