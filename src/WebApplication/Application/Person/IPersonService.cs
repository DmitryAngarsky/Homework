using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Domain.DTO;

namespace Application
{
    public interface IPersonService
    {
        Task<PersonModel> Get(int id);
        Task<IEnumerable<PersonBook>> GetPersonBooks(int personId);
        Task<PersonModel> Add(PersonModel person);
        Task<PersonBooks> AddBookInLibraryCard(LibraryCard libraryCard);
        Task<PersonBooks> DeleteBookInLibraryCard(LibraryCard libraryCard);
        Task<PersonModel> Update(PersonModel person);
        Task Delete(int personId);
        void Delete(PersonModel person);
    }
}