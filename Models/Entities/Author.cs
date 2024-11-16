namespace LibraryManagement.Models.Entities
{
    public class Author
    {
        public int Id { get; set; }
        
        public required string Name { get; set; } = string.Empty;

        public string? Bio { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
