using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Models.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "UserName must be betwen 2 and 25 characters"), MinLength(2)]
        public string UserName { get; set; }

        [Required]
        public string HashedPassword { get; set; }
        public string ClearTextPassword
        {
            set { HashedPassword = MyHashMethod(value); }
        }

        [Required]
        [MaxLength(50, ErrorMessage = "Name must be betwen 2 and 50 characters"), MinLength(2)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100, ErrorMessage = "Email addres must be betwen 5 and 100 characters"), MinLength(5)]
        public string EmailAddress { get; set; }

        public static string MyHashMethod(string password)
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
