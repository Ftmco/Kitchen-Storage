using KitchenStorage.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace KitchenStorage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodHistoryController : ControllerBase
    {
        private readonly IGetFoodHistory _query;
        private readonly IFoodHistoryViewModel _viewModel;

        public FoodHistoryController(IGetFoodHistory query,
            IFoodHistoryViewModel viewModel)
        {
            _query = query;
            _viewModel = viewModel;
        }

        [HttpGet("FoodHistories")]
        public async Task<IActionResult> Get(int page, int count)
        {
            var history = await _query.FoodHistoriesAsync(page, count);
            return Ok(Success("", "", new
            {
                history.PageCount,
                History = await _viewModel.CreateFoodHistoryViewModel(history.Result),
            }));
        }
    }
}
