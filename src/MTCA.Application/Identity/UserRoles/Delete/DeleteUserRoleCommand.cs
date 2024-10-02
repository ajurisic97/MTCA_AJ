using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.UserRoles.Delete;
public sealed record DeleteUserRoleCommand(
    Guid UserId,
    Guid RoleId) : ICommand<CommandResponse<Guid>>;