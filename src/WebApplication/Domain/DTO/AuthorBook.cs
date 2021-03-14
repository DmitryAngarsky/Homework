using System.Collections.Generic;

namespace Domain.DTO
{
    public class AuthorBook
    {
        public AuthorBook()
        {
            Genres = new List<Genre>();
        }

        public Book Book { get; set; }
        public List<Genre> Genres { get; set; }
    }
}