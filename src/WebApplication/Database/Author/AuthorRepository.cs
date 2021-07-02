using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Database;
using Domain;

namespace WebApplication.Database
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly Context _context;

        public AuthorRepository(Context context)
        {
            _context = context;
        }

        public async Task<Author> GetByIdAsync(int authorId)
        {
            return await _context.Authors
                .SingleOrDefaultAsync(a => a.Id == authorId);
        }

        public IQueryable<Author> GetAll()
        {
            return _context.Authors
                .AsQueryable();
        }
        
        public IQueryable<Author> GetAllWithBooks()
        {
            return _context.Authors
                .Include(a => a.Books)
                .AsQueryable();
        }

        public async Task<Author> AddAsync(Author author)
        {
            var newAuthor = _context.Authors
                .AddAsync(author);
            
            await _context.SaveChangesAsync();
            
            return newAuthor.Result.Entity;
        }

        private void Delete(int authorId)
        {
            var author = _context.Authors.Find(authorId);
            
            if (author is null) 
                return;

            _context.Authors.Remove(author);
        }
        
        public Task DeleteAsync(int authorId)
        {
            return Task.Run(() => Delete(authorId));
        }
    }
}