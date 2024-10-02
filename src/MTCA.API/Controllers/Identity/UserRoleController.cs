using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTCA.API.Abstractions;
using MTCA.API.Contracts.Identity.UserRole;
using MTCA.Application.Commons.Models;
using MTCA.Application.Identity.UserRoles.Create;
using MTCA.Application.Identity.UserRoles.Delete;
using MTCA.Domain.Authorization;
using MTCA.Infrastructure.Authentication;

namespace MTCA.API.Controllers.Identity;
/// <summary>
/// UserRoleController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UserRoleController : ApiController
{

    /// <summary>
    /// UserRoleController constructor
    /// </summary>
    /// <param name="sender"></param>
    public UserRoleController(ISender sender)
        : base(sender)
    {
    }

    /// <summary>
    /// Create connection between user and role.
    /// </summary>
    /// <param name="request">
    /// - userId
    /// - roleId
    /// </param>
    /// <returns></returns>

    [HasPermission(ActionCatalog.Create, ResourceCatalog.UserRoles)]
    [HttpPost]
    public async Task<IActionResult> Create(UserRoleRequest request)
    {
        var command = new CreateUserRoleCommand(request.UserId, request.RoleId);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }

    /// <summary>
    /// Delete connection between user and role.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>

    [HasPermission(ActionCatalog.Delete, ResourceCatalog.UserRoles)]
    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteUseRoleRequest request)
    {
        var command = new DeleteUserRoleCommand(request.UserId, request.RoleId);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);

    }
}
