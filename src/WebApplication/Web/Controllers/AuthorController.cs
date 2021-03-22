using System.Threading.Tasks;
using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _authorService.GetAll();
            return Ok(data);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _authorService.GetById(id);
            
            if (data == null) 
                return NotFound("Author not found");
            
            return Ok(data);
        }
        
        [HttpGet("{id}/books")]
        public async Task<IActionResult> GetAllWithBooks(int id)
        {
            var author = await _authorService.GetAllWithBooks(id);
            
            if (author == null)
                return NotFound("Author not found");

            return Ok(author);
        }
        
        [HttpPost]
        public async Task<ActionResult> Add(AuthorModel author)
        {
            var data = await _authorService.Add(author);
            return Ok(data);
        }
        
        [HttpPost("books_collection")]
        public async Task<ActionResult> Add(AuthorBooksCollection authorBooksCollection)
        {
            if (authorBooksCollection == null)
                return BadRequest();

            var result = await _authorService.AddWithBooks(authorBooksCollection);

            return Ok(result);
        }
        
        //TODO: Добавить обработку ошибок и возвратить результать.
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.Delete(id);
            
            return Ok();
        }
    }
}