using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.DTOs
{
    public class BookDTO
    {
        [Required(ErrorMessage = "Book title is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "PublishedYear is required")]
        [Range(1000, 2100, ErrorMessage = "Published year must be between 1000 and 2100")]
        [DefaultValue(2025)]
        public int PublishedYear { get; set; }

        [Required(ErrorMessage = "Author ID is required")]
        public int AuthorId { get; set; }
    }
}
