using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Models.Models
{
    public class Movie 
    {
        public Movie()
        {
            this.Genres = new HashSet<Genre>();
            this.Actors = new HashSet<Actor>();
            this.Countries = new HashSet<Country>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name must be betwen 2 and 100 characters"), MinLength(2)]
        public string Title { get; set; }

        [Required]
        public string Plot { get; set; }

        [Required]
        public int Duration { get; set; }

        [Range(1, 10, ErrorMessage = "The rating should be betwen 1 and 10")]
        [Required]
        public double ImdbRating { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        [Range(0, long.MaxValue, ErrorMessage = "Budget must be positive value")]
        public long BoxOffice { get; set; }              

        [Required]
        public virtual Director Director { get; set; }

        public virtual ICollection<Country> Countries { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }

        public virtual ICollection<Movie_User> Movies_Users { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Title: {this.Title}");
            sb.AppendLine($"Duration: {this.Duration} minutes");
            sb.AppendLine($"Genre: {string.Join(", ", this.Genres.Select(x => string.Concat(x.Name)))}");
            sb.AppendLine($"Director: {this.Director.Name}");
            sb.AppendLine($"Actros: {string.Join(",", this.Actors.Select(x => string.Concat(x.Name)))}");
            sb.AppendLine($"Box office: {this.BoxOffice}");
            return sb.ToString();
        }
    }
}
