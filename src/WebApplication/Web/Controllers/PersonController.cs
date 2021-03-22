using System.Threading.Tasks;
using Application;
using Domain;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _personService.Get(id);
            return Ok(person);
        }
        
        [HttpGet("person_books/{id}")]
        public async Task<IActionResult> GetPersonBooks(int id)
        {
            var personBooks = await _personService.GetPersonBooks(id);
            return Ok(personBooks);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PersonModel person)
        {
            var result = await _personService.Add(person);
            return Ok(result);
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> Update(PersonModel person)
        {
            var data = await _personService.Update(person);
            return Ok(data);
        }
        
        [HttpPost("add_book")]
        public async Task<IActionResult> AddBookInLibraryCard(LibraryCard libraryCard)
        {
            var personBooks = await _personService.AddBookInLibraryCard(libraryCard);
            return Ok(personBooks);
        }
        
        //TODO: Добавить обработку ошибок и возвратить результать.
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _personService.Delete(id);

            return Ok();
        }
        
        //TODO: Добавить обработку ошибок и возвратить результать.
        [HttpDelete]
        public IActionResult Delete(PersonModel person)
        {
            _personService.Delete(person);
            return Ok();
        }
        
        [HttpDelete("delete_book")]
        public async Task<IActionResult> DeleteBookInLibraryCard(LibraryCard libraryCard)
        {
            var personBooks = await _personService.DeleteBookInLibraryCard(libraryCard);
            return Ok(personBooks);
        }
    }
}