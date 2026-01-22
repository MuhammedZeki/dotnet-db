using dotnet_db.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_db.Controllers;

// The /product URL is now the main and only route for product listings.
// If you have links pointing to /Product/List, update them to point to /product.
[Route("product")]
public class ProductController : Controller
{

    private readonly DataContext _context;

    public ProductController(DataContext context)
    {
        _context = context;
    }

    [Route("")] // Also accessible via /product/index
    public ActionResult Index()
    {
        var products = _context.Products.ToList();
        return View(products);
    }


    [Route("list")]
    public ActionResult List(string? q)
    {
        // var product = _context.Products.ToList();

        // var query = _context.Products.AsQueryable();
        var query = _context.Products.Where(i => i.IsActive);

        if (!string.IsNullOrEmpty(q))
        {
            query = query.Where(i => i.Name.ToLower().Contains(q.ToLower()));
        }

        return View(query.ToList());
    }


    [Route("category/{url}")]
    public ActionResult ListByCategory(string url, string? q)
    {
        // var products = _context.Products.Where(i => i.IsActive && i.Category.Url == url).ToList();
        var query = _context.Products.Where(i => i.IsActive);

        if (!string.IsNullOrEmpty(url))
        {
            query = query.Where(i => i.Category.Url == url);
        }

        if (!string.IsNullOrEmpty(q))
        {
            query = query.Where(i => i.Name.ToLower().Contains(q.ToLower()));
        }

        return View(query.ToList());
    }

    [Route("detail/{id}")]
    public ActionResult Detail(int id)
    {
        // var  product = _context.Products.FirstOrDefault(p => p.Id == id);
        var product = _context.Products.Find(id);

        if (product == null)
        {
            return RedirectToAction("Index", "Home");
        }

        ViewData["sameProducts"] = _context.Products
                                        .Where(i => i.IsActive && i.CategoryId == product.CategoryId && i.Id != product.Id)
                                        .Take(4)
                                        .ToList();

        return View(product);
    }
}