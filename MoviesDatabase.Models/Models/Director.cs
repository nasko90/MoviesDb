using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Models.Models
{
    public class Director : Person
    {

        public Director()
        {
            this.Movies = new HashSet<Movie>();
        }
        public virtual IEnumerable<Movie> Movies { get; set; }
    }
}
