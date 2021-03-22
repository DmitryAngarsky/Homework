using System.Collections.Generic;

namespace Domain.DTO
{
    public class PersonBook
    {
        public BookModel Book { get; set; }
        public AuthorModel Author { get; set; }
        public List<GenreModel> Genre { get; set; }
    }
}