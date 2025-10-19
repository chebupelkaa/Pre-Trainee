using LibraryManagement.DAL.Models;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book GetById(int id);
        Book Create(Book item);
        void Update(Book item);
        void Delete(int id);
        bool AuthorExists(int authorId);
    }
}
