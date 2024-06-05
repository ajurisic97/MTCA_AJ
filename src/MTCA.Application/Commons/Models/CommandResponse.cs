using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Models;
public class CommandResponse<T>
{
    public bool IsSuccessfulStatusCode => Data.Any();
    public List<T> Data { get; }
    public CustomError? ApiError { get; }

    public CommandResponse(List<T> data)
    {
        Data = data;
    }

    public CommandResponse(CustomError customError)
    {
        ApiError = customError;
        Data = new List<T>();
    }
}
