using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database.UnitOfWork;
using Domain;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookModel>> GetAuthorBooks(AuthorModel author)
        {
            var books = await _unitOfWork.Authors
                .GetAllWithBooks()
                .Where(a => a.FirstName.Equals(author.FirstName)
                            || a.LastName.Equals(author.LastName)
                            || a.MiddleName.Equals(author.MiddleName))
                .SelectMany(a => a.Books)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BookModel>>(books);
        }

        public async Task<IEnumerable<PersonBook>> GetGenreBooks(int genreId)
        {
            var books = _unitOfWork
                .Books
                .GetAllWithGenre()
                .Where(b => b.Genres
                    .Select(p => p.Id)
                    .Contains(genreId))
                .AsEnumerable()
                .Select(async b =>
                    new PersonBook
                    {
                        Author = _mapper.Map<AuthorModel>(await _unitOfWork.Authors.GetByIdAsync(b.AuthorId)),
                        Book = _mapper.Map<BookModel>(b),
                        Genre = _mapper.Map<List<GenreModel>>(b.Genres)
                    }
                )
                .Select(b => b.Result);

            return books;
        }

        public void Delete(int bookId)
        {
            var persons = _unitOfWork.Persons
                .GetAllWithBooks()
                .Where(p => p.Books.Select(b => b.Id).Contains(bookId));

            if (!persons.Any())
                throw new Exception();
            
            _unitOfWork.Books.Delete(bookId);
            _unitOfWork.Commit();
        }
    }
}