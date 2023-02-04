using KitchenStorage.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace KitchenStorage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementTypeController : ControllerBase
    {
        private readonly IMeasurementTypeAction _action;
        private readonly IGetMeasurementType _query;
        private  readonly IMeasurementTypeViewModel _viewModel;

        public MeasurementTypeController
            (IMeasurementTypeAction action,
            IGetMeasurementType query,
            IMeasurementTypeViewModel viewModel)
        {
            _action = action;
            _query = query;
            _viewModel = viewModel;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(Success("", "", await _query.TypesAsync()));
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
        {
            var delete = await _action.DeleteAsync(id);
            return delete.Match(Right: (measurement) => Ok(Success("نوع اندازه گیری با موفقیت حذف شد", "", new
            {
                Measurement = _viewModel.CreateMeasurementTypeViewModel(measurement),
            })),
                                Left: (status) => MeasurementActionResult(status));
        }

        [NonAction]
        OkObjectResult MeasurementActionResult(MeasurementTypeActionStatus status) => status switch
        {
            MeasurementTypeActionStatus.Failed => Ok(ApiException()),
            MeasurementTypeActionStatus.NotFound => Ok(Faild(404, "نوع اندازگیری مورد نظر یافت نشد", "")),
            _ => Ok(ApiException()),
        };
    }
}
