using System;
using System.Collections.Generic;

#nullable disable

namespace NSTPersonSkills.Models
{
    public partial class Person
    {
        public Person()
        {
            Skills = new HashSet<Skill>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}
