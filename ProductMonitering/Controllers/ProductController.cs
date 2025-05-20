using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProductMonitering.Models;
using ProductMonitering.Services;
using ProductMonitering.SignalR;

namespace ProductMonitering.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly IHubContext<ProductHub> _hubContext;
        private readonly PracticalDbContext _context;

        public ProductController(ProductService productService, IHubContext<ProductHub> hubContext, PracticalDbContext context)
        {
            _productService = productService;
            _hubContext = hubContext;
            _context = context;
        }

        public async Task<IActionResult> Monitor()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            // var products = await _productService.GetAllProductsAsync();

            var products = await _context.Products
               .Include(p => p.Cat)
               .Select(p => new
               {
                   id = p.Id,
                   name = p.Name,
                   price = p.Price,
                   stock = p.Stock,
                   categoryName = p.Cat != null ? p.Cat.Name : "No Category"
               }).ToListAsync();
            return Json(products);
        }
        public async Task<IActionResult> Add()
        {

            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        public async Task<IActionResult> AddProduct(Product product)
        {
            await _productService.AddProductAsync(product);
            await _hubContext.Clients.All.SendAsync("ReceiveUpdate");
            return RedirectToAction("Add");
        }
    }
}
