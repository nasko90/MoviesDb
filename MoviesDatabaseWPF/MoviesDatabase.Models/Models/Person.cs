using MoviesDatabase.Models.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Models.Models
{
    public abstract class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [MaxLength(50, ErrorMessage = "Name must be betwen 2 and 25 characters"), MinLength(2)]
        public string Name { get; set; }
     

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public virtual Country Country { get; set; }

        public virtual Gender Gender { get; set; }

    }
}
