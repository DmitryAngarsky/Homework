using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Domain;

namespace WebApplication.Database
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<IEnumerable<GenreStatistics>> GetStatistics();
    }
}