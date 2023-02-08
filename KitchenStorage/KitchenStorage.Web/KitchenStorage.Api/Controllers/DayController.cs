using KitchenStorage.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace KitchenStorage.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DayController : ControllerBase
{

    private readonly IDayAction _action;

    private readonly IGetDay _get;

    private readonly IDayViewModel _viewModel;

    public DayController(IDayAction action, IGetDay get, IDayViewModel viewModel)
    {
        _action = action;
        _get = get;
        _viewModel = viewModel;
    }

    [HttpGet("Days")]
    public async Task<IActionResult> GetDaysAsync()
    {
        return Ok();
    }

    [HttpPost("AddDay")]
    public async Task<IActionResult> AddDayAsync()
    {
        return Ok();
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteDayAsync(Guid id)
    {
        return Ok();
    }
}
