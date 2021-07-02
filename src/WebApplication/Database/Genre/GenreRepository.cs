using System.Linq;
using System.Threading.Tasks;
using Database;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Database
{
    public class GenreRepository : IGenreRepository
    {
        private readonly Context _context;

        public GenreRepository(Context context)
        {
            _context = context;
        }


        public IQueryable<Genre> GetAll()
        {
            return _context.Genres
                .AsQueryable();
        }

        public IQueryable<Genre> GetAllWithBooks()
        {
            return _context.Genres
                .Include(g => g.Books);
        }

        public async Task<Genre> AddAsync(Genre genre)
        {
            var newGenre = await _context.Genres
                .AddAsync(genre);

            return newGenre.Entity;
        }
    }
}