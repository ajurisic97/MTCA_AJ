﻿using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using MTCA.Domain.Models;
using MTCA.Shared.DtoModels.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Roles.GetById;
public sealed record GetByIdRoleQuery(Guid RoleId) : IQuery<QueryResponse<RolePermissionDto>>;