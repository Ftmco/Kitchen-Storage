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

        [HttpGet("Notes")]

        public async Task<IActionResult> GetNotesAsync(int page, int count)
        {
            var result = await _query.NotesAsync(page, count);
            return Ok(Success("", "", new
            {
                result.PageCount,
                Notes = _viewModel.CreateNoteViewModel(result.Result)
            }));
        }

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
            => NoteActionResult(await _action.DeleteAsync(id));

        [NonAction]
        OkObjectResult NoteActionResult(NoteActionStatus status) => status switch
        {
            NoteActionStatus.Failed => Ok(ApiException()),
            NoteActionStatus.NotFound => Ok(Failed(404, "یادداشت مورد نظر یافت نشد", "")),
            NoteActionStatus.Success => Ok(Success("علمیات با موفقیت انجام شد", "", new { })),
            _ => Ok(ApiException()),
        };
    }
}

