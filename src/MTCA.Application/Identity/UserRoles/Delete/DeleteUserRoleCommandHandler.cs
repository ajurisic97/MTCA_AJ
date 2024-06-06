using MediatR;
using Microsoft.Extensions.Logging;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Errors;
using MTCA.Application.Commons.Models;
using MTCA.Application.Identity.UserRoles.Create;
using MTCA.Application.Identity.UserRoles.Specifications;
using MTCA.Application.Identity.Users.Events;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.UserRoles.Delete;
internal class DeleteUserRoleCommandHandler : ICommandHandler<DeleteUserRoleCommand, CommandResponse<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<UserRole> _userRoleRepository;
    private readonly IPublisher _publisher;

    public DeleteUserRoleCommandHandler(IUnitOfWork unitOfWork, IRepository<UserRole> userRoleRepository, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _userRoleRepository = userRoleRepository;
        _publisher = publisher;
    }

    public async Task<Result<CommandResponse<Guid>>> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _userRoleRepository.SingleOrDefaultAsync(new UserRoleUniqueSpec(request.UserId, request.RoleId), cancellationToken);

            if (data == null)
            {
                return Result.Failure<CommandResponse<Guid>>(ApplicationErrors.UserRole.UserRoleNotFound);

            }

            await _userRoleRepository.DeleteAsync(data, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var identityEvent = new IdentityChangeEvent
            {
                UserIds = new List<string> { request.UserId.ToString() }
            };
            await _publisher.Publish(identityEvent, cancellationToken);

            var response = new CommandResponse<Guid>([request.UserId]);
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
