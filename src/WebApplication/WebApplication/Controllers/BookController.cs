using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Database;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly List<Book> _books;

        public BookController()
        {
            _books = SeedData.Books;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return _books;
        }

        [HttpGet("{author}")]
        public ActionResult<IEnumerable<Book>> Get(string author)
        {
            var books = _books
                .Where(b => string.Equals(b.Author, author, StringComparison.CurrentCultureIgnoreCase));
            
            if (!books.Any())
                return NotFound();
            
            return new ObjectResult(books);
        }
        
        [HttpPost]
        public ActionResult<IEnumerable<Book>> Post(Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _books.Add(book);
            return new ObjectResult(_books);
        }
        
        [HttpDelete("{author}/{title}")]
        public ActionResult<Book> Delete(string author, string title)
        {
            Book book = _books
                .FirstOrDefault(b => string.Equals(b.Author + b.Title, author + title, StringComparison.CurrentCultureIgnoreCase));
            
            if (book == null)
                return NotFound();
            
            _books.Remove(book);
            return Ok(book);
        }
    }
}