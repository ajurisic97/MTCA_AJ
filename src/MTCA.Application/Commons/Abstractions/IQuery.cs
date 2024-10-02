using MediatR;
using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
