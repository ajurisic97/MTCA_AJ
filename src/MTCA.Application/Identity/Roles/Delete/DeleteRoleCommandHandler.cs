using Microsoft.Extensions.Logging;
using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Errors;
using MTCA.Application.Commons.Models;
using MTCA.Application.Identity.Roles.Specifications;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Roles.Delete;
internal class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand, CommandResponse<Guid>>
{
    private readonly IRepository<Role> _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoleCommandHandler(IRepository<Role> roleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CommandResponse<Guid>>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _roleRepository.SingleOrDefaultAsync(new RoleByIdPopulatedSpec(request.Id),cancellationToken);

            if (data == null)
            {
                return Result.Failure<CommandResponse<Guid>>(ApplicationErrors.Role.RoleNotFound);
            }

            if(data.Users.Count != 0 || data.Permissions.Count != 0)
            {
                return Result.Failure<CommandResponse<Guid>>(ApplicationErrors.Role.RoleCannotBeDeleted);
            }

            await _roleRepository.DeleteAsync(data);
            await _unitOfWork.SaveChangesAsync();
            var response = new CommandResponse<Guid>([data.Id]);

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
