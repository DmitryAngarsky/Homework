using System.Threading.Tasks;
using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _genreService
                .GetAll();
            
            return Ok(genres);
        }
        
        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var genres = await _genreService
                .GetStatistics();
            
            return Ok(genres);
        }
        
        [HttpPost]
        public async Task<ActionResult> Add(GenreModel genre)
        {
            var result = await _genreService.Add(genre);
            return Ok(result);
        }
    }
}