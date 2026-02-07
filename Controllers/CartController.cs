using System.Threading.Tasks;
using dotnet_db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_db.Controllers;


[Route("cart")]
public class CartController : Controller
{
    private readonly DataContext _context;

    public CartController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public async Task<ActionResult> Index()
    {
        var cart = await GetCard();
        return View(cart);
    }

    [HttpPost("add-to-cart")]
    public async Task<ActionResult> AddToCart(int productId, int quantity = 1)
    {

        var cart = await GetCard();

        var product = await _context.Products.FirstOrDefaultAsync(i => i.Id == productId);

        if (product != null)
        {
            cart.AddItemCart(product, quantity);
            await _context.SaveChangesAsync();
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost("remove-item")]
    public async Task<ActionResult> Remove(int cartItemId)
    {


        var cart = await GetCard();

        cart.RemoveItem(cartItemId);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost("increase-item")]
    public async Task<ActionResult> Increase(int cartItemId)
    {
        var cart = await GetCard();
        cart.InceareItem(cartItemId);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");

    }

    [HttpPost("decrease-item")]
    public async Task<ActionResult> Decrease(int cartItemId)
    {
        var cart = await GetCard();
        cart.DecreaseItem(cartItemId);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }


    private async Task<Card> GetCard()
    {
        var customerId = User.Identity?.Name ?? Request.Cookies["customerId"];

        var cart = await _context.Cards
                                .Include(i => i.CardItems)
                                .ThenInclude(i => i.Product)
                                .Where(i => i.CustomerId == customerId)
                                .FirstOrDefaultAsync();

        if (cart == null)
        {
            customerId = User.Identity?.Name;

            if (string.IsNullOrEmpty(customerId))
            {
                customerId = Guid.NewGuid().ToString();
                var options = new CookieOptions
                {
                    Expires = DateTime.Now.AddMonths(1),
                    IsEssential = true
                };
                Response.Cookies.Append("customerId", customerId, options);
            }
            cart = new Card { CustomerId = customerId };
            _context.Cards.Add(cart);
        }
        return cart;
    }

}