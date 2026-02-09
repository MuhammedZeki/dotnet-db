using System.Threading.Tasks;
using dotnet_db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_db.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("order")]
public class OrderController : Controller
{
    private readonly DataContext _context;

    public OrderController(DataContext context)
    {
        _context = context;
    }


    [Route("")]
    public ActionResult Index()
    {
        return View(_context.Orders.ToList());
    }

    [HttpGet("detail/{id}")]
    public ActionResult Detail(int id)
    {
        var order = _context.Orders
                        .Include(i => i.OrderItems)
                        .ThenInclude(i => i.Product)
                        .FirstOrDefault(i => i.Id == id);
        return View(order);
    }

    [HttpPost]
    public async Task<ActionResult> UpdateStatus(int id, OrderStatus newStatus)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            order.Status = newStatus;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}