using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Domain.DTO;

namespace Application
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorModel>> GetAll();
        Task<AuthorModel> GetById(int id);
        Task<AuthorBooks> GetAllWithBooks(int authorId);
        Task<AuthorModel> Add(AuthorModel author);
        Task<AuthorBooksCollection> AddWithBooks(AuthorBooksCollection authorBooksCollection);
        Task Delete(int id);
    }
}