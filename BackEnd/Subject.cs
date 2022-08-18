using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.BackEnd
{
    public class Subject
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(20)]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [MinLength(3)]
        public string Teacher { get; set; }
        
        [Required]
        public int SpecializationID { get; set; }

        [ForeignKey("SpecializationID")]
        public virtual Specialization Specialization { get; set; }
    }
}
