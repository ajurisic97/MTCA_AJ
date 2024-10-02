﻿using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MTCA.Application.Catalog.Streets.Update;
public record UpdateStreetCommand(
    Guid Id,
    string Name,
    Guid? RegionId,
    decimal? Longitude,
    decimal? Latitude) : ICommand<CommandResponse<Guid>>;