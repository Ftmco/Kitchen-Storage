using KitchenStorage.Services.Abstraction;
using KitchenStorage.Services.Abstraction.Base;
using Microsoft.AspNetCore.Mvc;

namespace KitchenStorage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodAction _action;
        private readonly IGetFood _query;
        private readonly IFoodViewModel _viewModel;

        public FoodController
            (IFoodAction action,
            IGetFood query,
            IFoodViewModel viewModel)
        {
            _action = action;
            _query = query;
            _viewModel = viewModel;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(Success("", "", await _query.FoodsAsync()));
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> UpsertAsync(UpsertFoodViewModel upsert)
        {
            var upsertFood = await _action.UpsertAsync(upsert);
            return upsertFood.Match(Right: (food) => Ok(Success("غذا با موفقیت ثبت شد", "", new
            {
                Food = _viewModel.CreateFoodViewModel(food),
            })),
                                Left: (status) => FoodActionResult(status));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var delete = await _action.DeleteAsync(id);
            return delete.Match(Right: (food) => Ok(Success("غذا با موفقیت حذف شد", "", new
            {
                Food = _viewModel.CreateFoodViewModel(food),
            })),
                                Left: (status) => FoodActionResult(status));
        }

        [NonAction]
        OkObjectResult FoodActionResult(FoodActionStatus status) => status switch
        {
            FoodActionStatus.Failed => Ok(ApiException()),
            FoodActionStatus.NotFound => Ok(Faild(404, "غذا مورد نظر یافت نشد", "")),
            _ => Ok(ApiException()),
        };
    }
}
