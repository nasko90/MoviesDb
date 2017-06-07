using MoviesDatabase.Context;
using MoviesDatabase.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabaseWPF
{
    public class Checker
    {
        private readonly MovieDatabaseContext db;

        public Checker(MovieDatabaseContext db)
        {
            this.db = db;
        }

        public User CheckUser(string userName)
        {
            var user = db.User.FirstOrDefault(x => x.UserName.Equals(userName));

            return user;
        }

        public bool IsPassCorrect(string password, User user)
        {
            if (user == null)
            {
                return false;
            }

            var hashingPass = User.MyHashMethod(password);
            var isPasscorrect = user.HashedPassword == hashingPass;

            if (isPasscorrect)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
