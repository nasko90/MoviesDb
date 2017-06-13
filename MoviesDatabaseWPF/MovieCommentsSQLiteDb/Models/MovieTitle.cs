using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;

namespace MovieCommentsSQLiteDb.Models
{
    [Table("MovieTitles")]
    public class MovieTitle
    {
        public MovieTitle()
        {
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Title must be betwen 2 and 100 characters"), MinLength(2)]
        public string Title { get; set; }

        [Required]
        public ICollection<Comment> Comments { get; set; }

    }
}
