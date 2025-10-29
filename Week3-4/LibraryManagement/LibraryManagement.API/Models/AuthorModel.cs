using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.API.Models
{
    public class AuthorModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
