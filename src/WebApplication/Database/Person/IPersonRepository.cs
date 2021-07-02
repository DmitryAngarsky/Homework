using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Domain;
using Domain.DTO;

namespace WebApplication
{
    public interface IPersonRepository
    {
        Task<Person> GetByIdAsync(int personId);
        public IQueryable<Person> GetAllWithBooks();
        Task<Person> AddAsync(Person person);
        void AddPersonBook(LibraryCard libraryCard);
        void DeletePersonBook(LibraryCard libraryCard);
        Person Update(Person person);

        void Delete(int personId);
        // Task<int> DeleteByNameAsync(Person person);
        // Task<IEnumerable<PersonBook>> GetAllPersonBooksAsync(int id);
    }
}