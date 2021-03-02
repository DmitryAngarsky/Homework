using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private static readonly List<Person> Persons = new List<Person>()
        {
            new()
            {
                Forename = "Petr",
                Surname = "Krivov",
                DOB = DateTime.Now
            },
            new()
            {
                Forename = "Max",
                Surname = "Potapov",
                DOB = DateTime.Now
            },
            new()
            {
                Forename = "Ivan",
                Surname = "Kotin",
                DOB = DateTime.Now
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            return Persons;
        }
        
        [HttpGet("{forename}")]
        public ActionResult<IEnumerable<Person>> Get(string forename)
        {
            var persons = Persons
                .Where(b => string.Equals(b.Forename, forename, StringComparison.CurrentCultureIgnoreCase));
            
            if (!persons.Any())
                return NotFound();
            
            return new ObjectResult(persons);
        }
        
        [HttpPost]
        public ActionResult<Person> Post(Person person)
        {
            if (person == null)
                return BadRequest();

            Persons.Add(person);
            return Ok(person);
        }
        
        [HttpDelete("{forename}/{surname}")]
        public ActionResult<Person> Delete(string forename, string surname)
        {
            Person person = Persons
                .FirstOrDefault(b => string.Equals(b.Forename + b.Surname, forename + surname, StringComparison.CurrentCultureIgnoreCase));
            
            if (person == null)
                return NotFound();
            
            Persons.Remove(person);
            return Ok(person);
        }
    }
}