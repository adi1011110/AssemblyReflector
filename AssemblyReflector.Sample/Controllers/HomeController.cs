using Microsoft.AspNetCore.Mvc;

namespace AssemblyReflector.Sample.Controllers;

public class HomeController : BaseController
{
    [HttpGet]
    public IActionResult Home()
    {
        return null;
    }

    [HttpPost]
    public IActionResult Subscribe(string email)
    {
        return null;
    }
}
