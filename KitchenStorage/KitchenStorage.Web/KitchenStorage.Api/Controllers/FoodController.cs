namespace KitchenStorage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodAction _action;
        private readonly IGetFood _query;
        private readonly IGetNorm _normQuery;
        private readonly IFoodViewModel _viewModel;
        private readonly INormAction _normAction;
        private readonly INormViewModel _normViewModel;

        public FoodController
            (IFoodAction action,
            IGetFood query,
            IFoodViewModel viewModel,
            INormAction normAction,
            INormViewModel normViewModel, IGetNorm getNorm)
        {
            _normQuery = getNorm;
            _action = action;
            _query = query;
            _viewModel = viewModel;
            _normAction = normAction;
            _normViewModel = normViewModel;
        }

        [HttpGet("Foods")]
        public async Task<IActionResult> GetAsync(int page, int count)
        {
            var foods = await _query.FoodsAsync(page, count);
            return Ok(Success("", "", new { foods.PageCount, Foods = _viewModel.CreateFoodViewModel(foods.Result) }));
        }

        [HttpGet("Norms")]
        public async Task<IActionResult> GetNormsAsync(Guid foodId)
        {
            IEnumerable<Entities.Norm> norms = await _normQuery.NormsAsync(foodId);
            return Ok(Success("", "", new { Norms = await _normViewModel.CreateNormViewModelAsync(norms) }));
        }

        [HttpPost("AddNorm")]
        public async Task<IActionResult> AddNormAsync(AddNormViewModel norm)
        {
            var addNorm = await _normAction.CreateAsync(norm);
            return await addNorm.MatchAsync(RightAsync: async (norm) => Ok(Success("نرم  با موفقیت ثبت شد", "", new
            {
                Norm = await _normViewModel.CreateNormViewModelAsync(norm),
            })),
                                Left: (status) => FoodActionResult(status));
        }

        [HttpGet("RemoveNorm")]
        public async Task<IActionResult> RemoveNormAsync(Guid id)
                => FoodActionResult(await _normAction.DeleteAsync(id));

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

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteAsync(Guid id)
                => FoodActionResult(await _action.DeleteAsync(id));

        [NonAction]
        OkObjectResult FoodActionResult(FoodActionStatus status) => status switch
        {
            FoodActionStatus.Failed => Ok(ApiException()),
            FoodActionStatus.NotFound => Ok(Failed(404, "غذا مورد نظر یافت نشد", "")),
            FoodActionStatus.Success => Ok(Success("عملیات مورد نظر با موفقیت انجام شد", "", new { })),
            _ => Ok(ApiException()),
        };
    }
}
