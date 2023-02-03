using KitchenStorage.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace KitchenStorage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IGetNote _query;
        private readonly INoteAction _action;
        private readonly INoteViewModel _viewModel;

        public NoteController
            (IGetNote noteQuery,
            INoteAction noteAction,
            INoteViewModel noteViewModel)
        {
            _query = noteQuery;
            _action = noteAction;
            _viewModel = noteViewModel;
        }

        [HttpGet("GetPaginated")]

        public async Task<IActionResult> GetAsync(int page, int pageCount)
        {
            var result = await _query.NotesAsync(page, pageCount);
            return Ok(Success("", "", result));
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAsync()
            => Ok(Success("", "", await _query.NotesAsync()));


        [HttpPost("Upsert")]
        public async Task<IActionResult> UpsertAsync(UpsertNoteViewModel upsert)
        {
            var upsertNote = await _action.UpsertAsync(upsert);
            return upsertNote.Match(Right: (note) => Ok(Success("یادداشت با موفقیت ثبت شد", "", new
            {
                Note = _viewModel.CreateNoteViewModel(note),
            })),
                                Left: (status) => NoteActionResult(status));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var delete = await _action.DeleteAsync(id);
            return delete.Match(Right: (note) => Ok(Success("یادداشت با موفقیت حذف شد", "", new
            {
                Note = _viewModel.CreateNoteViewModel(note),
            })),
                                Left: (status) => NoteActionResult(status));
        }

        [NonAction]
        OkObjectResult NoteActionResult(NoteActionStatus status) => status switch
        {
            NoteActionStatus.Failed => Ok(ApiException()),
            NoteActionStatus.NotFound => Ok(Faild(404, "یادداشت مورد نظر یافت نشد", "")),
            _ => Ok(ApiException()),
        };
    }
}

