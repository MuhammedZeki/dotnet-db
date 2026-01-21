using dotnet_db.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_db.ViewComponents;


public class Navbar : ViewComponent
{
    private readonly DataContext _context;

    public Navbar(DataContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        return View(_context.Categories.ToList());
    }
}

//Views/{Scope}/Components/{Name}/Default.cshtml