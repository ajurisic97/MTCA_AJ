using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Roles.Create;
public sealed record CreateRoleCommand(
    string Name,
    string? Description):ICommand<CommandResponse<Guid>>;