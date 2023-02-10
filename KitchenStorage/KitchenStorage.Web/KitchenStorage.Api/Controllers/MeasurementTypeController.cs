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

    [HttpGet("Conversions")]
    public async Task<IActionResult> GetConversionsAsync(Guid id)
    {
        IEnumerable<TypeConvert> conversions = await _query.ConversionsAsync(id);
        return Ok(Success("", "", new
        {
            Conversions = await _viewModel.CreateConvertViewModelAsync(conversions)
        }));
    }

    [HttpPost("AddConvert")]
    public async Task<IActionResult> AddConvertAsync(AddConvertViewModel addConvert)
    {
        var convert = await _action.AddConvertAsync(addConvert);
        return await convert.MatchAsync(RightAsync: async (conversion) => Ok(Success("تبدیل واحد با موفقیت افزوده شد", "", new
        {
            Conversion = await _viewModel.CreateConvertViewModelAsync(conversion)
        })),
         Left: (status) => MeasurementActionResult(status));
    }

    [HttpDelete("RemoveConveert")]
    public async Task<IActionResult> RemoveConvertAsync(Guid id)
            => MeasurementActionResult(await _action.DeleteConvertAsync(id));

    [NonAction]
    OkObjectResult MeasurementActionResult(MeasurementTypeActionStatus status) => status switch
    {
        MeasurementTypeActionStatus.Failed => Ok(ApiException()),
        MeasurementTypeActionStatus.NotFound => Ok(Failed(404, "نوع اندازگیری مورد نظر یافت نشد", "")),
        MeasurementTypeActionStatus.Success => Ok(Success("عملیات مورد نظر با موفقیت انجام شد", "", new { })),
        _ => Ok(ApiException()),
    };
}
