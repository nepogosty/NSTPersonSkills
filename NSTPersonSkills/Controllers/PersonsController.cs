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
        CompanyContext db = new CompanyContext();
        [HttpGet(Name = "persons")]

        public IEnumerable<Person> Get()
        {
            IEnumerable<Person> people = db.People.Include(p => p.Skills);
            return people;
        }
    }
}
