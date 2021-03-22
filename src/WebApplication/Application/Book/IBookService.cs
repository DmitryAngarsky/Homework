using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Domain.DTO;

namespace Application
{
    public interface IBookService
    {
        Task<IEnumerable<BookModel>> GetAuthorBooks(AuthorModel author);
        Task<IEnumerable<PersonBook>> GetGenreBooks(int genreId);
        void Delete(int bookId);
    }
}