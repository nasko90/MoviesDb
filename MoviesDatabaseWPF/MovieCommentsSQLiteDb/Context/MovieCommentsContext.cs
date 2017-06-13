using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MovieCommentsSQLiteDb.Models;
using MovieCommentsSQLiteDb.CommentsMigrations;

namespace MovieCommentsSQLiteDb.Context
{
    public class MovieCommentsContext : DbContext
    {
        public MovieCommentsContext()
            : base("MovieComments")
        {
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<MovieCommentsContext, CommentsConfig>(true));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("public");
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<MovieTitle> MovieTitle { get; set; }
    }
}
