using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Domain;
using Domain.DTO;

namespace WebApplication
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<int> DeleteByNameAsync(Person person);
        Task<IEnumerable<PersonBook>> GetAllPersonBooksAsync(int id);
        Task<LibraryCard> GetLibraryCard(LibraryCard libraryCard);
        Task<int> AddBookInLibraryCard(LibraryCard libraryCard);
        Task<int> DeleteBookInLibraryCard(LibraryCard libraryCard);
        Task<PersonBooks> GetPersonBooks(int personId);
    }
}