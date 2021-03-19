using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Domain;
using Domain.DTO;

namespace WebApplication.Database
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IConfiguration _configuration;

        public AuthorRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task<Author> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Authors WHERE Id = @Id";
            
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Author>(query, new { Id = id });
                return result;
            }
        }
        
        public async Task<List<AuthorBook>> GetAllAuthorsBookAsync(int authorId)
        {
            var query = @"SELECT *
                          FROM Authors
                          INNER JOIN Books ON Books.AuthorId = Authors.Id
                          WHERE AuthorId = @AuthorId";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var authorBooks = await connection
                    .QueryAsync<AuthorBook, Book, AuthorBook>(
                        query,
                        ((authorBook, book) =>
                        {
                            authorBook.Book = book;
                            return authorBook;
                        }),
                        new {AuthorId = authorId},
                        splitOn: "Id");

                var allAuthorsBooks = authorBooks.ToList();

                const string genreQuery = @"SELECT Id, GenreName FROM Book_Genre_lnk
                                            INNER JOIN Genres ON Book_Genre_lnk.GenreId = Genres.Id
                                            WHERE BookId = @BookId";

                foreach (var authorBook in allAuthorsBooks)
                {
                    var bookId = authorBook.Book.Id;
                    var genres = await connection
                        .QueryAsync<Genre>(genreQuery, new {BookId = bookId});

                    authorBook.Genres = new List<Genre>();
                    authorBook.Genres.AddRange(genres);
                }

                return allAuthorsBooks;
            }
        }
        
        public async Task<IReadOnlyList<Author>> GetAllAsync()
        {
            string query = "SELECT * FROM Authors";
            
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Author>(query);
                return result.ToList();
            }
        }
        
        public async Task<IEnumerable<Author>> GetAllAuthorsAsync(Author author)
        {
            const string query = @" SELECT * FROM Authors 
                                    WHERE FirstName = @FirstName 
                                      AND LastName = @LastName 
                                      AND MiddleName = @MiddleName";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var result = await connection
                    .QueryAsync<Author>(query, new
                    {
                        FirstName = author.FirstName,
                        LastName = author.LastName,
                        MiddleName = author.MiddleName,
                    });
                return result;
            }
        }

        public async Task<Author> AddAsync(Author author)
        {
            string query = @"INSERT INTO Authors (FirstName, LastName, MiddleName) 
                             OUTPUT INSERTED.* 
                             VALUES (@FirstName, @LastName, @MiddleName)";
            
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<Author>(query, author);
                return result;
            }
        }

        public Task<Author> UpdateAsync(Author author)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = @"DELETE FROM Authors 
                          WHERE Id = @Id";
            
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(query, new { Id = id });
                return result;
            }
        }
    }
}