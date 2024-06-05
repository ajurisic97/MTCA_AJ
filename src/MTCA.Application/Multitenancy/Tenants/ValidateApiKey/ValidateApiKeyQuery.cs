using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using MTCA.Shared.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Multitenancy.Tenants.ValidateApiKey;
public sealed record ValidateApiKeyQuery(Guid ApiKey) : IQuery<QueryResponse<TenantValidationDto>>;