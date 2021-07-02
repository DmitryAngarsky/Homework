using System.Collections.Generic;

namespace Domain.DTO
{
    public class AuthorBook
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public List<GenreModel> Genres { get; set; } = new List<GenreModel>();
    }
}