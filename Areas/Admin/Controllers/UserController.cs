using dotnet_db.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_db.Controllers;



[Area("Admin")]
[Route("admin/users")]
public class UserController : Controller
{


    private UserManager<AppUser> _userManager;

    public UserController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("")]
    public ActionResult Index()
    {
        return View(_userManager.Users);
    }

}