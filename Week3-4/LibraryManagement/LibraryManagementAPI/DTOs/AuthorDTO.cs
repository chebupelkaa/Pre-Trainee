using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.DTOs
{
    public class AuthorDTO
    {
        [Required(ErrorMessage = "Author name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
    }
}
