namespace LibraryManagement.BLL.DTOs
{
    public class AuthorWithBooksDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public List<BookDTO> Books { get; set; } = new();
    }
}
