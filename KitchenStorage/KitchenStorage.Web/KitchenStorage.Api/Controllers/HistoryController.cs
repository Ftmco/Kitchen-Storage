namespace KitchenStorage.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistoryController : ControllerBase
{
    private readonly IGetHistory _query;
    private readonly IHistoryViewModel _viewModel;


    public HistoryController(IGetHistory query,
        IHistoryViewModel viewModel)
    {
        _query = query;
        _viewModel = viewModel;
    }

    [HttpGet("FoodHistories")]
    public async Task<IActionResult> GetFoodHistoryAsync(int page, int count, string? q)
    {
        var histories = await _query.FoodHistoriesAsync(page, count, q);
        return Ok(Success("", "", new
        {
            histories.PageCount,
            Histories = await _viewModel.CreateFoodHistoryViewModel(histories.Result),
        }));
    }

    [HttpGet("InventoryHistory")]
    public async Task<IActionResult> GetInventoryHistoryAsync(int page, int count, string? q)
    {
        var histories = await _query.InventoryHistoriesAsync(page, count, q);
        return Ok(Success("", "", new
        {
            histories.PageCount,
            Histories = await _viewModel.CreateInventoryHisstoryViewModelAsync(histories.Result),
        }));
    }
}
