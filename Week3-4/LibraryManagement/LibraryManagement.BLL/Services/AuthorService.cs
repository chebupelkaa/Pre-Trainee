using AutoMapper;
using LibraryManagement.BLL.DTOs;
using LibraryManagement.BLL.Exceptions;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Interfaces;

namespace LibraryManagement.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<AuthorDTO> CreateAsync(AuthorDTO item)
        {
            var authorEntity = _mapper.Map<Author>(item);
            var createdAuthor = await _authorRepository.CreateAsync(authorEntity);
            return _mapper.Map<AuthorDTO>(createdAuthor);
        }

        public async Task<IEnumerable<AuthorDTO>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDTO>>(authors);
        }

        public async Task<AuthorDTO?> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                throw new NotFoundException($"Author with ID {id} not found");

            return _mapper.Map<AuthorDTO>(author);
        }

        public async Task<AuthorDTO> UpdateAsync(AuthorDTO item)
        {
            var existingAuthor = await GetByIdAsync(item.Id);
            var authorEntity = _mapper.Map<Author>(item);
            var updatedAuthor = await _authorRepository.UpdateAsync(authorEntity);
            return _mapper.Map<AuthorDTO>(updatedAuthor);
        }

        public async Task DeleteAsync(int id)
        {
            var author = await GetByIdAsync(id);

            if (author.Books?.Any() == true)
                throw new InvalidOperationException($"Cannot delete author with ID {id}. Author has associated books that must be deleted first.");

            await _authorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AuthorDTO>> FindAuthorsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                throw new ArgumentException("Search term must be at least 2 characters long");

            var authors = await _authorRepository.FindAuthorsByNameAsync(name);
            return _mapper.Map<IEnumerable<AuthorDTO>>(authors);
        }

        public async Task<IEnumerable<AuthorWithBooksDTO>> GetAuthorsWithBooksAsync()
        {
            var authors = await _authorRepository.GetAuthorsWithBooksAsync();
            return _mapper.Map<IEnumerable<AuthorWithBooksDTO>>(authors);
        }

        public async Task<IEnumerable<AuthorWithBooksCountDTO>> GetAuthorsWithBookCountAsync()
        {
            var authors = await _authorRepository.GetAuthorsWithBooksAsync();
            return authors.Select(author => new AuthorWithBooksCountDTO
            {
                Id = author.Id,
                Name = author.Name,
                DateOfBirth = author.DateOfBirth,
                BooksCount = author.Books?.Count ?? 0
            }).ToList();
        }
    }
}
