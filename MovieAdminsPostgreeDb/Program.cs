using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieAdminsPostgreeDb.PostgreeDb.Context;
using MovieAdminsPostgreeDb.PostgreeDb.Models;
using MovieDatabase.PostgreeDatabase.Models;

namespace MovieAdminsPostgreeDb
{
    public class Program
    {
        static void Main(string[] args)
        {
            var movieAdminsContext = new MovieAdminsContext();
            var admin = new Admin()
            {
                UserName = "SuperAdmin",
                ClearTextPassword = "0000",
                Email = "admin@abv.bg",
                Permissions = {movieAdminsContext.Permissions.Find(1), movieAdminsContext.Permissions.Find(2)}
            };

            movieAdminsContext.Admins.Add(admin);
            movieAdminsContext.SaveChanges();
        }
    }
}
