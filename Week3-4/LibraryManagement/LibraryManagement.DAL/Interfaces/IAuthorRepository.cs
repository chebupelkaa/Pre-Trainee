using LibraryManagement.DAL.Entities;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IAuthorRepository:IRepository<Author>
    {
        Task<IEnumerable<Author>> GetAuthorsWithBooksAsync();
        Task <IEnumerable<Author>> FindAuthorsByNameAsync(string name);
    }
}