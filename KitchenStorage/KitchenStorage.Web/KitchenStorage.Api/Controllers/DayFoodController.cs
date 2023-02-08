using Microsoft.AspNetCore.Mvc;

namespace KitchenStorage.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DayFoodController : ControllerBase
{

    [HttpGet("DaysFoods")]
    public async Task<IActionResult> GetDaysFoodsAsync(int page, int count)
    {
        return Ok();
    }
}
