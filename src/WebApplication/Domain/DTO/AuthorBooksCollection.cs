using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class AuthorBooksCollection
    {
        [Required(ErrorMessage = "Title is required.")]
        public Author Author { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}