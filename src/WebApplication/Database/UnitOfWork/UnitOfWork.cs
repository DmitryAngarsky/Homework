
using WebApplication;
using WebApplication.Database;

namespace Database.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IAuthorRepository authorRepository, 
            IBookRepository bookRepository, 
            IGenreRepository genreRepository, 
            IPersonRepository personRepository)
        {
            Authors = authorRepository;
            Books = bookRepository;
            Genres = genreRepository;
            Persons = personRepository;
        }
        
        public IAuthorRepository Authors { get; }
        public IBookRepository Books { get; set; }
        public IGenreRepository Genres { get; set; }
        public IPersonRepository Persons { get; set; }
    }
}