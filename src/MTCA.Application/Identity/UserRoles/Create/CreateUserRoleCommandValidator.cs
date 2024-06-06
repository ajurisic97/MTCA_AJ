using FluentValidation;
using MTCA.Application.Commons.Behaviors;
using MTCA.Application.Commons.Errors;
using MTCA.Application.Identity.Roles.Create;
using MTCA.Application.Identity.UserRoles.Specifications;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.UserRoles.Create;
internal class CreateUserRoleCommandValidator : AbstractValidator<CreateUserRoleCommand>
{
    private readonly IRepository<Role> _roleRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserRole> _userRoleRepository;


    public CreateUserRoleCommandValidator(IRepository<User> userRepository, IRepository<UserRole> userRoleRepository, IRepository<Role> roleRepository)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _roleRepository = roleRepository;

        var validationHelper = new ValidationHelper();

        RuleFor(x => x.RoleId)
            .MustAsync(async (id, ct) => await validationHelper.ValidForeignKey(id, _roleRepository))
                .WithMessage(ApplicationErrors.Role.RoleNotFound.Message);

        RuleFor(x => x.UserId)
        .MustAsync(async (id, ct) => await validationHelper.ValidForeignKey(id, _userRepository))
            .WithMessage(ApplicationErrors.User.UserNotFound.Message);

        RuleFor(x => new { x.RoleId, x.UserId })
        .MustAsync(async (parameters, ct) => await _userRoleRepository.FirstOrDefaultAsync(new UserRoleUniqueSpec(parameters.UserId,parameters.RoleId)) is null)
            .WithMessage(Domain.Errors.DomainErrors.UserRoleError.UserRoleAlreadyExists.Message);
    }
}