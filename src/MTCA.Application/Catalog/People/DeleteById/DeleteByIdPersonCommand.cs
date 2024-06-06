﻿using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.People.DeleteById;
public record DeleteByIdPersonCommand(Guid Id) : ICommand<CommandResponse<Guid>>;
