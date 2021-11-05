using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace NSTPersonSkills.Models
{
    public partial class Skill
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите уровень навыка")]
        [Range(1, 10, ErrorMessage = "Уровень навыка должен находиться в диапазоне от 1 до 10")]
        public byte Level { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? PersonId { get; set; }

        public virtual Person Person { get; set; }
    }
}
