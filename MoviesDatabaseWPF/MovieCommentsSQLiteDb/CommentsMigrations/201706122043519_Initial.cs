namespace MovieCommentsSQLiteDb.CommentsMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentText = c.String(nullable: false, maxLength: 2147483647),
                        MovieTitle_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovieTitles", t => t.MovieTitle_Id, cascadeDelete: true)
                .Index(t => t.MovieTitle_Id);
            
            CreateTable(
                "dbo.MovieTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "MovieTitle_Id", "dbo.MovieTitles");
            DropIndex("dbo.Comments", new[] { "MovieTitle_Id" });
            DropTable("dbo.MovieTitles");
            DropTable("dbo.Comments");
        }
    }
}
