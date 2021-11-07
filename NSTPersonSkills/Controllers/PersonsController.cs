using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSTPersonSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSTPersonSkills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly CompanyContext db;

        public PersonsController(CompanyContext _db)
        {
            db = _db;
        }
        [HttpGet(Name = "persons")]

        public IEnumerable<Person> Get()
        {
            IEnumerable<Person> people = db.People.Include(p => p.Skills);
            return people;
        }
    }
}
