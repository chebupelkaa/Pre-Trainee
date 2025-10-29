namespace LibraryManagement.BLL.DTOs
{
    public class AuthorWithBooksCountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int BooksCount { get; set; }
    }
}
