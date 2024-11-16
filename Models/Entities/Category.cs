namespace LibraryManagement.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}