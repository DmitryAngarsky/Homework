using System;

namespace WebApplication.Model
{
    public class Order
    {
        public Person Person { get; set; }
        
        public Book Book { get; set; }
        
        public DateTimeOffset TimeOfReceipt { get; set; }
    }
}