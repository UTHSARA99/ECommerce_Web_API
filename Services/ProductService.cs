using e_commerce_web_api.Models;
using e_commerce_web_api.Repositories;

namespace e_commerce_web_api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task AddProductAsync(Product product) =>
            await _repository.AddAsync(product);

        public async Task DeleteProductAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product is not null)
                await _repository.DeleteAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() =>
            await _repository.GetAllAsync();

        public async Task<Product?> GetProductByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task UpdateProductAsync(Product product) =>
            await _repository.UpdateAsync(product);
    }
}
