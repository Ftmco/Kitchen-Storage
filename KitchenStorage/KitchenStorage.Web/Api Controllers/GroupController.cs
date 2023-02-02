using KitchenStorage.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace KitchenStorage.Web.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController : ControllerBase
{
    private readonly IGroupAction _action;

    public GroupController(IGroupAction action)
    {
        _action = action;
    }

    [HttpPost("Upsert")]
    public async Task<IActionResult> UpsertAsync(UpsertGroupViewModel upsert)
    {
        var upsertGroup = await _action.UpsertAsync(upsert);
        return upsertGroup.Match(Right: (group) => Ok(Success("گروه با موفقیت ثبت شد", "", new { })),
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
