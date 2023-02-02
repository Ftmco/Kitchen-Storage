using KitchenStorage.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("Get")]

        public async Task<IActionResult> GetAsync()
            => Ok(await _query.InventorysAsync());


        [HttpPost("Upsert")]
        public async Task<IActionResult> UpsertAsync(UpsertInventoryViewModel upsert)
        {
            var upserInventory = await _action.UpsertAsync(upsert);
            return upserInventory.Match(Right: (inventory) => Ok(Success("دارایی با موفقیت ثبت شد", "", new
            {
                Group = _viewModel.CreateInventoryViewModel(inventory),
            })),
                                Left: (status) => InventoryActionResult(status));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var delete = await _action.DeleteAsync(id);
            return delete.Match(Right: (group) => Ok(Success("دارایی با موفقیت حذف شد", "", new
            {
                Group = _viewModel.CreateInventoryViewModel(group),
            })),
                                Left: (status) => InventoryActionResult(status));
        }

        [NonAction]
        OkObjectResult InventoryActionResult(InventoryActionStatus status) => status switch
        {
            InventoryActionStatus.Failed => Ok(ApiException()),
            InventoryActionStatus.NotFound => Ok(Faild(404, "دارایی مورد نظر یافت نشد", "")),
            _ => Ok(ApiException()),
        };
    }
}

