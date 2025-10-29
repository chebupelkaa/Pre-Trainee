using LibraryManagement.DAL.Entities;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksWithAuthorsAsync();
        Task<IEnumerable<Book>> GetBooksPublishedAfterAsync(int year);
    }
}
