using System.Collections.Generic;
using e_commerce_web_api.Models;
using e_commerce_web_api.Repositories;
using e_commerce_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_web_api.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/category/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // POST: api/category
        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            await _categoryService.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        // DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existing = await _categoryService.GetCategoryByIdAsync(id);
                if (existing == null)
                    return NotFound();

                await _categoryService.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}
