using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database.UnitOfWork;
using Domain;
using Domain.DTO;

namespace Application
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<AuthorModel>> GetAll()
        {
            var authors = await _unitOfWork.Authors
                .GetAll()
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<AuthorModel>>(authors);
        }

        public async Task<AuthorModel> GetById(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            
            return _mapper.Map<AuthorModel>(author);
        }

        public async Task<AuthorBooks> GetAllWithBooks(int authorId)
        {
            var author = await _unitOfWork.Authors
                .GetByIdAsync(authorId);
            
            var books = await _unitOfWork
                .Books
                .GetAllWithGenre()
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();

            return new AuthorBooks {Author = _mapper.Map<AuthorModel>(author), Books = _mapper.Map<List<AuthorBook>>(books)};
        }

        public async Task<AuthorModel> Add(AuthorModel authorModel)
        {
            var author = _mapper.Map<Author>(authorModel);
            var result = await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.Commit();

            return _mapper.Map<AuthorModel>(result);
        }

        public async Task<AuthorBooksCollection> AddWithBooks(AuthorBooksCollection authorBooksCollection)
        {
            var author = await _unitOfWork.Authors.AddAsync(_mapper.Map<Author>(authorBooksCollection.Author));

            if (authorBooksCollection.Books.Any())
            {
                var books = _mapper.Map<List<Book>>(authorBooksCollection.Books);
                
                foreach (var book in books)
                    book.AuthorId = author.Id;
                
                await _unitOfWork.Books.AddRangeAsync(books);
                
            }
            
            await _unitOfWork.Commit();
            var resultBooks = _unitOfWork.Books
                .GetAll()
                .Where(b => b.AuthorId == author.Id);
            
            authorBooksCollection.Author = _mapper.Map<AuthorModel>(author);
            authorBooksCollection.Books = _mapper.Map<List<BookModel>>(resultBooks);
            
            return authorBooksCollection;
        }
        
        //TODO: Добавить результат.
        public async Task Delete(int authorId)
        {
            await _unitOfWork.Authors.DeleteAsync(authorId);
            await _unitOfWork.Commit();
        }
    }
}