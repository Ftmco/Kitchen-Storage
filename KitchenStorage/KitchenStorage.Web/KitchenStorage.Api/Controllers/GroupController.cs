namespace KitchenStorage.Web.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController : ControllerBase
{
    private readonly IGroupAction _action;
    private readonly IGroupGet _query;
    private readonly IGroupViewModel _viewModel;

    public GroupController
        (IGroupAction action,
        IGroupViewModel viewModel,
        IGroupGet query)
    {
        _action = action;
        _viewModel = viewModel;
        _query = query;
    }

    [HttpGet("Groups")]
    public async Task<IActionResult> GetGroupsAsync(int page, int count)
    {
        var groups = await _query.GroupsAsync(page, count);
        return Ok(Success("", "", new
        {
            groups.PageCount,
            Groups = _viewModel.CreateGroupViewModel(groups.Result),
        }));
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

    [HttpGet("Delete")]
    public async Task<IActionResult> DeleteAsync(Guid id)
            => GroupActionResult(await _action.DeleteAsync(id));

    [NonAction]
    OkObjectResult GroupActionResult(GroupActionStatus status) => status switch
    {
        GroupActionStatus.Failed => Ok(ApiException()),
        GroupActionStatus.NotFound => Ok(Failed(404, "گروه مورد نظر یافت نشد", "")),
        GroupActionStatus.Success => Ok(Success("عملیات مورد نظر با موفقیت انجام شد", "", new { })),
        _ => Ok(ApiException()),
    };
}