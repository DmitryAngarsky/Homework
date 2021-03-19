using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Person
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "MiddleName is required.")]
        public string MiddleName { get; set; }
        
        [Required(ErrorMessage = "Date of Birth is required.")]
        // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime BirthDate { get; set; }
    }
}