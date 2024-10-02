using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTCA.API.Abstractions;
using MTCA.API.Contracts.Identity.RolePermission;
using MTCA.Application.Commons.Models;
using MTCA.Application.Identity.RolePermissions.Create;
using MTCA.Application.Identity.RolePermissions.Delete;
using MTCA.Domain.Authorization;
using MTCA.Infrastructure.Authentication;

namespace MTCA.API.Controllers.Identity;
/// <summary>
/// RolePermissionController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RolePermissionController : ApiController
{

    /// <summary>
    /// RolePermissionController constructor
    /// </summary>
    /// <param name="sender"></param>
    public RolePermissionController(ISender sender)
        : base(sender)
    {
    }

    /// <summary>
    /// Create connection between role and permission.
    /// </summary>
    /// <param name="request">
    /// - roleId
    /// - permissionId
    /// </param>
    /// <returns></returns>

    [HasPermission(ActionCatalog.Create, ResourceCatalog.RolePermissions)]
    [HttpPost]
    public async Task<IActionResult> Create(RolePermissionRequest request)
    {
        var command = new CreateRolePermissionCommand(request.RoleId, request.PermissionIds);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }

    /// <summary>
    /// Delete relation between role and permission.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HasPermission(ActionCatalog.Delete, ResourceCatalog.RolePermissions)]
    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteRolePermissionRequest request)
    {
        var command = new DeleteRolePermissionCommand(request.RoleId, request.PermissionIds);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }
}
