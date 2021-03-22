using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Database
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder builder)
        {
            SeedAuthors(builder);
            SeedBooks(builder);
            SeedGenres(builder);
            SeedPersons(builder);
        }

        private static void SeedAuthors(this ModelBuilder builder)
        {
            builder.Entity<Author>().HasData(
                new Author {Id = 1, FirstName = "Agatha", LastName = "Mary", MiddleName = "Clarissa"},
                new Author {Id = 2, FirstName = "Danielle", LastName = "Fernandes", MiddleName = "Steel"},
                new Author {Id = 3, FirstName = "Joanne", LastName = "Robert", MiddleName = "Rowling"});
        }
        
        private static void SeedBooks(this ModelBuilder builder)
        {
            builder.Entity<Book>().HasData(
                new Book {Id = 1, Name = "The Lord of the Rings", AuthorId = 1},
                new Book {Id = 2, Name = "The Great Gatsby", AuthorId = 2},
                new Book {Id = 3, Name = "1984", AuthorId = 3});
        }
        
        private static void SeedGenres(this ModelBuilder builder)
        {
            builder.Entity<Genre>().HasData(
                new Genre {Id = 1, GenreName = "Adventure"},
                new Genre {Id = 2, GenreName = "Alternate history"},
                new Genre {Id = 3, GenreName = "Biography"});
        }
        
        private static void SeedPersons(this ModelBuilder builder)
        {
            builder.Entity<Person>().HasData(
                new Person {Id = 1, FirstName = "Dmitry", LastName = "Angarsky", MiddleName = "Aleksandrovich", BirthDate = DateTime.Now},
                new Person {Id = 2, FirstName = "Sam", LastName = "Robert", MiddleName = "Jhonhson", BirthDate = DateTime.Now},
                new Person {Id = 3, FirstName = "Pedro", LastName = "Sanches", MiddleName = "Rodrigo", BirthDate = DateTime.Now});
        }
    }
}