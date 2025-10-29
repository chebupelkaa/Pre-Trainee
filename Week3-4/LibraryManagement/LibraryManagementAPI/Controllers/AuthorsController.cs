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
            try
            {
                var author = _authorRepository.GetById(id);
                return Ok(author);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorDTO authorDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var author = new Author
                {
                    Name = authorDTO.Name,
                    DateOfBirth = authorDTO.DateOfBirth
                };
                var createdAuthor = _authorRepository.Create(author);
                return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
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
        public IActionResult UpdateAuthor(int id, [FromBody] AuthorDTO authorDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var author = new Author
                {
                    Id = id,
                    Name = authorDTO.Name,
                    DateOfBirth = authorDTO.DateOfBirth
                };
                _authorRepository.Update(author);
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
        public IActionResult DeleteAuthor(int id)
        {
            try
            {
                _authorRepository.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
