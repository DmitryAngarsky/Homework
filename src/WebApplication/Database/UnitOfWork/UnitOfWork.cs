
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using WebApplication;
using WebApplication.Database;

namespace Database.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Context _context;
        private bool _disposed = false;
        public IAuthorRepository Authors { get; set; }
        public IBookRepository Books { get; set; }
        public IGenreRepository Genres { get; set; }
        public IPersonRepository Persons { get; set; }
        
        public UnitOfWork(
            IAuthorRepository authorRepository, 
            IBookRepository bookRepository, 
            IGenreRepository genreRepository, 
            IPersonRepository personRepository, 
            Context context)
        {
            Authors = authorRepository;
            Books = bookRepository;
            Genres = genreRepository;
            Persons = personRepository;
            _context = context;
        }
        
        public Task<int> Commit()
        {
            return _context.SaveChangesAsync();
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}