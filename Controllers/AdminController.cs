using Microsoft.AspNetCore.Mvc;

namespace dotnet_db.Controllers;

[Route("admin")]
public class AdminController : Controller
{

    [Route("")]
    public ActionResult Index()
    {
        return View();
    }
}