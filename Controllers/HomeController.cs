using Microsoft.AspNetCore.Mvc;
using dotnet_db.Models;

namespace dotnet_db.Controllers;

[Route("")]
public class HomeController : Controller
{
    private readonly DataContext _context;

    public HomeController(DataContext context)
    {
        _context = context;
    }

    [Route("")]
    public ActionResult Index()
    {
        var products = _context.Products.Where(mali => mali.IsHome && mali.IsActive).ToList();
        ViewData["categories"] = _context.Categories.ToList();
        return View(products);
    }

}
