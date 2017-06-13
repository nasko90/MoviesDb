namespace MoviesDatabase.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makinguser_moviesratingandisWatchedcolumsnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movie_User", "beenWatched", c => c.Boolean());
            AlterColumn("dbo.Movie_User", "Rating", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movie_User", "Rating", c => c.Int(nullable: false));
            AlterColumn("dbo.Movie_User", "beenWatched", c => c.Boolean(nullable: false));
        }
    }
}
