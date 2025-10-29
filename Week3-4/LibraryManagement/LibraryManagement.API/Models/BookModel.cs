using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.API.Models
{
    public class BookModel
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public int PublishedYear { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}
