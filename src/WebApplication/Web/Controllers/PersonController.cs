using System.Threading.Tasks;
using Database.UnitOfWork;
using Domain;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public PersonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            return Ok(person);
        }
        
        [HttpGet("person_books/{id}")]
        public async Task<IActionResult> GetPersonBooks(int id)
        {
            var personBooks = await _unitOfWork.Persons.GetAllPersonBooksAsync(id);
            return Ok(personBooks);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            var result = await _unitOfWork.Persons.AddAsync(person);
            return Ok(result);
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> Update(Person person)
        {
            var data = await _unitOfWork.Persons.UpdateAsync(person);
            return Ok(data);
        }
        
        [HttpPost("add_book")]
        public async Task<IActionResult> AddBookInLibraryCard(LibraryCard libraryCard)
        {
            var libraryCardExist = await _unitOfWork.Persons.GetLibraryCard(libraryCard) != null;
            if(libraryCardExist)
                return BadRequest("The person already has this book ");
            
            var libraryCardResult = await _unitOfWork.Persons.AddBookInLibraryCard(libraryCard);
            if(libraryCardResult == 0)
                return BadRequest("Book dont added");

            var personBooks = await _unitOfWork.Persons.GetPersonBooks(libraryCard.PersonId);
            return Ok(personBooks);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedRows = await _unitOfWork.Persons.DeleteAsync(id);
            
            if (deletedRows == 0)
                return NotFound("Person no found");

            return Ok(deletedRows);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(Person person)
        {
            var deletedRows = await _unitOfWork.Persons.DeleteByNameAsync(person);
            
            if (deletedRows == 0)
                return NotFound("Person no found");
            
            return Ok(deletedRows);
        }
        
        [HttpDelete("delete_book")]
        public async Task<IActionResult> DeleteBookInLibraryCard(LibraryCard libraryCard)
        {
            var libraryCardExist = await _unitOfWork.Persons.GetLibraryCard(libraryCard) != null;
            
            if (!libraryCardExist)
                return BadRequest("The person does not have this book ");
            
            var libraryCardResult = await _unitOfWork.Persons.DeleteBookInLibraryCard(libraryCard);
            if(libraryCardResult == 0)
                return BadRequest("Book dont deleted");

            var personBooks = await _unitOfWork.Persons.GetPersonBooks(libraryCard.PersonId);
            return Ok(personBooks);
        }
    }
}