using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace WebApplication
{
    public class BookRepository : IBookRepository
    {
        private readonly Context _context;

        public BookRepository(Context context)
        {
            _context = context;
        }

        public IQueryable<Book> GetAll()
        {
            return _context.Books
                .AsQueryable();
        }
        
        public async Task<Book> GetByIdAsync(int bookId)
        {
            return await _context.Books
                .SingleOrDefaultAsync(a => a.Id == bookId);
        }
        
        public IQueryable<Book> GetAllWithGenre()
        {
            return _context.Books
                .Include(b => b.Genres)
                .AsQueryable();
        }

        public async Task<Book> AddAsync(Book book)
        {
            var newBook = await _context.Books
                .AddAsync(book);
            
            return newBook.Entity;
        }

        public Task AddRangeAsync(IEnumerable<Book> books)
        {
            return _context.Books.AddRangeAsync(books);
        }
        
        public void Delete(int bookId)
        {
            var book = _context.Books.Find(bookId);
            
            if (book is null) 
                return;

            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}