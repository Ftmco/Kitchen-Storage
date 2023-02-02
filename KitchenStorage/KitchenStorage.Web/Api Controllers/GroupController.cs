using KitchenStorage.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace KitchenStorage.Web.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController : ControllerBase
{
    private readonly IGroupAction _action;

    private readonly IGroupViewModel _viewModel;

    public GroupController(IGroupAction action, IGroupViewModel viewModel)
    {
        _action = action;
        _viewModel = viewModel;
    }

    [HttpPost("Upsert")]
    public async Task<IActionResult> UpsertAsync(UpsertGroupViewModel upsert)
    {
        var upsertGroup = await _action.UpsertAsync(upsert);
        return upsertGroup.Match(Right: (group) => Ok(Success("گروه با موفقیت ثبت شد", "", new
        {
            Group = _viewModel.CreateGroupViewModel(group),
        })),
                            Left: (status) => GroupActionResult(status));
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        return Ok();
    }

    [NonAction]
    OkObjectResult GroupActionResult(GroupActionStatus status) => status switch
    {
        GroupActionStatus.Success => throw new NotImplementedException(),
        GroupActionStatus.Failed => throw new NotImplementedException(),
        GroupActionStatus.NotFound => throw new NotImplementedException(),
        _ => throw new NotImplementedException(),
    };
}
