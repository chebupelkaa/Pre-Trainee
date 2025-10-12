using Dapper;
using TaskManagement.DAL.DbConnection;

namespace TaskManagement.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly SqlServerConnection _connection;
        public TaskRepository(SqlServerConnection connection)
        {
            _connection = connection;
        }

        public async Task AddAsync(Models.Task task)
        {
            using var connection = _connection.CreateConnection();
            var sql = @"INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt)
                VALUES (@Title, @Description, @IsCompleted, @CreatedAt)";

            await connection.ExecuteAsync(sql, task);
        }

        public async Task<Models.Task> GetByIdAsync(int id)
        {
            using var connection = _connection.CreateConnection();
            var task = await connection.QueryFirstOrDefaultAsync<Models.Task>(
                "SELECT * FROM Tasks WHERE Id = @Id", new { Id = id });

            return task;
        }

        public async Task<IEnumerable<Models.Task>> GetAllAsync()
        {
            using var connection = _connection.CreateConnection();
            return await connection.QueryAsync<Models.Task>("SELECT * FROM Tasks ORDER BY Id ASC");
        }

        public async Task<bool> UpdateCompletionStatusAsync(int id, bool isCompleted)
        {
            using var connection = _connection.CreateConnection();
            var affectedRows = await connection.ExecuteAsync("UPDATE Tasks SET IsCompleted = @IsCompleted WHERE Id = @Id",
                new { Id = id, IsCompleted = isCompleted });

            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _connection.CreateConnection();
            var affectedRows = await connection.ExecuteAsync("DELETE FROM Tasks WHERE Id = @Id", new { Id = id });

            return affectedRows > 0;
        }
    }
}
