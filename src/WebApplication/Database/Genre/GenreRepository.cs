using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain;
using Microsoft.Extensions.Configuration;

namespace WebApplication.Database
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IConfiguration _configuration;

        public GenreRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public Task<Genre> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<Genre>> GetAllAsync()
        {
            string query = "SELECT * FROM Genres";
            
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<Genre>(query);
                return result.ToList();
            }
        }

        public async Task<Genre> AddAsync(Genre genre)
        {
            string query = @"INSERT INTO Genres (GenreName) 
                             OUTPUT INSERTED.* 
                             VALUES (@GenreName)";
            
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<Genre>(query, genre);
                return result;
            }
        }
        
        public async Task<IEnumerable<GenreStatistics>> GetStatistics()
        {
            const string query = @"SELECT COUNT(GenreId) AS BooksCount, Id, GenreName 
                                   FROM Book_Genre_lnk 
                                   INNER JOIN Genres ON Book_Genre_lnk.GenreId = Genres.Id 
                                   GROUP BY Id, GenreName";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var result = await connection
                    .QueryAsync<GenreStatistics, Genre, GenreStatistics>(query, MapResults, splitOn: "Id");
                
                return result;
            }
        }

        private GenreStatistics MapResults(GenreStatistics genreStatistics, Genre genre)
        {
            genreStatistics.Genre = genre;
            return genreStatistics;
        }

        public Task<Genre> UpdateAsync(Genre genre)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}