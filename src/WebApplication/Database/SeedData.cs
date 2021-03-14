using System.Data.SqlClient;
using Dapper;

namespace WebApplication.Database
{
    public static class SeedData
    {
        public static async void Seed()
        {
            await using (var connection = new SqlConnection())
            {
                connection.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Library_Application;Integrated Security=True;";
                connection.Open();
                
                var persons = @"INSERT INTO Persons (FirstName, LastName, MiddleName, BirthDate)
                                VALUES ('Dmitry', 'Angarsky', 'Aleksandrovich', '18-06-12 10:34:09 AM'),
                                       ('Sam', 'Robert', 'Jhonhson', '18-06-12 10:34:09 AM'),
                                       ('Pedro', 'Sanches', 'Rodrigo', '18-06-12 10:34:09 AM')";
                
                var authors = @"INSERT INTO Authors (FirstName, LastName, MiddleName)
                                VALUES ('Agatha', 'Mary', 'Clarissa'),
                                       ('Danielle', 'Fernandes', 'Steel'),
                                       ('Joanne', 'Robert', 'Rowling')";
                
                var books = @"INSERT INTO Books (Name, AuthorId)
                                VALUES ('The Lord of the Rings', 1),
                                       ('The Great Gatsby', 2),
                                       ('1984', 3)";
                
                var genres = @"INSERT INTO Genres (GenreName)
                                VALUES ('Adventure'),
                                       ('Alternate history'),
                                       ('Biography')";

                await connection.QueryMultipleAsync(string.Join(";", persons, authors, genres, books));
            }
        }
    }
}