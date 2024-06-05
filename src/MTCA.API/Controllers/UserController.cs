using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTCA.API.Abstractions;
using MTCA.Application.Commons.Models;
using MTCA.Domain.Authorization;
using MTCA.Infrastructure.Authentication;

namespace MTCA.API.Controllers;
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



    //[HasPermission(ActionCatalog.Search, ResourceCatalog.Users)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok();

    }
}
