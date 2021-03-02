using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private static readonly List<Book> Books = new List<Book>()
        {
            new()
            {
                Title = "Clean Code",
                Author = "Robert Martin",
                Genre = "Software Development"
            },
            new()
            {
                Title = "War and Peace",
                Author = "Leo Tolstoy",
                Genre = "Novel"
            },
            new()
            {
                Title = "1984",
                Author = "George Orwell",
                Genre = "Dystopian"
            }
        };
        
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return Books;
        }

        [HttpGet("{author}")]
        public ActionResult<IEnumerable<Book>> Get(string author)
        {
            var books = Books
                .Where(b => string.Equals(b.Author, author, StringComparison.CurrentCultureIgnoreCase));
            
            if (!books.Any())
                return NotFound();
            
            return new ObjectResult(books);
        }
        
        [HttpPost]
        public ActionResult<Book> Post(Book book)
        {
            if (book == null)
                return BadRequest();

            Books.Add(book);
            return Ok(book);
        }
        
        [HttpDelete("{author}/{title}")]
        public ActionResult<Book> Delete(string author, string title)
        {
            Book book = Books
                .FirstOrDefault(b => string.Equals(b.Author + b.Title, author + title, StringComparison.CurrentCultureIgnoreCase));
            
            if (book == null)
                return NotFound();
            
            Books.Remove(book);
            return Ok(book);
        }
    }
}