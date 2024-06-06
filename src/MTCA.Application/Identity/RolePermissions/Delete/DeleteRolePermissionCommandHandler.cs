using MediatR;
using Microsoft.Extensions.Logging;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using MTCA.Application.Identity.RolePermissions.Specifications;
using MTCA.Application.Identity.Users.Events;
using MTCA.Application.Identity.Users.Specifications;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.RolePermissions.Delete;


internal class DeleteRolePermissionCommandHandler : ICommandHandler<DeleteRolePermissionCommand, CommandResponse<bool>>
{
    private readonly IRepository<RolePermission> _rolePermissionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;
    private readonly IRepository<User> _userRepository;

    public DeleteRolePermissionCommandHandler(IRepository<RolePermission> rolePermissionRepository, IUnitOfWork unitOfWork, IPublisher publisher, IRepository<User> userRepository)
    {
        _rolePermissionRepository = rolePermissionRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
        _userRepository = userRepository;
    }

    public async Task<Result<CommandResponse<bool>>> Handle(DeleteRolePermissionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _rolePermissionRepository.ListAsync(new RolePermissionSpec(request.RoleId, request.PermissionIds), cancellationToken);
            if (data.Count == 0)
            {
                return Result.Failure<CommandResponse<bool>>(Commons.Errors.ApplicationErrors.PermissionRole.PermissionRoleNotFound);
            }

            await _rolePermissionRepository.DeleteRangeAsync(data, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var userByRole = await _userRepository.ListAsync(new UsersByRoleIdSpec(request.RoleId), cancellationToken);

            if (userByRole.Count != 0)
            {
                var identityEvent = new IdentityChangeEvent
                {
                    UserIds = userByRole.Select(x => x.Id.ToString()).ToList(),
                };
                await _publisher.Publish(identityEvent, cancellationToken);
            }

            var response = new CommandResponse<bool>([true]);
            return response;
        }
        catch (Exception ex)
        {
            return Result.Failure<CommandResponse<bool>>(new Error(
                    "Error",
                    ex.Message, Shared.Enums.LogTypeEnum.Error));
        }

    }
}