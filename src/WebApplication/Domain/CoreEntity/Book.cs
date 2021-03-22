﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Book
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Title is required.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Author is required.")]
        public int AuthorId { get; set; }

        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<Person> Persons { get; set; } = new List<Person>();
    }
}