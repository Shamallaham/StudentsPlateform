using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.BackEnd
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Nickname { get; set; }

        [Required]
        [MinLength(3)]
        public string Mother { get; set; }

        [Required]
        [MinLength(3)]
        public string Father { get; set; }

        [Required]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        
        [Required]
        public int SpecializationID { get; set; }

        [ForeignKey("SpecializationID")]
        public virtual Specialization Specialization { get; set; }
        
        [Required]
        [MinLength(7)]
        [MaxLength(10)]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        
    }
}
