using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Models;
public class QueryResponse<T>
{
    public bool IsSuccessfulStatusCode => Data.Any();
    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;
    public PermissionModel AvailableActions { get; set; } = new();
    public List<T> Data { get; }
    public CustomError? ApiError { get; }


    public QueryResponse(List<T> data, int page = 1, int pageSize = 1, int totalCount = 1)
    {
        Data= data;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    public QueryResponse(CustomError customError)
    {
        ApiError = customError;
        Data = new List<T>();
    }

}


