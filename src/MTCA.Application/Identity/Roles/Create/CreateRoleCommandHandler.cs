using Ardalis.Specification;
using MediatR;
using Microsoft.Extensions.Logging;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Roles.Create;
internal class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, CommandResponse<Guid>>
{
    private readonly IRepository<Role> _roleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private IPublisher _publisher;

    public CreateRoleCommandHandler(IUnitOfWork unitOfWork, IRepository<Role> roleRepository, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _publisher = publisher;
    }

    public async Task<Result<CommandResponse<Guid>>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newRole = Role.Create(request.Name, request.Description);

            await _roleRepository.AddAsync(newRole, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var roleCreatedEvent = new RoleCreatedEvent
            {
                Id = newRole.Id,
            };

            await _publisher.Publish(roleCreatedEvent, cancellationToken);
            var response = new CommandResponse<Guid>([newRole.Id]);

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
