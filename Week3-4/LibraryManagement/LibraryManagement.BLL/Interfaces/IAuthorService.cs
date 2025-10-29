using LibraryManagement.BLL.DTOs;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDTO> CreateAsync(AuthorDTO item);
        Task<IEnumerable<AuthorDTO>> GetAllAsync();
        Task<AuthorDTO?> GetByIdAsync(int id);
        Task<AuthorDTO> UpdateAsync(AuthorDTO item);
        Task DeleteAsync(int id);
        Task<IEnumerable<AuthorWithBooksDTO>> GetAuthorsWithBooksAsync();
        Task<IEnumerable<AuthorWithBooksCountDTO>> GetAuthorsWithBookCountAsync();
        Task<IEnumerable<AuthorDTO>> FindAuthorsByNameAsync(string name);
    }
}
