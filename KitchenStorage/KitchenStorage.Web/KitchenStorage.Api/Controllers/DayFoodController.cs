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
    public async Task<IActionResult> GetDaysFoodsAsync(int page, int count, string? q)
    {
        var daysFoods = await _get.DayFoodsAsync(page, count, q);

        return Ok(Success("", "", new
        {
            daysFoods.PageCount,
            DayFoods = await _viewModel.CreateDayFoodViewModelAsync(daysFoods.Result)
        }));
    }

    [HttpPost("Upsert")]
    public async Task<IActionResult> UpsertFoodAsync(UpsertDayFoodViewModel upsert)
    {
        var dayFood = await _action.UpsertAsync(upsert);

        return await dayFood.MatchAsync(RightAsync: async (food) => Ok(Success("غذای روزانه با موفقیت ثبت شد", "", new
        {
            Food = await _viewModel.CreateDayFoodViewModelAsync(food)
        })),
        Left: (status) => DayFoodActionResult(status));
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> DeleteFoodAsync(Guid id)
            => DayFoodActionResult(await _action.DeleteAsync(id));

    [HttpPost("MakeMeal")]
    public async Task<IActionResult> MakeMealAsync(MakeMealViewModel makeMeal)
                => DayFoodActionResult(await _action.MakeMealAsync(makeMeal));

    [NonAction]
    OkObjectResult DayFoodActionResult(DayFoodActionStatus status) => status switch
    {
        DayFoodActionStatus.Success => Ok(Success("عملیات مورد نظر با موفقیت انجام شد", "", new { })),
        DayFoodActionStatus.Failed => Ok(ApiException()),
        DayFoodActionStatus.NotFound => Ok(Failed(404, "غذای مورد نظر یافت نشد", "")),
        DayFoodActionStatus.LackOfInventory => Ok(Failed(400, "موجودی انبار شما کافی نمی باشد", "")),
        _ => Ok(ApiException()),
    };
}
