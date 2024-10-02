using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTCA.API.Abstractions;
using MTCA.API.Contracts.Identity.Role;
using MTCA.Application.Commons.Models;
using MTCA.Application.Identity.Roles.Create;
using MTCA.Application.Identity.Roles.Delete;
using MTCA.Application.Identity.Roles.GetAll;
using MTCA.Application.Identity.Roles.GetById;
using MTCA.Domain.Authorization;
using MTCA.Infrastructure.Authentication;

namespace MTCA.API.Controllers.Identity;
[Route("api/[controller]")]
[ApiController]
public class RoleController : ApiController
{

    /// <summary>
    /// RoleController constructor
    /// </summary>
    /// <param name="sender"></param>
    public RoleController(ISender sender)
        : base(sender)
    {
    }

    /// <summary>
    /// Create new role.
    /// </summary>
    /// <param name="request">
    /// - name
    /// - description [null]
    /// </param>
    /// <returns>Guid of created model or failure result.</returns>

    [HasPermission(ActionCatalog.Create, ResourceCatalog.Roles)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleRequest request)
    {
        var command = new CreateRoleCommand(request.Name, request.Description);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }

    /// <summary>
    /// Catalog get all - get all roles without any relations to them.
    /// </summary>
    /// <returns>List of roles or failure result if there is no data.</returns>

    [HasPermission(ActionCatalog.Search, ResourceCatalog.Roles)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var command = new GetAllRoleQuery();
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<QueryResponse<string>>(response);
    }

    /// <summary>
    /// Catalog get by id - get role by id with permissions related to that role.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>One role or failure result.</returns>

    [HasPermission(ActionCatalog.View, ResourceCatalog.Roles)]
    [Route("{id}")]
    [HttpGet]
    public async Task<IActionResult> GetById(Guid id)
    {
        var command = new GetByIdRoleQuery(id);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<QueryResponse<string>>(response);
    }

    /// <summary>
    /// Delete role by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HasPermission(ActionCatalog.Delete, ResourceCatalog.Roles)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteRoleCommand(id);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }
}

