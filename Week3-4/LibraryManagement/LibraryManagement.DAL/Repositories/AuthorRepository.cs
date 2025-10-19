using LibraryManagement.DAL.Data;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DAL.Models;

namespace LibraryManagement.DAL.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public IEnumerable<Author> GetAll() => DataContext.Authors;

        public Author? GetById(int id) => DataContext.Authors.FirstOrDefault(a => a.Id == id);

        public Author Create(Author author)
        {
            author.Id = DataContext.Authors.Max(a => a.Id) + 1;
            DataContext.Authors.Add(author);
            return author;
        }

        public void Update(Author author)
        {
            var existing = GetById(author.Id);
            if (existing != null)
            {
                existing.Name = author.Name;
                existing.DateOfBirth = author.DateOfBirth;
            }
        }

        public void Delete(int id)
        {
            var author = GetById(id);
            if (author != null)
                DataContext.Authors.Remove(author);
        }
    }
}

