using LibraryManagement.DAL.Data;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context) { }

        public override async Task<Book>? GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBooksWithAuthorsAsync()
        {
            return await _dbSet
                .Include(b => b.Author)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksPublishedAfterAsync(int year)
        {
            return await _dbSet
                .Where(b => b.PublishedYear > year)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
