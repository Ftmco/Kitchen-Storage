using KitchenStorage.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace KitchenStorage.Web.Controllers;

public class GroupController : Controller
{
    private readonly IGroupGet _get;

    private readonly IGroupViewModel _viewModel;

    public GroupController(IGroupGet get, IGroupViewModel viewModel)
    {
        _get = get;
        _viewModel = viewModel;
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
}
