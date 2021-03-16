using System.Linq;
using System.Threading.Tasks;
using Database.UnitOfWork;
using Domain;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.Authors.GetAllAsync();
            return Ok(data);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _unitOfWork.Authors.GetByIdAsync(id);
            
            if (data == null) 
                return NotFound("Author not found");
            
            return Ok(data);
        }
        
        [HttpGet("{id}/books")]
        public async Task<IActionResult> GetAllWithBooks(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
                return NotFound("Author not found");
            
            var resultAuthorBooks = new AuthorBooks
            {
                Author = author,
                Books = await _unitOfWork.Authors.GetAllAuthorsBookAsync(id)
            };

            return Ok(resultAuthorBooks);
        }
        
        [HttpPost]
        public async Task<ActionResult> Add(Author author)
        {
            var data = await _unitOfWork.Authors.AddAsync(author);
            return Ok(data);
        }
        
        [HttpPost("books_collection")]
        public async Task<ActionResult> Add(AuthorBooksCollection authorBooksCollection)
        {
            if (authorBooksCollection == null)
                return BadRequest();
            
            var author = await _unitOfWork.Authors.AddAsync(authorBooksCollection.Author);

            foreach (var book in authorBooksCollection.Books)
                book.AuthorId = author.Id;
            
            if(authorBooksCollection.Books.Any()) 
                await _unitOfWork.Books.AddRangeAsync(authorBooksCollection.Books);

            authorBooksCollection.Author = author;
            authorBooksCollection.Books = await _unitOfWork.Books
                .GetAllAuthorBooksAsync(author.Id);
            
            return Ok(authorBooksCollection);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var authorBooks = await _unitOfWork.Books.GetAllAuthorBooksAsync(id);

            if (authorBooks.Any())
                return BadRequest("Author has books");

            var data = await _unitOfWork.Authors.DeleteAsync(id);
            return Ok(data);
        }
    }
}