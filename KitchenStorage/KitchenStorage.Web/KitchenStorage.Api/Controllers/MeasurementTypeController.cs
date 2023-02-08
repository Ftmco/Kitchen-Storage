using KitchenStorage.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace KitchenStorage.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MeasurementTypeController : ControllerBase
{
    private readonly IMeasurementTypeAction _action;
    private readonly IGetMeasurementType _query;
    private readonly IMeasurementTypeViewModel _viewModel;

    public MeasurementTypeController
        (IMeasurementTypeAction action,
        IGetMeasurementType query,
        IMeasurementTypeViewModel viewModel)
    {
        _action = action;
        _query = query;
        _viewModel = viewModel;
    }

    [HttpGet("Types")]
    public async Task<IActionResult> GetTypesAsync(int page, int count)
    {
        var types = await _query.TypesAsync(page, count);
        return Ok(Success("", "", new
        {
            types.PageCount,
            Types = _viewModel.CreateMeasurementTypeViewModel(types.Result),
        }));
    }

    [HttpPost("Upsert")]
    public async Task<IActionResult> UpsertAsync(UpsertMeasurementTypeViewModel upsert)
    {
        var upsertMeasurement = await _action.UpsertAsync(upsert);
        return upsertMeasurement.Match(Right: (measurement) => Ok(Success("نوع اندازه گیری با موفقیت ثبت شد", "", new
        {
            Measurement = _viewModel.CreateMeasurementTypeViewModel(measurement),
        })),
                            Left: (status) => MeasurementActionResult(status));
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAsync(Guid id)
            => MeasurementActionResult(await _action.DeleteAsync(id));

    [NonAction]
    OkObjectResult MeasurementActionResult(MeasurementTypeActionStatus status) => status switch
    {
        MeasurementTypeActionStatus.Failed => Ok(ApiException()),
        MeasurementTypeActionStatus.NotFound => Ok(Failed(404, "نوع اندازگیری مورد نظر یافت نشد", "")),
        MeasurementTypeActionStatus.Success => Ok(Success("عملیات مورد نظر با موفقیت انجام شد", "", new { })),
        _ => Ok(ApiException()),
    };
}
