namespace KitchenStorage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IGetInventory _query;
        private readonly IInventoryAction _action;
        private readonly IInventoryViewModel _viewModel;

        public InventoryController
            (IInventoryAction action,
            IInventoryViewModel viewModel,
            IGetInventory query)
        {
            _action = action;
            _viewModel = viewModel;
            _query = query;
        }

        [HttpGet("Inventory")]
        public async Task<IActionResult> GetInventoryAsync(int page, int count)
        {
            var inventory = await _query.InventorysAsync(page, count);
            return Ok(Success("", "", new
            {
                inventory.PageCount,
                Inventory = await _viewModel.CreateInventoryViewModelAsync(inventory.Result),
            }));
        }

        [HttpGet("Preview")]
        public async Task<IActionResult> GetPreviewInventoriesAsync()
        {
            IEnumerable<Entities.Inventory> inventories = await _query.InventorysAsync();
            return Ok(Success("", "", new
            {
                Inventory = _viewModel.CreatePreviewInventiryViewModel(inventories)
            }));
        }

        [HttpGet("AlertLimit")]
        public async Task<IActionResult> GetAlertLimitsAsync(int page, int count)
        {
            var alertLimit = await _query.GetAlertLimitAsync(page, count);
            return Ok(Success("", "", new
            {
                alertLimit.PageCount,
                Inventory = await _viewModel.CreateInventoryViewModelAsync(alertLimit.Result),
            }));
        }


        [HttpPost("Upsert")]
        public async Task<IActionResult> UpsertAsync(UpsertInventoryViewModel upsert)
        {
            var upserInventory = await _action.UpsertAsync(upsert);
            return await upserInventory.MatchAsync(RightAsync: async (inventory) => Ok(Success("دارایی با موفقیت ثبت شد", "", new
            {
                Inventory = await _viewModel.CreateInventoryViewModelAsync(inventory),
            })),
                                Left: (status) => InventoryActionResult(status));
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteAsync(Guid id)
                => InventoryActionResult(await _action.DeleteAsync(id));

        [NonAction]
        OkObjectResult InventoryActionResult(InventoryActionStatus status) => status switch
        {
            InventoryActionStatus.Failed => Ok(ApiException()),
            InventoryActionStatus.NotFound => Ok(Failed(404, "دارایی مورد نظر یافت نشد", "")),
            InventoryActionStatus.Success => Ok(Success("عملیات مورد نظر با موفقیت انجام شد", "", new { })),
            _ => Ok(ApiException()),
        };
    }
}

