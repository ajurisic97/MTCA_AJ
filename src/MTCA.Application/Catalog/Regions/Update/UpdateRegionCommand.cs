using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.Regions.Update;
public record UpdateRegionCommand(
    Guid Id,
    string Name,
    string CustomRegionName,
    decimal? Longitude,
    decimal? Latitude) : ICommand<CommandResponse<Guid>>;

