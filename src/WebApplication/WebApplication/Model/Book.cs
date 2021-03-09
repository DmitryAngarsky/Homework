using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication.Model
{
    public class Book
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; }
        
        [Required(ErrorMessage = "Genre is required.")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Genre { get; set; }
    }
}