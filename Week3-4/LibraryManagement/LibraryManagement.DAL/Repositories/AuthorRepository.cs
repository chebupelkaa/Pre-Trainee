using LibraryManagement.DAL.Data;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DAL.Models;

namespace LibraryManagement.DAL.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public IEnumerable<Author> GetAll() => DataContext.Authors;

        public Author GetById(int id) 
        {
            var author = DataContext.Authors.FirstOrDefault(b => b.Id == id);
            if (author == null)
                throw new KeyNotFoundException($"Author with ID {id} not found");
            return author;
        }

        public Author Create(Author author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            author.Id = DataContext.Authors.Max(a => a.Id) + 1;
            DataContext.Authors.Add(author);
            return author;
        }

        public void Update(Author author)
        {
            var existing = GetById(author.Id);
            existing.Name = author.Name;
            existing.DateOfBirth = author.DateOfBirth;
        }

        public void Delete(int id)
        {
            var author = GetById(id);
            DataContext.Authors.Remove(author);
        }
    }
}

