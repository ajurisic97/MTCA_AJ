using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTCA.API.Abstractions;
using MTCA.API.Contracts.Catalog.Person;
using MTCA.Application.Catalog.People.Create;
using MTCA.Application.Catalog.People.DeleteById;
using MTCA.Application.Catalog.People.GetAll;
using MTCA.Application.Catalog.People.Update;
using MTCA.Application.Commons.Models;
using MTCA.Domain.Authorization;
using MTCA.Infrastructure.Authentication;

namespace MTCA.API.Controllers.Catalog;
[Route("api/[controller]")]
[ApiController]
public class PersonController : ApiController
{

    /// <summary>
    /// PersonController constructor
    /// </summary>
    /// <param name="sender"></param>
    public PersonController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Create new person.
    /// </summary>
    /// <param name="request">
    /// - firstName
    /// - lastName
    /// </param>
    /// <returns>Guid of created model or failure result.</returns>

    [HasPermission(ActionCatalog.Create, ResourceCatalog.People)]
    [HttpPost]
    public async Task<IActionResult> Create(CreatePersonRequest request)
    {
        var command = new CreatePersonCommand(
            request.FirstName,
            request.LastName);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }


    /// <summary>
    /// Catalog get all people.
    /// </summary>
    /// <returns>List of people or failure result if there is no data.</returns>

    [HasPermission(ActionCatalog.Search, ResourceCatalog.People)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] SearchPersonRequest request)
    {
        var query = new GetAllPersonQuery(
            request.Id,
            request.FullName,
            request.Page,
            request.PageSize);
        var response = await Sender.Send(query);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<QueryResponse<string>>(response);

    }



    /// <summary>
    /// Update existing person.
    /// </summary>
    /// <param name="request">
    /// - id
    /// - firstName
    /// - lastName
    /// </param> 
    /// <returns>Guid of updated model or failure result.</returns>

    [HasPermission(ActionCatalog.Update, ResourceCatalog.People)]
    [HttpPut]
    public async Task<IActionResult> Update(UpdatePersonRequest request)
    {
        var command = new UpdatePersonCommand(
            request.Id,
            request.FirstName,
            request.LastName);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }

    /// <summary>
    /// Delete existing person by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Guid of deleted model or failure result.</returns>

    [HasPermission(ActionCatalog.Delete, ResourceCatalog.People)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteByIdPersonCommand(id);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<CommandResponse<string>>(response);
    }

}

