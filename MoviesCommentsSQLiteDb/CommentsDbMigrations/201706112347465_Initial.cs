namespace MoviesCommentsSQLiteDb.CommentsDbMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentText = c.String(nullable: false, maxLength: 2147483647),
                        MovieTitle_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.MovieTitles", t => t.MovieTitle_Id, cascadeDelete: true)
                .Index(t => t.MovieTitle_Id);
            
            CreateTable(
                "public.MovieTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.Comments", "MovieTitle_Id", "public.MovieTitles");
            DropIndex("public.Comments", new[] { "MovieTitle_Id" });
            DropTable("public.MovieTitles");
            DropTable("public.Comments");
        }
    }
}
