using System.Collections.Generic;

namespace MoviesDatabase.Models.Models
{
    public class Actor : Person
    {
        public Actor ()
        {
            this.Movies = new HashSet<Movie>();
        }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
