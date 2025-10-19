using LibraryManagement.DAL.Data;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DAL.Models;

namespace LibraryManagement.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        public IEnumerable<Book> GetAll() => DataContext.Books;

        public Book? GetById(int id) => DataContext.Books.FirstOrDefault(b => b.Id == id);

        public Book Create(Book book)
        {
            book.Id = DataContext.Books.Max(b => b.Id) + 1;
            DataContext.Books.Add(book);
            return book;
        }

        public void Update(Book book)
        {
            var existing = GetById(book.Id);
            if (existing != null)
            {
                existing.Title = book.Title;
                existing.PublishedYear = book.PublishedYear;
                existing.AuthorId = book.AuthorId;
            }
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
                DataContext.Books.Remove(book);
        }
    }
}
