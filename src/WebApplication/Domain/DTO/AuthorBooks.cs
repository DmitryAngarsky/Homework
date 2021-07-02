using System.Collections.Generic;

namespace Domain.DTO
{
    public class AuthorBooks
    {
        public AuthorModel Author { get; set; }
        public List<AuthorBook> Books { get; set; } = new List<AuthorBook>();
    }
}