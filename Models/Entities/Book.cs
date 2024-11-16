namespace LibraryManagement.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public int PublicationYear { get; set; }
        public int AuthorId { get; set; } // Foreign Key
        public Author? Author { get; set; } // Navigation Property
        public int CategoryId { get; set; } // Foreign Key
        public Category? Category { get; set; } // Navigation Property
    }
}