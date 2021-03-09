using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication.Model
{
    public class Person
    {
        [Required(ErrorMessage = "Forename is required.")]
        public string Forename { get; set; }
        
        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "Date of Birth is required.")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime DOB { get; set; }
    }
}