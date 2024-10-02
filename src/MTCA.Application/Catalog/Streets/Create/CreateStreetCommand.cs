using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using MTCA.Shared.DtoModels.Catalog.Street;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.Streets.Create;
public record CreateStreetCommand(
    string Name,
    decimal? Longitude,
    decimal? Latitude,
    Guid CityId,
    Guid? RegionId) : ICommand<CommandResponse<Guid>>;