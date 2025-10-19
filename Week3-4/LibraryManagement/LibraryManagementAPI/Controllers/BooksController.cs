using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DAL.Models;
using LibraryManagement.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        public BooksController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }
        [HttpGet]
        public IActionResult GetAllBooks() => Ok(_bookRepository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _bookRepository.GetById(id);
            return book == null ? NotFound($"Book with ID {id} not found") : Ok(book);
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] BookDTO bookDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var checkAuthor = _authorRepository.GetById(bookDTO.AuthorId);
            if (checkAuthor == null) return BadRequest("Author with this ID does not exist");

            var book = new Book
            {
                Title = bookDTO.Title,
                PublishedYear = bookDTO.PublishedYear,
                AuthorId = bookDTO.AuthorId,
            };

            var createdBook = _bookRepository.Create(book);
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookDTO bookDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var book = _bookRepository.GetById(id);
            if (book == null) return NotFound($"Book with ID {id} not found");

            var checkAuthor = _authorRepository.GetById(bookDTO.AuthorId);
            if (checkAuthor == null) return BadRequest("Author with this ID does not exist");

            book.Title = bookDTO.Title;
            book.PublishedYear = bookDTO.PublishedYear;
            book.AuthorId = bookDTO.AuthorId;
            _bookRepository.Update(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var authorToDelete = _bookRepository.GetById(id);
            if (authorToDelete == null) return NotFound($"Book with ID {id} not found");
            _bookRepository.Delete(id);
            return NoContent();
        }
    }
}
