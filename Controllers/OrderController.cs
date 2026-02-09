using dotnet_db.Models;
using dotnet_db.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_db.Controllers;



[Authorize]
[Route("order")]
public class OrderController : Controller
{
    private readonly ICartService _cartService;
    private readonly DataContext _context;

    public OrderController(ICartService cartService, DataContext context)
    {
        _context = context;
        _cartService = cartService;
    }

    [HttpGet("checkout")]
    public async Task<ActionResult> CheckOut()
    {
        ViewBag.Cart = await _cartService.GetCart(User.Identity?.Name!);
        return View();
    }

    [HttpPost("checkout")]
    public async Task<ActionResult> CheckOut(OrderCreateModel model)
    {
        var username = User.Identity?.Name!;
        var cart = await _cartService.GetCart(username);

        if (cart.CardItems.Count == 0)
        {
            return RedirectToAction("Index", "Home");
        }

        if (ModelState.IsValid)
        {
            var order = new Order
            {
                Fullname = model.Fullname,
                Address = model.Address,
                City = model.City,
                Phone = model.Phone,
                PostalCode = model.PostalCode,
                OrderNote = model.OrderNote!,
                TotalPrice = cart.TotalPrice(),
                Username = username,
                OrderDate = DateTime.Now,
                OrderItems = cart.CardItems.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Product.Price,
                }).ToList()
            };
            _context.Orders.Add(order);
            _context.Cards.Remove(cart);
            await _context.SaveChangesAsync();

            return RedirectToAction("Completed", new { orderId = order.Id });
        }

        ViewBag.Cart = cart;
        return View(model);
    }

    [HttpGet("completed/{orderId}")]
    public ActionResult Completed(string orderId)
    {
        return View("Completed", orderId);
    }
}

