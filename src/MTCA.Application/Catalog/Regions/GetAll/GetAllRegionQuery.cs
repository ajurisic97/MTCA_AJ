using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using MTCA.Shared.DtoModels.Catalog.Region;
using MTCA.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.Regions.GetAll;
public record GetAllRegionQuery(
    string? Name,
    Guid? ParentId,
    RegionTypeEnum? Type,
    string? CustomRegionName,
    int Page,
    int PageSize) : IQuery<QueryResponse<RegionDto>>;