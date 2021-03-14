using System.Collections.Generic;

namespace Domain.DTO
{
    public class PersonBook
    {
        public Book Book { get; set; }
        public Author Author { get; set; }
        public List<Genre> Genre { get; set; }
    }
}