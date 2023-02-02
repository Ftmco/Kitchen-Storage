using Microsoft.AspNetCore.Mvc;

namespace KitchenStorage.Web.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController : ControllerBase
{
    [HttpPost("Upsert")]
    public async Task<IActionResult> UpsertAsync(UpsertGroupViewModel upsert)
    {
        return Ok();
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        return Ok();
    }
}
