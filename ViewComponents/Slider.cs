using dotnet_db.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_db.ViewComponents;


public class Slider : ViewComponent
{

    private readonly DataContext _context;

    public Slider(DataContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var sliders = _context.Sliders.ToList();
        return View(sliders);
    }
}