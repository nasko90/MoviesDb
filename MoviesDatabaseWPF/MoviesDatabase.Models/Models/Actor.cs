using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
