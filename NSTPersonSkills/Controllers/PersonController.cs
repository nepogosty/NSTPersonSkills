using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSTPersonSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSTPersonSkills.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly CompanyContext db;

        public PersonController(CompanyContext _db)
        {
            db = _db;
        }
        //[HttpGet(Name = "persons")]

        //public IEnumerable<Person> Get()
        //{
        //    IEnumerable<Person> people = db.People.Include(p => p.Skills);
        //    return people;
        //}

        //CompanyContext db = new CompanyContext();

        //Вывод записи по ID
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            Person person = db.People.Find(id);

            if (person == null)
            {
                return NotFound();
            }
            IEnumerable<Person> people = db.People.Include(p => p.Skills);
            var item = people.Where(q => q.Id == id);

            return Ok(item);
        }

        //Создание записи
        [HttpPost]
        public IActionResult PostPerson([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            Person finished = new Person();
            finished.Id = person.Id;
            finished.Name = person.Name;
            finished.DisplayName = person.DisplayName;

            //поиск максимального ID в БД
            //var result = from item in db.People
            //                orderby item.Id descending
            //                select item;
            var result = db.People.OrderByDescending(x => x.Id);
            try
            {
                //Присвоение максимального id + 1 
                finished.Id = result.First().Id + 1;
            }
            catch {
                //Если нет в БД записей, то id+1
                finished.Id = 1; 
            }
               
            //Поиск записей-дубликатов скиллов и их устранение
            foreach (var update in person.Skills.GroupBy(p=>p.Name).Select(g=>g.FirstOrDefault()).Distinct())
            {
                update.PersonId = (long)finished.Id;
                finished.Skills.Add(update);
            }
            db.People.Add(finished);
            db.SaveChanges();
            return Ok(finished);
      
        }

        //Редактирование записи
        [HttpPut("{id}")]
        public IActionResult PutPerson(long id, [FromBody] Person updatedPersonItem)
        {
            if (updatedPersonItem == null)
            {
                return BadRequest();
            }
            if (!db.People.Any(x => x.Id == id))
            {
                return NotFound();
            }

            //Обновление записи на значения из тела запроса
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
            //Удаление соответстующих записей из таблицы Skill для предотвращения ошибки 
            var skills = db.Skills.Where(t => t.PersonId == id);
            foreach(var item in skills)
            {
                db.Skills.Remove(item);
            }
            
            db.SaveChanges();
            //Удаление записи из таблицы Person
            db.People.Remove(person);
            db.SaveChanges();
            return Ok(person);

        }
    }
}
