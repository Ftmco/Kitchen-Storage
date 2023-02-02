using Microsoft.AspNetCore.Mvc;

namespace KitchenStorage.Web.Controllers;

public class GroupController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }
}
