using FluentValidation;
using MTCA.Application.Identity.Roles.Specifications;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Roles.Create;
internal class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    private readonly IRepository<Role> _roleRepository;

    public CreateRoleCommandValidator(IRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;

        RuleFor(x => x.Name)
           .NotEmpty()
           .NotNull();

        RuleFor(x => x.Name)
            .MustAsync(async (name, ct) => await _roleRepository.FirstOrDefaultAsync(new RoleByNameSpec(name)) is null)
                .WithMessage(Domain.Errors.DomainErrors.RoleError.RoleNameAlreadyExists.Message);
    }
}
