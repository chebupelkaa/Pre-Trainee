using LibraryManagement.DAL.Models;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAll();
        Author? GetById(int id);
        Author Create(Author item);
        void Update(Author item);
        void Delete(int id);
    }
}
