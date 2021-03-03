using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Database;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly List<Person> _persons;

        public PersonController()
        {
            _persons = SeedData.Persons;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            return _persons;
        }
        
        [HttpGet("{forename}")]
        public ActionResult<IEnumerable<Person>> Get(string forename)
        {
            var persons = _persons
                .Where(b => string.Equals(b.Forename, forename, StringComparison.CurrentCultureIgnoreCase));
            
            if (!persons.Any())
                return NotFound();
            
            return new ObjectResult(persons);
        }
        
        [HttpPost]
        public ActionResult<Person> Post(Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _persons.Add(person);
            return new ObjectResult(_persons);;
        }
        
        [HttpDelete("{forename}/{surname}")]
        public ActionResult<Person> Delete(string forename, string surname)
        {
            Person person = _persons
                .FirstOrDefault(b => string.Equals(b.Forename + b.Surname, forename + surname, StringComparison.CurrentCultureIgnoreCase));
            
            if (person == null)
                return NotFound();
            
            _persons.Remove(person);
            return Ok(person);
        }
    }
}