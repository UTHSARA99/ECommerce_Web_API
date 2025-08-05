
using e_commerce_web_api.Data;
using e_commerce_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_web_api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbcontext;

        public ProductRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task AddAsync(Product product)
        {
            await _dbcontext.Products.AddAsync(product);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _dbcontext.Products.Remove(product);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _dbcontext.Products.ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) =>
            await _dbcontext.Products.FindAsync(id);
        

        public async Task UpdateAsync(Product product)
        {
            _dbcontext.Products.Update(product);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
