
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.PostgreeDatabase.Models
{
    [Table("Admins", Schema = "public")]
    public class Admin
    {
        public Admin()
        {
            this.Permissions = new HashSet<Permission>();
        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string ClearTextPassword
        {
            set { Password = MyHashMethod(value); }
        }

        [Required]
        public string Email { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }

        private static string MyHashMethod(string password)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
