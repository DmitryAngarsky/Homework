using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain;
using Domain.DTO;
using Microsoft.Extensions.Configuration;

namespace WebApplication
{
    public class BookRepository : IBookRepository
    {
        private readonly IConfiguration _configuration;

        public BookRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public Task<Book> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<Book>> GetAllAsync()
        {
            string query = "SELECT * FROM Books";
            
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Book>(query);
                return result.ToList();
            }
        }
        
        public async Task<IEnumerable<PersonBook>> GetAllGenreBooksAsync(int genreId)
        {
            const string query = @"SELECT *
                                   FROM Library_Card
                                   INNER JOIN Books ON Library_Card.BookId = Books.Id
                                   INNER JOIN Authors ON Books.AuthorId = Authors.Id
                                   INNER JOIN Book_Genre_lnk ON Books.Id = Book_Genre_lnk.BookId
                                   WHERE GenreId = @GenreId";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var resultGenreBooks = await connection
                    .QueryAsync<PersonBook, Book, Author, PersonBook>(
                        query,
                        (genreBooks, book, author) =>
                        {
                            genreBooks.Book = book;
                            genreBooks.Author = author;

                            return genreBooks;
                        },
                        new {GenreId = genreId},
                        splitOn: "PersonId,Id");

                var allGenreBooks = resultGenreBooks.ToList();

                const string genreQuery = @"SELECT Id, GenreName FROM Book_Genre_lnk
                                            INNER JOIN Genres ON Book_Genre_lnk.GenreId = Genres.Id
                                            WHERE BookId = @BookId";

                foreach (var genreBook in allGenreBooks)
                {
                    var bookId = genreBook.Book.Id;
                    var genres = await connection
                        .QueryAsync<Genre>(genreQuery, new {BookId = bookId});

                    genreBook.Genre = new List<Genre>();
                    genreBook.Genre.AddRange(genres);
                }

                return allGenreBooks;
            }
        }

        public async Task<IReadOnlyList<Book>> GetAllAuthorBooksAsync(int authorId)
        {
            string query = @"SELECT * FROM Books 
                             WHERE AuthorId = @AuthorId";
            
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var result = await connection
                    .QueryAsync<Book>(query, new { AuthorId = authorId });
                
                return result.ToList();
            }
        }
        
        public async Task<IEnumerable<LibraryCard>> GetBookLibraryCardsAsync(int id)
        {
            var query = @"Select * FROM Library_Card 
                          WHERE BookId = @Id";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var result = await connection
                    .QueryAsync<LibraryCard>(query, new { Id = id });
                
                return result;
            }
        }
        
        public async Task<IEnumerable<Book>> GetAllAuthorsBooksAsync(int[] idArray)
        {
            var query = string.Format(@"SELECT * FROM Authors 
                                             INNER JOIN Books on Authors.Id = Books.AuthorId 
                                             WHERE AuthorId in ({0})", string.Join(", ", idArray));

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var authorBooks = await connection
                    .QueryAsync<Book>(
                        query);
                return authorBooks;
            }
        }

        public async Task<Book> AddAsync(Book book)
        {
            string query = "INSERT INTO Books (Name, AuthorId) VALUES (@Name, @AuthorId)";
            
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Book>(query, book);
                return result;
            }
        }

        public Task<Book> UpdateAsync(Book entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> AddRangeAsync(IEnumerable<Book> books)
        {
            string query = "INSERT INTO Books (Name, AuthorId) VALUES (@Name, @AuthorId)";
            
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(query, books);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = @"DELETE FROM Books 
                          WHERE Id = @Id";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(query, new { Id = id });
                
                return result;
            }
        }
    }
}