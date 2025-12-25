using e_commerce_web_api.Models;

namespace e_commerce_web_api.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        //Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<bool> HasAssociatedProductsAsync(int categoryId);

    }
}
