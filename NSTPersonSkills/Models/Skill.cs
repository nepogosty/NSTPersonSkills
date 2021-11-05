using System;
using System.Collections.Generic;

#nullable disable

namespace NSTPersonSkills.Models
{
    public partial class Skill
    {
        public string Name { get; set; }
        public byte Level { get; set; }
        public long PersonId { get; set; }

        public virtual Person Person { get; set; }
    }
}
