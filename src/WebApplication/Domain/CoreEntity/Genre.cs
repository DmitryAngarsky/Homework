using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Genre
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "GenreName is required.")]
        public string GenreName { get; set; }
    }
}