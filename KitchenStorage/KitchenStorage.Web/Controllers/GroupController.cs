using KitchenStorage.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace KitchenStorage.Web.Controllers;

public class GroupController : Controller
{
    private readonly IGroupGet _get;

    private readonly IGroupAction _action;

    private readonly IGroupViewModel _viewModel;

    public GroupController(IGroupGet get, IGroupViewModel viewModel, IGroupAction action)
    {
        _get = get;
        _viewModel = viewModel;
        _action = action;
    }

    public async Task<IActionResult> Index()
    {
        var groups = await _get.GroupsAsync();

        return View(_viewModel.CreateGroupViewModel(groups));
    }

    public IActionResult Create()
    {
        return View();
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var group = await _get.FindByIdAsync(id);
        GroupViewModel? viewModel = group is null ? null : _viewModel.CreateGroupViewModel(group);

        return View(viewModel);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var group = await _get.FindByIdAsync(id);
        GroupViewModel? viewModel = group is null ? null : _viewModel.CreateGroupViewModel(group);

        return View(viewModel);
    }
}
