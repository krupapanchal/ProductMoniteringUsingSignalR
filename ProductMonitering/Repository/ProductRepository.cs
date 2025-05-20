using Microsoft.EntityFrameworkCore;
using ProductMonitering.Models;

namespace ProductMonitering.Repository
{
    public class ProductRepository
    {
        private readonly PracticalDbContext _context;
        public ProductRepository(PracticalDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task AddProduct(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
        }
    }
}
