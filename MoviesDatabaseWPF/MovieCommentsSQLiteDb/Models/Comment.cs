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
    [Table("Comments")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        //[MaxLength(100, ErrorMessage = "Comment must be betwen 2 and 100 characters"), MinLength(2)]
        //ntext
        public string CommentText { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Movie title must be betwen 2 and 100 characters"), MinLength(2)]
        public MovieTitle MovieTitle { get; set; }
    }
}
