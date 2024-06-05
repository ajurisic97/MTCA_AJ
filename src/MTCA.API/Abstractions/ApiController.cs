using MediatR;
using Microsoft.AspNetCore.Mvc;
using MTCA.Application.Commons.Models;
using MTCA.Shared.Errors;

namespace MTCA.API.Abstractions;

/// <summary>
/// ApiController
/// </summary>
[ApiController]
public class ApiController : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    protected readonly ISender Sender;
    /// <summary>
    /// ApiController constructor
    /// </summary>
    /// <param name="sender"></param>
    protected ApiController(ISender sender) => Sender = sender;

    /// <summary>
    /// HandleFailure
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected IActionResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                BadRequest(
                    CreateProblemDetails(
                        "Validation Error", StatusCodes.Status400BadRequest,
                        result.Error,
                        validationResult.Errors)),
            _ =>
                BadRequest(
                    CreateProblemDetails(
                        "Bad Request",
                        StatusCodes.Status400BadRequest,
                        result.Error))
        };
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected IActionResult HandleFailure<T>(Result result)
    {

        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }
        else if (result is IValidationResult validationResult)
        {
            var validationError = CreateProblemDetails("Validation Error", StatusCodes.Status400BadRequest, result.Error, validationResult.Errors);
            var customError = new CustomError(
                validationError.Type,
                validationError.Title,
                validationError.Status,
                validationError.Detail,
                validationResult.Errors);

            if (typeof(T) == typeof(QueryResponse<string>))
            {
                var responseQueryError = new QueryResponse<string>(customError);
                return BadRequest(responseQueryError);
            }
            else
            {
                var responseCommandError = new CommandResponse<string>(customError);
                return BadRequest(responseCommandError);
            }
        }
        else
        {
            var errorArray = new Error[] { result.Error };
            var customError = new CustomError(
                result.Error.Code,
                "Bad Request",
                StatusCodes.Status400BadRequest,
                result.Error.Message,
                errorArray);

            if (typeof(T) == typeof(QueryResponse<string>))
            {

                var responseQueryError = new QueryResponse<string>(customError);
                return BadRequest(responseQueryError);
            }
            else
            {
                var responseCommandError = new CommandResponse<string>(customError);
                return BadRequest(responseCommandError);
            }
        }
    }

    private static ProblemDetails CreateProblemDetails(
    string title,
    int status,
    Error error,
    Error[]? errors = null) =>
    new()
    {
        Title = title,
        Type = error.Code,
        Detail = error.Message,
        Status = status,
        Extensions = { { nameof(errors), errors } }
    };
}
