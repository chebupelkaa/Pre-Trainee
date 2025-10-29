using AutoMapper;
using LibraryManagement.BLL.DTOs;
using LibraryManagement.BLL.Exceptions;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Interfaces;

namespace LibraryManagement.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        private readonly IAuthorRepository _authorRepository;

        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<BookDTO> CreateAsync(BookDTO item)
        {
            if (item.PublishedYear < 1000 || item.PublishedYear > DateTime.Now.Year)
                throw new ArgumentException("Invalid published year");

            var authorExists = await _authorRepository.GetByIdAsync(item.AuthorId);
            if (authorExists == null)
                throw new ArgumentException($"Author with ID {item.AuthorId} does not exist");

            var bookEntity = _mapper.Map<Book>(item);
            var createdBook = await _bookRepository.CreateAsync(bookEntity);
            return _mapper.Map<BookDTO>(createdBook);
        }

        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<BookDTO?> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                throw new NotFoundException($"Book with ID {id} not found");

            return _mapper.Map<BookDTO>(book);
        }

        public async Task<BookDTO> UpdateAsync(BookDTO item)
        {
            var existingBook = await GetByIdAsync(item.Id);
            var authorExists = await _authorRepository.GetByIdAsync(item.AuthorId);
            if (authorExists == null)
                throw new ArgumentException($"Author with ID {item.AuthorId} does not exist");

            var bookEntity = _mapper.Map<Book>(item);
            var updatedBook = await _bookRepository.UpdateAsync(bookEntity);
            return _mapper.Map<BookDTO>(updatedBook);
        }

        public async Task DeleteAsync(int id)
        {
            var book = await GetByIdAsync(id);
            await _bookRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookDTO>> GetBooksPublishedAfterAsync(int year)
        {
            if (year < 1000 || year > DateTime.Now.Year)
                throw new ArgumentException("Invalid year");

            var books = await _bookRepository.GetBooksPublishedAfterAsync(year);
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

    }
}
