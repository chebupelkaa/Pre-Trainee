using LibraryManagement.DAL.Models;

namespace LibraryManagement.DAL.Data
{
    public class DataContext
    {
        public static List<Author> Authors { get; } = [];
        public static List<Book> Books { get; } = [];
        static DataContext()
        {
            Authors.AddRange(
            [
                new Author { Id = 1, Name = "Stephen King", DateOfBirth = new DateTime(1947, 9, 21) },
                new Author { Id = 2, Name = "J.K. Rowling", DateOfBirth = new DateTime(1965, 7, 31) },
                new Author { Id = 3, Name = "George Orwell", DateOfBirth = new DateTime(1903, 6, 25) }
            ]);

            Books.AddRange(
            [
                new Book { Id = 1, Title = "The Shining", PublishedYear = 1977, AuthorId = 1 },
                new Book { Id = 2, Title = "Harry Potter and the Philosopher's Stone", PublishedYear = 1997, AuthorId = 2 },
                new Book { Id = 3, Title = "1984", PublishedYear = 1949, AuthorId = 3 },
                new Book { Id = 4, Title = "It", PublishedYear = 1986, AuthorId = 1 }
            ]);
        }
    }
}
