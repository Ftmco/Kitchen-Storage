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
        IEnumerable<Entities.Day> days = await _get.DaysAsync();
        return Ok(Success("", "", new
        {
            Days = _viewModel.CreateDayViewModel(days)
        }));
    }

    [HttpPost("AddDay")]
    public async Task<IActionResult> AddDayAsync(AddDayViewModel addDay)
    {
        var create = await _action.CreateAsync(addDay);

        return create.Match(Right: (day) => Ok(Success("روز با موفقیت ثبت شد", "", new
        {
            Day = _viewModel.CreateDayViewModel(day)
        })),
        Left: (status) => DayActionResult(status));
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteDayAsync(Guid id)
            => DayActionResult(await _action.DeleteAsync(id));

    [NonAction]
    OkObjectResult DayActionResult(DayActionStatus status) => status switch
    {
        DayActionStatus.Success => Ok(Success("روز با موفقیت حذف شد", "", new { })),
        DayActionStatus.Failed => Ok(ApiException()),
        DayActionStatus.NotFound => Ok(Failed(404, "روز مورد نظر یافت نشد", "")),
        DayActionStatus.Exist => Ok(Failed(400, "روز مورد نظر از قبل وجود دارد", "")),
        _ => Ok(ApiException()),
    };
}
