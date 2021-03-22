using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database.UnitOfWork;
using Domain;

namespace Application
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<GenreModel>> GetAll()
        {
            var genres = await _unitOfWork.Genres
                .GetAll()
                .ToListAsync();

            return _mapper.Map<IEnumerable<GenreModel>>(genres);
        }

        public async Task<IEnumerable<GenreStatistics>> GetStatistics()
        {
            IEnumerable<GenreStatistics> genres = await _unitOfWork.Genres
                .GetAllWithBooks()
                .Select(g =>
                    new GenreStatistics
                    {
                        Genre = _mapper.Map<GenreModel>(g),
                        BooksCount = g.Books.Count
                    })
                .ToListAsync();
            
            return genres;
        }

        public async Task<GenreModel> Add(GenreModel genreModel)
        {
            var genre = _mapper.Map<Genre>(genreModel);
            var result = await _unitOfWork.Genres.AddAsync(genre);
            
            await _unitOfWork.Commit();

            return _mapper.Map<GenreModel>(result);
        }
    }
}