using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Models.Models
{
    public class Genre
    {
        public Genre()
        {
            this.Movies = new HashSet<Movie>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Genre name must be betwen 2 and 50 characters"), MinLength(2)]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

    }
}
