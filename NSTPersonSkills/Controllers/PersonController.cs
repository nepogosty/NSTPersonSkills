using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSTPersonSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NSTPersonSkills.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        CompanyContext db=new CompanyContext();
        [HttpGet(Name = "GetAllItems")]
    
        public IEnumerable<Person> Get()
        {
            IEnumerable<Person> people = db.People.Include(p=>p.Skills);
            return people;
        }

        
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            Person person = db.People.Find(id);

            if (person == null)
            {
                return NotFound();
            }
            IEnumerable<Person> people = db.People.Include(p => p.Skills);
            var item= people.Where(q => q.Id == id);

            return Ok(item);
        }

     
        [HttpPost]
        public IActionResult PostPerson([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            if (db.People.Any(x => x.Id == person.Id))
            {
                
                return BadRequest();
            }
            foreach(var update in person.Skills)
            {
                update.PersonId = person.Id;
            }
            db.People.Add(person);
            db.SaveChanges();
            return Ok(person);
        }

        
        [HttpPut("{id}")]
        public IActionResult PutPerson(long id,[FromBody] Person updatedPersonItem)
        {
            if (updatedPersonItem == null)
            {
                return BadRequest();
            }
            if (!db.People.Any(x => x.Id == id))
            {
                return NotFound();
            }
            Person currentItem = db.People.Include(q => q.Skills).FirstOrDefault(w => w.Id == Convert.ToInt32(id));
            currentItem.Name = updatedPersonItem.Name;
            currentItem.DisplayName = updatedPersonItem.DisplayName;
            foreach (var skill in updatedPersonItem.Skills)
            {
                Skill updatedSkill = db.Skills.FirstOrDefault(q => q.Name == skill.Name && q.PersonId == id);
                if (updatedSkill == null)
                {
                    Skill newSkill = new Skill() { PersonId = id, Name = skill.Name, Level = skill.Level, Person = currentItem };
                    currentItem.Skills.Add(newSkill);
                    db.Skills.Add(newSkill);
                    continue;
                }
                else
                {
                    updatedSkill.Level = skill.Level;
                    db.Skills.Update(updatedSkill);
                }
            }

            db.SaveChanges();
            return Ok(currentItem);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Person person = db.People.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            db.SaveChanges();
            return Ok(person);

        }
    }
}
