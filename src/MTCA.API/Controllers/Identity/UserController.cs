using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTCA.API.Abstractions;
using MTCA.API.Contracts.Identity.User;
using MTCA.Application.Commons.Models;
using MTCA.Application.Identity.Users.Create;
using MTCA.Application.Identity.Users.GetAll;
using MTCA.Application.Identity.Users.GetById;
using MTCA.Application.Identity.Users.Login;
using MTCA.Application.Identity.Users.RefreshToken;
using MTCA.Application.Identity.Users.Update;
using MTCA.Domain.Authorization;
using MTCA.Infrastructure.Authentication;
using MTCA.Shared.Multitenancy;
using System.ComponentModel.DataAnnotations;

namespace MTCA.API.Controllers.Identity;
/// <summary>
/// UserController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UserController : ApiController
{

    /// <summary>
    /// UserController constructor
    /// </summary>
    /// <param name="sender"></param>
    public UserController(ISender sender)
        : base(sender)
    {
    }


    /// <summary>
    /// Create new user.
    /// </summary>
    /// <param name="request">
    /// - username
    /// - password
    /// - personId [null] 
    /// </param>
    /// <returns>Guid of created model or failure result.</returns>

    [HasPermission(ActionCatalog.Create, ResourceCatalog.Users)]
    [HttpPost]
    public async Task<IActionResult> Create(RegisterUserRequest request)
    {
        var command = new CreateUserCommand(request.UserName, request.Password, request.PersonId);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }


    /// <summary>
    /// Catalog get all - get all users without any relations to them.
    /// </summary>
    /// <returns>List of users or failure result if there is no data.</returns>

    [HasPermission(ActionCatalog.Search, ResourceCatalog.Users)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] SearchUserRequest request)
    {
        var command = new GetAllUserQuery(request.PersonId, request.UserName, request.Page, request.PageSize);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<QueryResponse<string>>(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>

    [HasPermission(ActionCatalog.Update, ResourceCatalog.Users)]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        var command = new UpdateUserCommand(request.Id, request.UserName, request.Password);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }


    /// <summary>
    /// Catalog get by id - Get user by id without any relations with him.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>One user or failure result.</returns>

    [HasPermission(ActionCatalog.View, ResourceCatalog.Users)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var command = new GetByIdUserQuery(id);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<QueryResponse<string>>(response);
    }


    /// <summary>
    /// Token generator.
    /// </summary>
    /// <param name="request">
    /// <param name="header">
    /// - username
    /// - password
    /// </param>
    /// <returns>string token or failure result</returns>
    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> LoginUser([FromHeader(Name = MultitenancyConstants.TenantIdName)][Required] string header, [FromBody] LoginRequest request)
    {
        var command = new LoginUserCommand(request.Username, request.Password);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }

    [Route("refresh")]
    [HttpPost]
    public async Task<IActionResult> RefreshUserToken([FromBody] RefreshTokenRequest request)
    {
        var command = new RefreshTokenCommand(request.Token, request.RefreshToken);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }

}
