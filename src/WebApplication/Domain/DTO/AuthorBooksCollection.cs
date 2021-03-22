using System.Collections.Generic;

namespace Domain
{
    public class AuthorBooksCollection
    {
        public AuthorModel Author { get; set; }
        public List<BookModel> Books { get; set; }
    }
}