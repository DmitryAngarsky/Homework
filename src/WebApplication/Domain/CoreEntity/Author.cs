﻿using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Author
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "MiddleName is required.")]
        public string MiddleName { get; set; }
    }
}