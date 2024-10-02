using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTCA.API.Abstractions;
using MTCA.API.Contracts.Tenant;
using MTCA.Application.Commons.Models;
using MTCA.Application.Multitenancy.Tenants.Activate;
using MTCA.Application.Multitenancy.Tenants.Deactivate;
using MTCA.Application.Multitenancy.Tenants.GetAll;
using MTCA.Application.Multitenancy.Tenants.ValidateApiKey;
using MTCA.Domain.Authorization;
using MTCA.Infrastructure.Authentication;

namespace MTCA.API.Controllers.Multitenancy;
[Route("api/[controller]")]
[ApiController]
public class TenantController : ApiController
{
    public TenantController(ISender sender) : base(sender)
    {

    }

    [HasPermission(ActionCatalog.Search, ResourceCatalog.Tenants)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var command = new GetAllTenantQuery();
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<QueryResponse<string>>(response);

    }
    [HttpGet("validateapikey")]
    public async Task<IActionResult> ValidateApiKey([FromQuery] ValidateApiKeyRequest request)
    {
        var command = new ValidateApiKeyQuery(request.ApiKey);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<QueryResponse<string>>(response);

    }

    [HasPermission(ActionCatalog.Update, ResourceCatalog.Tenants)]
    [HttpPut("activate")]
    public async Task<IActionResult> Activate([FromBody] ValidateApiKeyRequest request)
    {
        var command = new ActivateTenantCommand(request.ApiKey);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<QueryResponse<string>>(response);

    }

    [HasPermission(ActionCatalog.Update, ResourceCatalog.Tenants)]
    [HttpPut("deactivate")]
    public async Task<IActionResult> Deactivate([FromBody] ValidateApiKeyRequest request)
    {
        var command = new DeactivateTenantCommand(request.ApiKey);
        var response = await Sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure<QueryResponse<string>>(response);

    }
}
