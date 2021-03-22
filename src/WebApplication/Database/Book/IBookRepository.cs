using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Domain;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace WebApplication
{
    public interface IBookRepository
    {
        IQueryable<Book> GetAll();
        Task<Book> GetByIdAsync(int bookId);
        IQueryable<Book> GetAllWithGenre();
        Task AddRangeAsync(IEnumerable<Book> books);
        void Delete(int bookId);
    }
}