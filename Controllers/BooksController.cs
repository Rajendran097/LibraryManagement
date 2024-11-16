using LibraryManagement.Data;
using LibraryManagement.DTO;
using LibraryManagement.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly libraryContext _libraryContext;

        public BooksController(libraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _libraryContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    PublicationYear = b.PublicationYear,
                    AuthorId = b.AuthorId,
                    CategoryId = b.CategoryId
                })
                .ToListAsync();

            return books;
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<BookDto>> PostBook(BookDto bookDto)
        {
            // Validate Author
            var authorExists = await _libraryContext.Author.AnyAsync(a => a.Id == bookDto.AuthorId);
            if (!authorExists)
            {
                return NotFound(new { Message = "Author not found." });
            }

            // Validate Category
            var categoryExists = await _libraryContext.Category.AnyAsync(c => c.Id == bookDto.CategoryId);
            if (!categoryExists)
            {
                return NotFound(new { Message = "Category not found." });
            }

            // Create new Book entity
            var book = new Book
            {
                Title = bookDto.Title,
                Description = bookDto.Description,
                PublicationYear = bookDto.PublicationYear,
                AuthorId = bookDto.AuthorId,
                CategoryId = bookDto.CategoryId
            };

            // Add book to context and save
            _libraryContext.Books.Add(book);
            await _libraryContext.SaveChangesAsync();

            bookDto.Id = book.Id;
            return CreatedAtAction(nameof(GetBooks), new { id = bookDto.Id }, bookDto);
        }
    }
}