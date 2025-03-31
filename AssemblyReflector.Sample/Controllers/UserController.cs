using Microsoft.AspNetCore.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace AssemblyReflector.Sample.Controllers;

public class RegisterModel
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }
    public string Role { get; set; }
}

public class UserController : BaseController
{

    [HttpPost]
    public IActionResult LogIn(string userName, string password)
    {
        return null;
    }

    [HttpPost]
    public IActionResult Register([FromForm]RegisterModel registerModel)
    {
        return null;
    }
}
