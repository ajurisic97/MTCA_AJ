using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using MTCA.Domain.Models;
using MTCA.Shared.DtoModels;
using MTCA.Shared.DtoModels.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Catalog.People.GetAll;
public record GetAllPersonQuery(
    Guid? Id,
    string? FullName,
    int Page,
    int PageSize) : IQuery<QueryResponse<PersonDto>>;

