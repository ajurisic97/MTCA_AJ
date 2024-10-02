using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using MTCA.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.Regions.Create;
public record CreateRegionCommand(
    string Name,
    Guid? ParentId,
    RegionTypeEnum Type,
    string CustomRegionName,
    decimal? Longitude,
    decimal? Latitude) : ICommand<CommandResponse<Guid>>;
