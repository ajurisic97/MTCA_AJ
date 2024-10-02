using MediatR;
using Microsoft.Extensions.Logging;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Errors;
using MTCA.Application.Commons.Interfaces;
using MTCA.Application.Commons.Models;
using MTCA.Application.Identity.Users.Create;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Users.Update;
internal class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, CommandResponse<Guid>>
{
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateUserCommandHandler(IRepository<User> userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<CommandResponse<Guid>>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                return Result.Failure<CommandResponse<Guid>>(ApplicationErrors.User.UserNotFound);
            }
            if(request.Password != null)
            {
                var passwordHash = _passwordHasher.Hash(request.Password);
                user.UpdatePassword(passwordHash);
            }
            if(user.Username != request.UserName)
            {
                user.UpdateUsername(request.UserName);
            }
            await _unitOfWork.SaveChangesAsync();
            var response = new CommandResponse<Guid>(new List<Guid>() { user.Id });
            return response;
        }
        catch (Exception ex)
        {
            return Result.Failure<CommandResponse<Guid>>(new Error(
                    "Error",
                    ex.Message, Shared.Enums.LogTypeEnum.Error));
        }

    }
}
