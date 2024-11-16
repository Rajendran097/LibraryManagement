using LibraryManagement.Data;
using LibraryManagement.DTO;
using LibraryManagement.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly libraryContext _context;

        public AuthorsController(libraryContext context)
        {
            _context = context;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var authors = await _context.Author
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Bio = a.Bio
                })
                .ToListAsync();

            return authors;
        }

        // POST: api/authors
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> PostAuthor(AuthorDto authorDto)
        {
            var author = new Author
            {
                Name = authorDto.Name,
                Bio = authorDto.Bio
            };

            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            authorDto.Id = author.Id; // Set the ID after saving
            return CreatedAtAction(nameof(GetAuthors), new { id = authorDto.Id }, authorDto);
        }
    }
}