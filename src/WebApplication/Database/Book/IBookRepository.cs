using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Domain;
using Domain.DTO;

namespace WebApplication
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IReadOnlyList<Book>> GetAllAuthorBooksAsync(int authorId);
        Task<int> AddRangeAsync(IEnumerable<Book> books);
        Task<IEnumerable<PersonBook>> GetAllGenreBooksAsync(int id);
        Task<IEnumerable<LibraryCard>> GetBookLibraryCardsAsync(int id);
        Task<IEnumerable<Book>> GetAllAuthorsBooksAsync(int[] idArray);
    }
}