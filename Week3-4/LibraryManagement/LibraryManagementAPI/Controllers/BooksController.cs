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
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet]
        public IActionResult GetAllBooks() => Ok(_bookRepository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            try
            {
                var book = _bookRepository.GetById(id);
                return Ok(book);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] BookDTO bookDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var book = new Book
                {
                    Title = bookDTO.Title,
                    PublishedYear = bookDTO.PublishedYear,
                    AuthorId = bookDTO.AuthorId,
                };
                var createdBook = _bookRepository.Create(book);
                return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookDTO bookDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var book = new Book
                {
                    Id = id,
                    Title = bookDTO.Title,
                    PublishedYear = bookDTO.PublishedYear,
                    AuthorId = bookDTO.AuthorId
                };
                _bookRepository.Update(book);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _bookRepository.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
