using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.BackEnd
{
    public class Specialization
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Specialization")]
        public string Name { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
