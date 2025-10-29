using LibraryManagement.DAL.Entities;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IRepository <T> where T : class
    {
        Task<T> CreateAsync(T item);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T>? GetByIdAsync(int id);
        Task<T> UpdateAsync(T item);
        Task DeleteAsync(int id);    
    }
}
