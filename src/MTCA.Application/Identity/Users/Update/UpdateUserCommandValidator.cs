using FluentValidation;
using MTCA.Application.Identity.Users.Specifications;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Users.Update;
internal class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IRepository<User> _userRepository;

    public UpdateUserCommandValidator(IRepository<User> userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.UserName)
            .NotEmpty();

        RuleFor(x => new { x.UserName, x.Id })
            .MustAsync(async (parameters, ct) =>
            {
                var user = await _userRepository.FirstOrDefaultAsync(new UserByUsernameSpec(parameters.UserName), ct);
                if (user != null)
                {
                    var same = user.Id == parameters.Id;
                    return same;
                }
                return true;

            })
            .WithMessage(Domain.Errors.DomainErrors.UserError.UserAlreadyExists.Message);
    }
}
