using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Models.Models
{
    public class Movie_User
    {
        [Key, Column(Order = 0)]
        public int MovieId { get; set; }
        [Key, Column(Order = 1)]
        public int UserId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }

        public bool? beenWatched { get; set; }

        [Range(1, 10, ErrorMessage = "The rating should be betwen 1 and 10")]
        public int? Rating { get; set; }

    }
}
