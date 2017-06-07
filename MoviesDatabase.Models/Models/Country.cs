using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Models.Models
{
    public class Country
    {
        public Country()
        {
            this.Actors = new HashSet<Actor>();
            this.Movies = new HashSet<Movie>();
            this.Directors = new HashSet<Director>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required]
        [MaxLength(50, ErrorMessage = "Country name must be betwen 2 and 50 characters"), MinLength(2)]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

        public IEnumerable<Actor> Actors { get; set; }

        public IEnumerable<Director> Directors { get; set; }

    }
}
