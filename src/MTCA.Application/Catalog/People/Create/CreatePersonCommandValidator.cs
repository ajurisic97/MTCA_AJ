using FluentValidation;
using MTCA.Application.Catalog.People.Specifications;
using MTCA.Domain.Models;
using MTCA.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.People.Create;
internal class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    private readonly IRepository<Person> _personRepository;

    public CreatePersonCommandValidator(IRepository<Person> personRepository)
    {
        _personRepository = personRepository;


        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Person.FirstNameMaxLength);

        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Person.LastNameMaxLength);

    }
}
