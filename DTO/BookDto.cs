namespace LibraryManagement.DTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int PublicationYear { get; set; }
        public int AuthorId { get; set; } 
        public int CategoryId { get; set; } 

    }
}
