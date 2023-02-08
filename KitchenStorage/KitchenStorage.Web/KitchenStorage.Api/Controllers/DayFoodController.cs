using KitchenStorage.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace KitchenStorage.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DayFoodController : ControllerBase
{
    private readonly IGetDayFood _get;

    private readonly IDayFoodAction _action;

    private readonly IDayFoodViewModel _viewModel;

    public DayFoodController(IGetDayFood get, IDayFoodAction action, IDayFoodViewModel viewModel)
    {
        _action = action;
        _get = get;
        _viewModel = viewModel;
    }

    [HttpGet("DaysFoods")]
    public async Task<IActionResult> GetDaysFoodsAsync(int page, int count)
    {
        var daysFoods = await _get.DayFoodsAsync(page, count);

        return Ok(Success("", "", new
        {
            daysFoods.PageCount,
            DayFoods = await _viewModel.CreateDayFoodViewModelAsync(daysFoods.Result)
        }));
    }

    [HttpPost("Upsert")]
    public async Task<IActionResult> UpsertFoodAsync(UpsertDayFoodViewModel upsert)
    {
        return Ok();
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteFoodAsync(Guid id)
    {
        return Ok();
    }

    [NonAction]
    OkObjectResult DayFoodActionResult(DayFoodActionStatus status) => status switch
    {
        DayFoodActionStatus.Success => throw new NotImplementedException(),
        DayFoodActionStatus.Failed => throw new NotImplementedException(),
        DayFoodActionStatus.NotFound => throw new NotImplementedException(),
        DayFoodActionStatus.LackOfInventory => throw new NotImplementedException(),
        _ => throw new NotImplementedException(),
    };
}
