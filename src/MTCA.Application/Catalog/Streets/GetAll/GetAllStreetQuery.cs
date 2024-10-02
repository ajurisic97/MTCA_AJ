using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using MTCA.Shared.DtoModels.Catalog.Street;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.Streets.GetAll;
public record GetAllStreetQuery() : IQuery<QueryResponse<StreetDto>>;