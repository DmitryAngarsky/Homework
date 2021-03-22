using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace WebApplication.Database
{
    public interface IAuthorRepository
    {
        IQueryable<Author> GetAll();
        IQueryable<Author> GetAllWithBooks();
        Task<Author> GetByIdAsync(int authorId);
        Task<Author> AddAsync(Author author);
        Task DeleteAsync(int authorId);
        
    }
}