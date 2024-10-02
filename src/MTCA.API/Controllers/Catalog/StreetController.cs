using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTCA.API.Abstractions;
using MTCA.API.Contracts.Catalog.Street;
using MTCA.Application.Catalog.Streets.Create;
using MTCA.Application.Catalog.Streets.Delete;
using MTCA.Application.Catalog.Streets.GetAll;
using MTCA.Application.Catalog.Streets.GetById;
using MTCA.Application.Catalog.Streets.Update;
using MTCA.Application.Commons.Models;
using MTCA.Domain.Authorization;
using MTCA.Infrastructure.Authentication;

namespace MTCA.API.Controllers.Catalog;
[Route("api/[controller]")]
[ApiController]
public class StreetController : ApiController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    public StreetController(ISender sender) : base(sender)
    {

    }
    //[HasPermission(ActionCatalog.Search, ResourceCatalog.Streets)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] SearchStreetRequest request)
    {
        var query = new GetAllStreetQuery(request.Name,request.CityId,request.RegionId,request.Page,request.PageSize);
        var response = await Sender.Send(query);
        return response.IsSuccess ? Ok(response.Value) : HandleFailure<QueryResponse<string>>(response);
    }

    //[HasPermission(ActionCatalog.View, ResourceCatalog.Streets)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetByIdStreetQuery(id);
        var response = await Sender.Send(query);
        return response.IsSuccess ? Ok(response.Value) : HandleFailure<QueryResponse<string>>(response);

    }

    //[HasPermission(ActionCatalog.Create, ResourceCatalog.Streets)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateStreetRequest request)
    {
        var command = new CreateStreetCommand(request.Name,request.Longitude,request.Latitude,request.CityId,request.RegionId);

        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }

    /// <summary>
    /// Delete existing object by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Guid of deleted model or failure result.</returns>

    //[HasPermission(ActionCatalog.Delete, ResourceCatalog.Streets)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteStreetCommand(id);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }

    //[HasPermission(ActionCatalog.Update, ResourceCatalog.Streets)]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateStreetRequest request)
    {
        var command = new UpdateStreetCommand(request.Id,request.Name,request.RegionId,request.Longitude,request.Latitude);
        var response = await Sender.Send(command);
        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);

    }
}
