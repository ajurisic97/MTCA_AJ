using Ardalis.Specification;
using MediatR;
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

namespace MTCA.Application.Identity.Users.Create;
internal class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CommandResponse<Guid>>
{
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;
    private readonly IPasswordHasher _passwordHasher;


    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IRepository<User> userRepository, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _publisher = publisher;
    }

    public async Task<Result<CommandResponse<Guid>>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var passwordHash = _passwordHasher.Hash(request.Password);
            var newUser = User.Create(request.Username, passwordHash, request.PersonId);
            await _userRepository.AddAsync(newUser, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var userCreatedEvent = new UserCreatedEvent
            {
                Id = newUser.Id,
            };

            await _publisher.Publish(userCreatedEvent, cancellationToken);
            var response = new CommandResponse<Guid>([newUser.Id]);
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
