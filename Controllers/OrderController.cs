using dotnet_db.Models;
using dotnet_db.Services;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_db.Controllers;



[Authorize]
[Route("order")]
public class OrderController : Controller
{
    private readonly ICartService _cartService;
    private readonly DataContext _context;
    private readonly IConfiguration _config;

    public OrderController(ICartService cartService, DataContext context, IConfiguration config)
    {
        _context = context;
        _cartService = cartService;
        _config = config;
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
                OrderItems = cart.CardItems.Select(i => new Models.OrderItem
                {
                    ProductId = i.ProductId,
                    Price = i.Product.Price,
                    Quantity = i.Quantity,
                }).ToList()
            };
            var payment = await ProccesPayment(model, cart);
            if (payment.Status == "success")
            {
                _context.Orders.Add(order);
                _context.Cards.Remove(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction("Completed", new { orderId = order.Id });
            }
            else
            {
                ModelState.AddModelError("", payment.ErrorMessage);
            }
        }

        ViewBag.Cart = cart;
        return View(model);
    }

    [HttpGet("completed/{orderId}")]
    public ActionResult Completed(string orderId)
    {
        return View("Completed", orderId);
    }
    [HttpGet("order-list")]
    public async Task<ActionResult> OrderList()
    {
        var username = User.Identity?.Name!;
        var orders = await _context.Orders
                        .Include(i => i.OrderItems)
                        .ThenInclude(i => i.Product)
                        .Where(i => i.Username == username)
                        .ToListAsync();
        return View(orders);
    }


    private async Task<Payment> ProccesPayment(OrderCreateModel model, Models.Card cart)
    {
        Options options = new Options();
        options.ApiKey = _config["PaymentAPI:ApiKey"];
        options.SecretKey = _config["PaymentAPI:SecretKey"];
        options.BaseUrl = "https://sandbox-api.iyzipay.com";

        CreatePaymentRequest request = new CreatePaymentRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = Guid.NewGuid().ToString();
        request.Price = cart.SubTotal().ToString();
        request.PaidPrice = cart.TotalPrice().ToString();
        request.Currency = Currency.TRY.ToString();
        request.Installment = 1;
        request.BasketId = "B67832";
        request.PaymentChannel = PaymentChannel.WEB.ToString();
        request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

        PaymentCard paymentCard = new PaymentCard();
        paymentCard.CardHolderName = model.CartName;
        paymentCard.CardNumber = model.CartNumber;
        paymentCard.ExpireMonth = model.CartExpirationMonth;
        paymentCard.ExpireYear = model.CartExpirationYear;
        paymentCard.Cvc = model.CartCVV;
        paymentCard.RegisterCard = 0;
        request.PaymentCard = paymentCard;

        Buyer buyer = new Buyer();
        buyer.Id = "BY789";
        buyer.Name = model.Fullname;
        buyer.Surname = "Doe";
        buyer.GsmNumber = model.Phone;
        buyer.Email = "email@email.com";
        buyer.IdentityNumber = "74300864791";
        buyer.LastLoginDate = "2015-10-05 12:43:35";
        buyer.RegistrationDate = "2013-04-21 15:12:09";
        buyer.RegistrationAddress = model.Address;
        buyer.Ip = "85.34.78.112";
        buyer.City = model.City;
        buyer.Country = "Turkey";
        buyer.ZipCode = model.PostalCode;
        request.Buyer = buyer;

        Address address = new Address();
        address.ContactName = model.Fullname;
        address.City = model.City;
        address.Country = "Turkey";
        address.Description = model.Address;
        address.ZipCode = model.PostalCode;
        request.ShippingAddress = address;
        request.BillingAddress = address;

        List<BasketItem> basketItems = new List<BasketItem>();

        foreach (var item in cart.CardItems)
        {
            BasketItem basketItem = new BasketItem();
            basketItem.Id = item.Id.ToString();
            basketItem.Name = item.Product.Name;
            basketItem.Category1 = "Telefon";
            basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            basketItem.Price = item.Product.Price.ToString();

            basketItems.Add(basketItem);
        }
        request.BasketItems = basketItems;

        return await Payment.Create(request, options);
    }

}

