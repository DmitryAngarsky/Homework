using System;
using System.Collections.Generic;
using WebApplication.Model;

namespace WebApplication.Database
{
    public class SeedData
    {
        public static readonly List<Person> Persons = new();
        public static readonly List<Book> Books = new();

        public static void SeedPersons()
        {
            Persons.AddRange(new List<Person>()
                {
                    new()
                    {
                        Forename = "Petr",
                        Surname = "Krivov",
                        DOB = DateTime.Today
                    },
                    new()
                    {
                        Forename = "Max",
                        Surname = "Potapov",
                        DOB = DateTime.Today
                    },
                    new()
                    {
                        Forename = "Ivan",
                        Surname = "Kotin",
                        DOB = DateTime.Today
                    }
                }
            );
        }
        
        public static void SeedBooks()
        {
            Books.AddRange(new List<Book>()
                {
                    new()
                    {
                        Title = "Clean Code",
                        Author = "Robert Martin",
                        Genre = "Software Development"
                    },
                    new()
                    {
                        Title = "War and Peace",
                        Author = "Leo Tolstoy",
                        Genre = "Novel"
                    },
                    new()
                    {
                        Title = "1984",
                        Author = "George Orwell",
                        Genre = "Dystopian"
                    }
                }
            );
        }
    }
}