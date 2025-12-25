using e_commerce_web_api.Data;
using e_commerce_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_web_api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbcontext;

        public CategoryRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _dbcontext.Categories.AddAsync(category);
            await _dbcontext.SaveChangesAsync();

            return category;
        }

        public async Task DeleteAsync(Category category)
        {
            _dbcontext.Categories.Remove(category);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbcontext.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _dbcontext.Categories.FindAsync(id);
        }

        public async Task UpdateAsync(Category category)
        {
            _dbcontext.Categories.Update(category);  
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<bool> HasAssociatedProductsAsync(int categoryId)
        {
            return await _dbcontext.Products.AnyAsync(p => p.CategoryId == categoryId);
        }

    }
}
