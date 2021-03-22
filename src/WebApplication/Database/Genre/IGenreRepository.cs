using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Domain;

namespace WebApplication.Database
{
    public interface IGenreRepository
    {
        IQueryable<Genre> GetAll();
        IQueryable<Genre> GetAllWithBooks();
        
        Task<Genre> AddAsync(Genre genre);
    }
}