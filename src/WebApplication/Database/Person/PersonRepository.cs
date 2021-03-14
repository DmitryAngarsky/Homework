using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain;
using Domain.DTO;
using Microsoft.Extensions.Configuration;
using WebApplication;

namespace Database
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IConfiguration _configuration;

        public PersonRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task<Person> GetByIdAsync(int id)
        {
            var query = @"SELECT * FROM Persons 
                          WHERE Id = @Id";
            
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Person>(query, new { Id = id });
                return result;
            }
        }

        public async Task<IEnumerable<PersonBook>> GetAllPersonBooksAsync(int id)
        {
            var query = @"SELECT *
                          FROM Library_Card
                          inner join Books on Library_Card.BookId = Books.Id
                          inner join Authors on Books.AuthorId = Authors.Id
                          where PersonId = @Id";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var personBooks = await connection
                    .QueryAsync<PersonBook, Book, Author, PersonBook>(
                        query,
                        MapResults,
                        new {Id = id},
                        splitOn: "PersonId,Id");

                var allPersonBooksAsync = personBooks.ToList();

                const string genreQuery = @"SELECT Id, GenreName FROM Book_Genre_lnk
                                            INNER JOIN Genres ON Book_Genre_lnk.GenreId = Genres.Id
                                            WHERE BookId = @BookId";

                foreach (var personBook in allPersonBooksAsync)
                {
                    var bookId = personBook.Book.Id;
                    var genres = await connection
                        .QueryAsync<Genre>(genreQuery, new {BookId = bookId});

                    personBook.Genre = new List<Genre>();
                    personBook.Genre.AddRange(genres);
                }

                return allPersonBooksAsync;
            }
        }

        private PersonBook MapResults(PersonBook personBook, Book book, Author author)
        {
            personBook.Book = book;
            personBook.Author = author;
            return personBook;
        }
        
        public async Task<LibraryCard> GetLibraryCard(LibraryCard libraryCard)
        {
            var query = @"SELECT * FROM Library_Card 
                          WHERE BookId = @BookId 
                            AND PersonId = @PersonId";
            
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection
                    .QueryFirstOrDefaultAsync<LibraryCard>(
                        query, 
                        new
                        {
                            BookId = libraryCard.BookId, 
                            PersonId = libraryCard.PersonId
                        });
                
                return result;
            }
        }

        public async Task<int> DeleteBookInLibraryCard(LibraryCard libraryCard)
        {
            const string query = @"DELETE FROM Library_Card 
                             WHERE BookId = @BookId 
                               AND PersonId = @PersonId";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var result = await connection
                    .ExecuteAsync(
                        query, 
                        new
                        {
                            BookId =  libraryCard.BookId, 
                            PersonId = libraryCard.PersonId
                        });
                
                return result;
            }
        }

        public async Task<PersonBooks> GetPersonBooks(int personId)
        {
            var query = @"SELECT *
                          FROM Library_Card
                          inner join Books on Library_Card.BookId = Books.Id
                          inner join Persons on Library_Card.PersonId = Persons.Id
                          where PersonId = @PersonId";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var resultPersonBooks = new PersonBooks();
                var result = await connection
                    .QueryAsync<PersonBooks, Book, Person, PersonBooks>(
                        query,
                        ((personBooks, book, person) =>
                        {
                            resultPersonBooks.Person = person;
                            resultPersonBooks.Books.Add(book);

                            return personBooks;
                        }),
                        new {PersonId = personId},
                        splitOn: "PersonId,Id");

                return resultPersonBooks;
            }
        }
        
        public async Task<int> AddBookInLibraryCard(LibraryCard libraryCard)
        {
            string query = @"INSERT INTO Library_Card (BookId, PersonId) 
                             VALUES (@BookId, @PersonId)";
            
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var result = await connection
                    .ExecuteAsync(query, new { BookId =  libraryCard.BookId, PersonId = libraryCard.PersonId});
                
                return result;
            }
        }

        public Task<IReadOnlyList<Person>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Person> AddAsync(Person person)
        {
            string query = @"INSERT INTO Persons (FirstName, LastName, MiddleName, BirthDate) 
                             OUTPUT INSERTED.* 
                             VALUES (@FirstName, @LastName, @MiddleName, @BirthDate)";
            
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<Person>(query, person);
                return result;
            }
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            string query = @"UPDATE Persons SET FirstName = @FirstName, 
                                                LastName = @LastName, 
                                                MiddleName = @MiddleName, 
                                                BirthDate = @BirthDate 
                             OUTPUT INSERTED.* 
                             WHERE Id = @Id";
            
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<Person>(query, person);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = @"DELETE FROM Persons 
                          WHERE Id = @Id";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(query, new { Id = id });
                
                return result;
            }
        }

        public async Task<int> DeleteByNameAsync(Person person)
        {
            var query = @"DELETE FROM Persons 
                          WHERE FirstName = @FirstName  
                            AND LastName = @LastName 
                            AND MiddleName = @MiddleName";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(query, person);
                
                return result;
            }
        }
    }
}