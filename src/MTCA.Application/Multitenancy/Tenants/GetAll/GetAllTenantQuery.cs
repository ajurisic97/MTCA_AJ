using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using MTCA.Shared.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Multitenancy.Tenants.GetAll;
public record GetAllTenantQuery() : IQuery<QueryResponse<TenantDto>>;