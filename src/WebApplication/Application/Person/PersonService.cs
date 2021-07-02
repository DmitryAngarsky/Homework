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
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<PersonModel> Get(int id)
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            
            return _mapper.Map<PersonModel>(person);
        }
        
        //TODO: Разобраться как работает.
        public async Task<IEnumerable<PersonBook>> GetPersonBooks(int personId)
        {
            var books =  _unitOfWork
                .Books
                .GetAllWithGenre()
                .Where(b => b.Persons
                    .Select(p => p.Id)
                    .Contains(personId))
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

        public async Task<PersonModel> Add(PersonModel personModel)
        {
            var person = _mapper.Map<Person>(personModel);
            var result = await _unitOfWork.Persons.AddAsync(person);
            await _unitOfWork.Commit();

            return _mapper.Map<PersonModel>(result);
        }

        public async Task<PersonBooks> AddBookInLibraryCard(LibraryCard libraryCard)
        {
            _unitOfWork.Persons.AddPersonBook(libraryCard);
            await _unitOfWork.Commit();

            var personBooks = await _unitOfWork.Persons
                .GetAllWithBooks()
                .FirstOrDefaultAsync(p => p.Id == libraryCard.PersonId);

            return _mapper.Map<PersonBooks>(personBooks);
        }

        public async Task<PersonBooks> DeleteBookInLibraryCard(LibraryCard libraryCard)
        {
            _unitOfWork.Persons.DeletePersonBook(libraryCard);
            await _unitOfWork.Commit();

            var personBooks = await _unitOfWork.Persons
                .GetAllWithBooks()
                .FirstOrDefaultAsync(p => p.Id == libraryCard.PersonId);

            return _mapper.Map<PersonBooks>(personBooks);
        }

        public async Task<PersonModel> Update(PersonModel personModel)
        {
            var person = _mapper.Map<Person>(personModel);
            var result = _unitOfWork.Persons.Update(person);
            await _unitOfWork.Commit();

            return _mapper.Map<PersonModel>(result);
        }

        public async Task Delete(int personId)
        {
            _unitOfWork.Persons.Delete(personId);
            await _unitOfWork.Commit();
        }

        public  void Delete(PersonModel person)
        {
            var persons =  _unitOfWork.Persons.GetAllWithBooks()
                .Where(p => p.FirstName.Equals(person.FirstName) 
                            && p.LastName.Equals(person.LastName) 
                            && p.MiddleName.Equals(person.MiddleName))
                .Select(p => p.Id).ToList();

            foreach (var p in persons)
            {
                _unitOfWork.Persons.Delete(p);
            }
        }
    }
}