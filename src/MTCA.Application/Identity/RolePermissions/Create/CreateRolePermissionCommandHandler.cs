using MediatR;
using Microsoft.Extensions.Logging;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Errors;
using MTCA.Application.Commons.Models;
using MTCA.Application.Identity.Roles.Specifications;
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

namespace MTCA.Application.Identity.RolePermissions.Create;
internal class CreateRolePermissionCommandHandler : ICommandHandler<CreateRolePermissionCommand, CommandResponse<bool>>
{
    private readonly IRepository<RolePermission> _rolePermissionRepository;
    private readonly IRepository<Role> _roleRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public CreateRolePermissionCommandHandler(IUnitOfWork unitOfWork, IRepository<RolePermission> rolePermissionRepository, IRepository<Role> roleRepository, IPublisher publisher, IRepository<User> userRepository)
    {
        _unitOfWork = unitOfWork;
        _rolePermissionRepository = rolePermissionRepository;
        _roleRepository = roleRepository;
        _publisher = publisher;
        _userRepository = userRepository;
    }

    public async Task<Result<CommandResponse<bool>>> Handle(CreateRolePermissionCommand request, CancellationToken cancellationToken)
    {
        _unitOfWork.BeginTransaction();
        try
        {
            var data = await _roleRepository.SingleOrDefaultAsync(new RoleByIdWithPermissionSpec(request.RoleId), cancellationToken);

            if (data == null)
            {
                return Result.Failure<CommandResponse<bool>>(ApplicationErrors.Role.RoleNotFound);
            }
            var rolePermissionList = new List<RolePermission>();
            foreach (var permissionId in request.PermissionIds)
            {
                if(!data.Permissions.Any(x => x.Id == permissionId))
                {
                    var newRolePermission = RolePermission.Create(request.RoleId, permissionId);
                    rolePermissionList.Add(newRolePermission);
                }
            }

            var userByRole = await _userRepository.ListAsync(new UsersByRoleIdSpec(request.RoleId),cancellationToken);
            if (userByRole.Any())
            {
                var identityEvent = new IdentityChangeEvent
                {
                    UserIds = userByRole.Select(x => x.Id.ToString()).ToList(),
                };
                await _publisher.Publish(identityEvent, cancellationToken);
            }

            await _rolePermissionRepository.AddRangeAsync(rolePermissionList, cancellationToken);
            await _unitOfWork.SaveAndCommitAsync();
            var response = new CommandResponse<bool>([true]);
            return response;
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            return Result.Failure<CommandResponse<bool>>(new Error(
                    "Error",
                    ex.Message, Shared.Enums.LogTypeEnum.Error));
        }

    }
}
