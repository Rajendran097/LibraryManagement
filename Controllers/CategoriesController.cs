using LibraryManagement.Data;
using LibraryManagement.DTO;
using LibraryManagement.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly libraryContext _libraryContext;

        public CategoriesController(libraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _libraryContext.Category
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync();

            return categories;
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> PostCategory(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };

            _libraryContext.Category.Add(category);
            await _libraryContext.SaveChangesAsync();

            categoryDto.Id = category.Id; // Set the ID after saving
            return CreatedAtAction(nameof(GetCategories), new { id = categoryDto.Id }, categoryDto);
        }
    }
}