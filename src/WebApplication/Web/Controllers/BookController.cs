using System.Threading.Tasks;
using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    //TODO: Добавить PUT и POST.
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        
        
        [HttpPost("books")]
        public async Task<IActionResult> GetAuthorBooks(AuthorModel author)
        {
            var books = await _bookService.GetAuthorBooks(author);
            return Ok(books);
        }

        [HttpGet("genre_books/{id}")]
        public async Task<IActionResult> GetGenreBooks(int id)
        {
            var personBooks = await _bookService.GetGenreBooks(id);
            return Ok(personBooks);
        }
        
        //TODO: Добавить обработку ошибок и возвратить результать.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookService.Delete(id);
            return Ok();
        }
    }
}