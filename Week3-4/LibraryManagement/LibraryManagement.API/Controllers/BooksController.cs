using AutoMapper;
using LibraryManagement.API.Models;
using LibraryManagement.BLL.DTOs;
using LibraryManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookModel book)
        {
            var bookDTO = _mapper.Map<BookDTO>(book);
            var createdBook = await _bookService.CreateAsync(bookDTO);
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookModel book)
        {
            var bookDTO = _mapper.Map<BookDTO>(book);
            bookDTO.Id = id;
            var updatedBook = await _bookService.UpdateAsync(bookDTO);
            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("getBooksAfterYear")]
        public async Task<IActionResult> GetBooksAfterYear(int year)
        {
            var books = await _bookService.GetBooksPublishedAfterAsync(year);
            return Ok(books);
        }
    }
}
