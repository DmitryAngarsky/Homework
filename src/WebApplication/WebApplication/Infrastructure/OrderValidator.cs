using System.Linq;
using WebApplication.Controllers;
using WebApplication.Database;
using WebApplication.Model;

namespace WebApplication.Infrastructure
{
    public class OrderValidator
    {
        private readonly Order _order;
        
        public OrderValidator(Order order)
        {
            _order = order;
        }
        
        public bool IsValidOrder()
        {
            return SeedData.Books
                       .Any(b => b.Author.Equals(_order.Book.Author)
                                 && b.Genre.Equals(_order.Book.Genre)
                                 && b.Title.Equals(_order.Book.Title))
                   && SeedData.Persons
                       .Any(p => p.Forename.Equals(_order.Person.Forename)
                                 && p.Surname.Equals(_order.Person.Surname)
                                 && p.DOB.Equals(_order.Person.DOB));
        }
    }
}