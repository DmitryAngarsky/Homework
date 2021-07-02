using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreModel>> GetAll();
        Task<IEnumerable<GenreStatistics>> GetStatistics();
        Task<GenreModel> Add(GenreModel genre);
    }
}