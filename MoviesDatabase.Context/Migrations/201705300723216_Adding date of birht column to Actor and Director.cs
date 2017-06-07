namespace MoviesDatabase.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingdateofbirhtcolumntoActorandDirector : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "DateOfBirth", c => c.DateTime());
            AddColumn("dbo.Directors", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Directors", "DateOfBirth");
            DropColumn("dbo.Actors", "DateOfBirth");
        }
    }
}
