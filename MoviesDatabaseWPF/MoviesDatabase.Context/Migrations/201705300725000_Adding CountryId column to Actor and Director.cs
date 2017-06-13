namespace MoviesDatabase.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCountryIdcolumntoActorandDirector : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CountryMovies", newName: "MovieCountries");
            DropPrimaryKey("dbo.MovieCountries");
            AddColumn("dbo.Actors", "Country_Id", c => c.Int());
            AddColumn("dbo.Directors", "Country_Id", c => c.Int());
            AddPrimaryKey("dbo.MovieCountries", new[] { "Movie_Id", "Country_Id" });
            CreateIndex("dbo.Actors", "Country_Id");
            CreateIndex("dbo.Directors", "Country_Id");
            AddForeignKey("dbo.Directors", "Country_Id", "dbo.Countries", "Id");
            AddForeignKey("dbo.Actors", "Country_Id", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Actors", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Directors", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Directors", new[] { "Country_Id" });
            DropIndex("dbo.Actors", new[] { "Country_Id" });
            DropPrimaryKey("dbo.MovieCountries");
            DropColumn("dbo.Directors", "Country_Id");
            DropColumn("dbo.Actors", "Country_Id");
            AddPrimaryKey("dbo.MovieCountries", new[] { "Country_Id", "Movie_Id" });
            RenameTable(name: "dbo.MovieCountries", newName: "CountryMovies");
        }
    }
}
