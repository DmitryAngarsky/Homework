using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Model
{
    public class Order
    {
        [Required(ErrorMessage = "Person is required.")]
        public Person Person { get; set; }
        
        [Required(ErrorMessage = "Book is required.")]
        public Book Book { get; set; }
        
        [Required(ErrorMessage = "TimeOfReceipt is required.")]
        public DateTimeOffset TimeOfReceipt { get; set; }
    }
}