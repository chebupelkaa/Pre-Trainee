using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DAL.Models;
using LibraryManagement.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        [HttpGet]
        public IActionResult GetAllAuthors() => Ok(_authorRepository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _authorRepository.GetById(id);
            return author == null ? NotFound($"Author with ID {id} not found") : Ok(author);
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorDTO authorDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var author = new Author
            {
                Name = authorDTO.Name,
                DateOfBirth = authorDTO.DateOfBirth
            };

            var createdAuthor = _authorRepository.Create(author);
            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] AuthorDTO authorDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var author = _authorRepository.GetById(id);
            if (author == null) return NotFound($"Author with ID {id} not found");

            author.Name = authorDTO.Name;
            author.DateOfBirth = authorDTO.DateOfBirth;
            _authorRepository.Update(author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = _authorRepository.GetById(id);
            if (author == null) return NotFound($"Author with ID {id} not found");
            _authorRepository.Delete(id);
            return NoContent();
        }
    }
}
