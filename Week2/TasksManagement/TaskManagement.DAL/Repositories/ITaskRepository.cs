namespace TaskManagement.DAL.Repositories
{
    public interface ITaskRepository
    {
        Task AddAsync(Models.Task task);
        Task<IEnumerable<Models.Task>> GetAllAsync();
        Task<Models.Task> GetByIdAsync(int id);
        Task<bool> UpdateCompletionStatusAsync(int id, bool isCompleted);
        Task<bool> DeleteAsync(int id);
    }
}
