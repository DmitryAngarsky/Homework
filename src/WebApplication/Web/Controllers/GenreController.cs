using System.Threading.Tasks;
using Database.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GenreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _unitOfWork.Genres.GetAllAsync();
            return Ok(genres);
        }
        
        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var genres = await _unitOfWork.Genres.GetStatistics();
            return Ok(genres);
        }
        
        [HttpPost]
        public async Task<ActionResult> Add(Genre genre)
        {
            var result = await _unitOfWork.Genres.AddAsync(genre);
            return Ok(result);
        }
    }
}