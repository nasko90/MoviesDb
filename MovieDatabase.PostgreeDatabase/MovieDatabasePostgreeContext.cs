using MovieDatabase.PostgreeDatabase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.PostgreeDatabase
{
    public class MovieDatabasePostgreeContext : DbContext
    {
        public MovieDatabasePostgreeContext()
           : base ("Movie_Admins")
        {

        }
       
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Map to the correct Database tables
            modelBuilder.Entity<Admin>().ToTable("Admins","public");
            modelBuilder.Entity<Permission>().ToTable("Permissions","public");

            // Database for PostgreSQL doesn't auto-increment Ids
            modelBuilder.Conventions
                .Remove<StoreGeneratedIdentityKeyConvention>();
        }
    }
}
