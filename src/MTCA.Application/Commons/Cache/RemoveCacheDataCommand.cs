using MTCA.Application.Commons.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Cache;
public record RemoveCacheDataCommand() : ICommand<bool>;