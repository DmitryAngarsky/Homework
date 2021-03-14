using System.Collections.Generic;

namespace Domain.DTO
{
    public class PersonBooks
    {
        public PersonBooks()
        {
            Books = new List<Book>();
        }

        public Person Person { get; set; }
        public List<Book> Books { get; set; }
    }
}