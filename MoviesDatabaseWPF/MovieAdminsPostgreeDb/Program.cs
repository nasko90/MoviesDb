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
            var addMoviePermission = new Permission
            {
                Name = "Add movie"
            };

            var deleteMovie = new Permission
            {
                Name = "Delete Movie"
            };
            var movieAdminsContext = new MovieAdminsContext();
            var admin = new Admin()
            {
                UserName = "SuperAdmin",
                ClearTextPassword = "0000",
                Email = "admin@abv.bg",
                Permissions = {addMoviePermission,deleteMovie}
            };

            movieAdminsContext.Admins.Add(admin);
            movieAdminsContext.SaveChanges();
        }
    }
}
