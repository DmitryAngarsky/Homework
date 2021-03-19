using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Domain;
using Domain.DTO;

namespace WebApplication.Database
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<List<AuthorBook>> GetAllAuthorsBookAsync(int authorId);
        Task<IEnumerable<Author>> GetAllAuthorsAsync(Author author);
    }
}