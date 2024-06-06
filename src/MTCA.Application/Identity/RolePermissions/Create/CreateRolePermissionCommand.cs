﻿using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.RolePermissions.Create;
public sealed record CreateRolePermissionCommand(
    Guid RoleId,
    List<int> PermissionIds):ICommand<CommandResponse<bool>>;
