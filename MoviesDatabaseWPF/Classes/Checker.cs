using MoviesDatabase.Context;
using MoviesDatabase.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieAdminsPostgreeDb.PostgreeDb.Context;
using MovieDatabase.PostgreeDatabase.Models;


namespace MoviesDatabaseWPF
{
    public class Checker
    {
        private readonly MovieDatabaseContext movieDatabase;
        private readonly MovieAdminsContext adminDatabase;

        public Checker(MovieDatabaseContext movieDatabase)
        {
            this.movieDatabase = movieDatabase;
        }

        public Checker(MovieAdminsContext adminDatabase)
        {
            this.adminDatabase = adminDatabase;
        }

        public User CheckUser(string userName)
        {
            var user = movieDatabase.User.FirstOrDefault(x => x.UserName.Equals(userName));
            return user;
        }

        public Admin CheckAdmin(string adminName)
        {
            var admin = this.adminDatabase.Admins.FirstOrDefault(x => x.UserName.Equals(adminName));
            return admin;
        }

        public bool IsUserPassCorrect(string password, User user)
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

        public bool IsAdminPassCorrect(string password, Admin admin)
        {
            if (admin == null)
            {
                return false;
            }

            var hashingPass = User.MyHashMethod(password);
            var isPasscorrect = admin.Password == hashingPass;

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
