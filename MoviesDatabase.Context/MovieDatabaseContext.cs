using MoviesDatabase.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Context
{
    public class MovieDatabaseContext : DbContext
    {
        public MovieDatabaseContext()
            : base("MovieDb")
        {
          
        }
       
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
            
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //}

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Movie_User> Movies_Users { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
