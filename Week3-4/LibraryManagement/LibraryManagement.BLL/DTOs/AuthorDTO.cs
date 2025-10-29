using System.Text.Json.Serialization;

namespace LibraryManagement.BLL.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        [JsonIgnore]
        public List<BookDTO>? Books { get; set; }
    }
}
