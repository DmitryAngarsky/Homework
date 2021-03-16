using System.Collections.Generic;

namespace Domain.DTO
{
    public class AuthorBooks
    {
        public AuthorBooks()
        {
            Books = new List<AuthorBook>();
        }

        public Author Author { get; set; }
        public List<AuthorBook> Books { get; set; }
    }
}