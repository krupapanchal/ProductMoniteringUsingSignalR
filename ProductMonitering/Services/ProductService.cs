using ProductMonitering.Models;
using ProductMonitering.Repository;

namespace ProductMonitering.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<List<Product>> GetAllProductsAsync() => _productRepository.GetAllProducts();
        public Task AddProductAsync(Product product) => _productRepository.AddProduct(product);
    }
}
