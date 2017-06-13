namespace MoviesDatabase.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Plot = c.String(nullable: false),
                        Duration = c.Int(nullable: false),
                        ImdbRating = c.Double(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        BoxOffice = c.Decimal(nullable: false, storeType: "money"),
                        Director_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Directors", t => t.Director_Id, cascadeDelete: true)
                .Index(t => t.Director_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movie_User",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        beenWatched = c.Boolean(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.UserId })
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 25),
                        HashedPassword = c.String(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 50),
                        EmailAddress = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieActors",
                c => new
                    {
                        Movie_Id = c.Int(nullable: false),
                        Actor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_Id, t.Actor_Id })
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.Actor_Id, cascadeDelete: true)
                .Index(t => t.Movie_Id)
                .Index(t => t.Actor_Id);
            
            CreateTable(
                "dbo.CountryMovies",
                c => new
                    {
                        Country_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Country_Id, t.Movie_Id })
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Country_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.GenreMovies",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Movie_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movie_User", "UserId", "dbo.Users");
            DropForeignKey("dbo.Movie_User", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.GenreMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.GenreMovies", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.Movies", "Director_Id", "dbo.Directors");
            DropForeignKey("dbo.CountryMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.CountryMovies", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.MovieActors", "Actor_Id", "dbo.Actors");
            DropForeignKey("dbo.MovieActors", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.GenreMovies", new[] { "Movie_Id" });
            DropIndex("dbo.GenreMovies", new[] { "Genre_Id" });
            DropIndex("dbo.CountryMovies", new[] { "Movie_Id" });
            DropIndex("dbo.CountryMovies", new[] { "Country_Id" });
            DropIndex("dbo.MovieActors", new[] { "Actor_Id" });
            DropIndex("dbo.MovieActors", new[] { "Movie_Id" });
            DropIndex("dbo.Movie_User", new[] { "UserId" });
            DropIndex("dbo.Movie_User", new[] { "MovieId" });
            DropIndex("dbo.Movies", new[] { "Director_Id" });
            DropTable("dbo.GenreMovies");
            DropTable("dbo.CountryMovies");
            DropTable("dbo.MovieActors");
            DropTable("dbo.Users");
            DropTable("dbo.Movie_User");
            DropTable("dbo.Genres");
            DropTable("dbo.Directors");
            DropTable("dbo.Countries");
            DropTable("dbo.Movies");
            DropTable("dbo.Actors");
        }
    }
}
