using AutoMapper;
using LibraryManagement.API.Models;
using LibraryManagement.BLL.DTOs;
using LibraryManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        public AuthorsController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorModel author)
        {
            var authorDTO = _mapper.Map<AuthorDTO>(author);
            var createdAuthor = await _authorService.CreateAsync(authorDTO);
            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorModel author)
        {
            var authorDTO = _mapper.Map<AuthorDTO>(author);
            authorDTO.Id = id;
            var updatedAuthor = await _authorService.UpdateAsync(authorDTO);
            return Ok(updatedAuthor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> FindByName(string name)
        {
            var authors = await _authorService.FindAuthorsByNameAsync(name);
            return Ok(authors);
        }

        [HttpGet("getAuthorsWithBooksCount")]
        public async Task<IActionResult> GetAuthorsWithBooksCount()
        {
            var authors = await _authorService.GetAuthorsWithBookCountAsync();
            return Ok(authors);
        }

        [HttpGet("getAuthorsWithBooks")]
        public async Task<IActionResult> GetAuthorsWithBooks()
        {
            var authors = await _authorService.GetAuthorsWithBooksAsync();
            return Ok(authors);
        }
    }
}
