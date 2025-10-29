using LibraryManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }
        public LibraryContext() { }
        public DbSet<Author> authors { get; set; }
        public DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)          
                .WithOne(b => b.Author)         
                .HasForeignKey(b => b.AuthorId) 
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Author>()
                .HasData(
                    new Author { Id = 1, Name = "Stephen King", DateOfBirth = new DateTime(1947, 9, 21) },
                    new Author { Id = 2, Name = "J.K. Rowling", DateOfBirth = new DateTime(1965, 7, 31) },
                    new Author { Id = 3, Name = "George Orwell", DateOfBirth = new DateTime(1903, 6, 25) },
                    new Author { Id = 4, Name = "Andy Weir", DateOfBirth = new DateTime(1972, 6, 16) }
                );

            modelBuilder.Entity<Book>()
                .HasData(
                    new Book { Id = 1, Title = "The Shining", PublishedYear = 1977, AuthorId = 1 },
                    new Book { Id = 2, Title = "Harry Potter and the Philosopher's Stone", PublishedYear = 1997, AuthorId = 2 },
                    new Book { Id = 3, Title = "It", PublishedYear = 1986, AuthorId = 1 },
                    new Book { Id = 4, Title = "1984", PublishedYear = 1949, AuthorId = 3 },
                    new Book { Id = 5, Title = "Animal Farm", PublishedYear = 1945, AuthorId = 3 },
                    new Book { Id = 9, Title = "Harry Potter and the Cursed Child", PublishedYear = 2016, AuthorId = 2 },
                    new Book { Id = 6, Title = "The Fairy Tale", PublishedYear = 2022, AuthorId = 1 }
                );
        }
    }
}
