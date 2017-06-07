using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.PostgreeDatabase.Models
{
    [Table("Permissions", Schema = "public")]
    public class Permission
    {
        public Permission()
        {
            this.Admins = new HashSet<Admin>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Admin> Admins { get; set; }
    }
}