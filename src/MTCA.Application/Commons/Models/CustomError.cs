using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Models;
public class CustomError
{
    public string? Type { get; set; }
    public string? Title { get; set; }
    public int? Status { get; set; }
    public string? Detail { get; set; }
    public Error[] Errors { get; set; }
    public CustomError(string? type, string? title, int? status, string? detail, Error[] errors)
    {
        Type = type;
        Title = title;
        Status = status;
        Detail = detail;
        Errors = errors;
    }

}
