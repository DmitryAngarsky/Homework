using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using WebApplication;

namespace Database
{
    public class PersonRepository : IPersonRepository
    {
        private readonly Context _context;

        public PersonRepository(Context context)
        {
            _context = context;
        }

        public async Task<Person> GetByIdAsync(int personId)
        {
            return await _context.Persons
                .SingleOrDefaultAsync(p => p.Id == personId);
        }
        
        public IQueryable<Person> GetAllWithBooks()
        {
            return _context.Persons
                .Include(p => p.Books)
                .AsQueryable();
        }
        
        public async Task<Person> AddAsync(Person person)
        {
            var newPerson = _context.Persons
                .AddAsync(person);
            
            await _context.SaveChangesAsync();
            
            return newPerson.Result.Entity;
        }
        
        public void AddPersonBook(LibraryCard libraryCard)
        {
            var person = _context.Persons
                .Find(libraryCard.PersonId);
            
            var book = _context.Books
                .Find(libraryCard.BookId);
            
            person.Books.Add(book);
        }

        public Person Update(Person person)
        {
            return _context.Persons.Update(person).Entity;
        }

        public void DeletePersonBook(LibraryCard libraryCard)
        {
            var person = _context.Persons
                .Find(libraryCard.PersonId);
            
            var book = _context.Books
                .Find(libraryCard.BookId);
            
            person.Books.Remove(book);
        }

        public void Delete(int personId)
        {
            var person = _context.Persons.Find(personId);
            
            if (person is null) 
                return;

            _context.Persons.Remove(person);
            _context.SaveChanges();
        }
    }
}