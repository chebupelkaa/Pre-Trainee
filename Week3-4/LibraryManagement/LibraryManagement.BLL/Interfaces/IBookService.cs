using LibraryManagement.BLL.DTOs;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IBookService
    {
        Task<BookDTO> CreateAsync(BookDTO item);
        Task<IEnumerable<BookDTO>> GetAllAsync();
        Task<BookDTO?> GetByIdAsync(int id);
        Task<BookDTO> UpdateAsync(BookDTO item);
        Task DeleteAsync(int id);
        Task<IEnumerable<BookDTO>> GetBooksPublishedAfterAsync(int year);
    }
}
