using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTCA.API.Abstractions;
using MTCA.API.Contracts.Catalog.Region;
using MTCA.API.Contracts.Catalog.Street;
using MTCA.Application.Catalog.Regions.Create;
using MTCA.Application.Catalog.Regions.Delete;
using MTCA.Application.Catalog.Regions.GetAll;
using MTCA.Application.Catalog.Regions.GetById;
using MTCA.Application.Catalog.Regions.Update;
using MTCA.Application.Catalog.Streets.Update;
using MTCA.Application.Commons.Models;
using MTCA.Domain.Authorization;
using MTCA.Infrastructure.Authentication;

namespace MTCA.API.Controllers.Catalog;

/// <summary>
/// RegionController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RegionController : ApiController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    public RegionController(ISender sender) : base(sender)
    {

    }
    //[HasPermission(ActionCatalog.Search, ResourceCatalog.Regions)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] SearchRegionRequest request)
    {
        var query = new GetAllRegionQuery(request.Name,request.ParentId,request.Type,request.CustomRegionName,request.Page,request.PageSize);
        var response = await Sender.Send(query);
        return response.IsSuccess ? Ok(response.Value) : HandleFailure<QueryResponse<string>>(response);
    }

    //[HasPermission(ActionCatalog.View, ResourceCatalog.Regions)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetByIdRegionQuery(id);
        var response = await Sender.Send(query);
        return response.IsSuccess ? Ok(response.Value) : HandleFailure<QueryResponse<string>>(response);

    }

    //[HasPermission(ActionCatalog.Create, ResourceCatalog.Regions)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateRegionRequest request)
    {
        var command = new CreateRegionCommand(
            request.Name,
            request.ParentId,
            request.Type,
            request.CustomRegionName,
            request.Longitude,
            request.Latitude);

        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }

    /// <summary>
    /// Delete existing object by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Guid of deleted model or failure result.</returns>

    //[HasPermission(ActionCatalog.Delete, ResourceCatalog.Regions)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteRegionCommand(id);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }

    //[HasPermission(ActionCatalog.Update, ResourceCatalog.Regions)]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateRegionRequest request)
    {
        var command = new UpdateRegionCommand(request.Id, request.Name, request.CustomRegionName, request.Longitude, request.Latitude);
        var response = await Sender.Send(command);
        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);

    }

}
