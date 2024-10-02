using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Users.Update;
public sealed record UpdateUserCommand(
    Guid Id,
    string UserName,
    string? Password) : ICommand<CommandResponse<Guid>>;