using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Users.Create;
public sealed record CreateUserCommand(
    string Username,
    string Password,
    Guid? PersonId) : ICommand<CommandResponse<Guid>>;
