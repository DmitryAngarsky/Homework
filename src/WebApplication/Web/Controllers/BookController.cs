using System.Linq;
using System.Threading.Tasks;
using Database.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.Books.GetAllAsync();
            return Ok(data);
        }
        
        [HttpPost("books")]
        public async Task<IActionResult> GetAuthorBooks(Author author)
        {
            var authors = await _unitOfWork.Authors.GetAllAuthorsAsync(author);
            int[] authorsId = authors.Select(a => a.Id).ToArray();
            var personBooks = await _unitOfWork.Books.GetAllAuthorsBooksAsync(authorsId);
            return Ok(personBooks);
        }

        [HttpGet("genre_books/{id}")]
        public async Task<IActionResult> GetGenreBooks(int id)
        {
            var personBooks = await _unitOfWork.Books.GetAllGenreBooksAsync(id);
            return Ok(personBooks);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Book book)
        {
            var data = await _unitOfWork.Books.AddAsync(book);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bookLibraryCards = await _unitOfWork.Books.GetBookLibraryCardsAsync(id);

            if (bookLibraryCards.Any())
                return BadRequest("Person have book");
            
            var deletedRows = await _unitOfWork.Books.DeleteAsync(id);
            
            if (deletedRows == 0)
                return NotFound("Person no found");
            
            return Ok();
        }
    }
}