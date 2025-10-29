using LibraryManagement.DAL.Data;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DAL.Models;

namespace LibraryManagement.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IAuthorRepository _authorRepository;
        public BookRepository(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public IEnumerable<Book> GetAll() => DataContext.Books;

        public Book GetById(int id) 
        {
            var book = DataContext.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found");
            return book;
        }

        public Book Create(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            if (!AuthorExists(book.AuthorId))
                throw new KeyNotFoundException($"Author with ID {book.AuthorId} does not exist");

            book.Id = DataContext.Books.Max(b => b.Id) + 1;
            DataContext.Books.Add(book);
            return book;
        }

        public void Update(Book book)
        {
            var existing = GetById(book.Id);

            if (!AuthorExists(book.AuthorId))
                throw new KeyNotFoundException($"Author with ID {book.AuthorId} does not exist");

            existing.Title = book.Title;
            existing.PublishedYear = book.PublishedYear;
            existing.AuthorId = book.AuthorId;
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            DataContext.Books.Remove(book);
        }

        public bool AuthorExists(int authorId) => _authorRepository.GetById(authorId) != null;
    }
}
