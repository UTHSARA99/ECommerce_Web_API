using e_commerce_web_api.Models;
using e_commerce_web_api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_web_api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await _categoryRepository.CreateAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                throw new ArgumentException($"Category with ID {id} does not exist.");

            bool hasProducts = await _categoryRepository.HasAssociatedProductsAsync(id);
            if (hasProducts)
                throw new InvalidOperationException("Cannot delete category because it has associated products.");

            try
            {
                await _categoryRepository.DeleteAsync(category);
            }
            catch (DbUpdateException e)
            {
                // This means, there are related products
                throw new InvalidOperationException("Cannot delete category because it is associated with one or more products.", e);
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        // TODO
        //public Task UpdateCategoryAsync(Category category)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
