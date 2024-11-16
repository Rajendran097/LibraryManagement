using LibraryManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class libraryContext : DbContext
    {
        public libraryContext(DbContextOptions<libraryContext> options) : base(options) 
        {
        }
        public DbSet<Author> Author { get; set; }   
        public DbSet<Category> Category { get; set; }
        public DbSet<Book> Books { get; set; }  
      
    }
}
