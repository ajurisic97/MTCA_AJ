using FluentValidation;
using MTCA.Application.Commons.Errors;
using MTCA.Application.Identity.Users.Specifications;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Users.Create;
internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IRepository<User> _userRepository;

    public CreateUserCommandValidator(IRepository<User> userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.Username)
            .NotEmpty();

        RuleFor(x => x.Username)
            .MustAsync(async (username, ct) => await _userRepository.SingleOrDefaultAsync(new UserByUsernameSpec(username), ct) is null)
                .WithMessage(ApplicationErrors.User.UserAlreadyExist.Message);
    }
}
