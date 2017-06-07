namespace MoviesDatabase.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingcolumnGendertoActorsandDirectors : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.Directors", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Directors", "Gender");
            DropColumn("dbo.Actors", "Gender");
        }
    }
}
