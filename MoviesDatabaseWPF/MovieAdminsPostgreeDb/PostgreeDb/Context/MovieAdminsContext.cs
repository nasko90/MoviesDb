using System.Data.Entity;
using MovieAdminsPostgreeDb.PostgreeDb.Models;
using MovieDatabase.PostgreeDatabase.Models;

namespace MovieAdminsPostgreeDb.PostgreeDb.Context
{
    public class MovieAdminsContext : DbContext
    {
        public MovieAdminsContext()
            :base("MovieAdmins")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("public");
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        
    }
}
