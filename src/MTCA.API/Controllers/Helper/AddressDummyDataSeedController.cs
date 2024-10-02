using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MTCA.API.Controllers.Helper;
/// <summary>
/// AddressDummyDataSeedController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AddressDummyDataSeedController : ControllerBase
{
    //[HasPermission(ActionCatalog.Search, ResourceCatalog.AddressDummy)]
    [HttpPost]
    public async Task<IActionResult> SeedData()
    {

        //var command = new AddressDummyCommand();

        //var response = await Sender.Send(command);
        return Ok("a");


    }
}
