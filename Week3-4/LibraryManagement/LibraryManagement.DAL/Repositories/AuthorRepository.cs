using LibraryManagement.DAL.Data;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext context):base(context) { }

        public override async Task<Author>? GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Author>> GetAuthorsWithBooksAsync()
        {
            return await _dbSet
                .Include(a => a.Books)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>> FindAuthorsByNameAsync(string name)
        {
            return await _dbSet
               .Include(a => a.Books)
               .Where(a => a.Name.Contains(name))
               .AsNoTracking()
               .ToListAsync();
        }
    }
}

