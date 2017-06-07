namespace MovieDatabase.PostgreeDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Admins", newSchema: "public");
            MoveTable(name: "dbo.Permissions", newSchema: "public");
            AlterColumn("public.Admins", "UserName", c => c.String(nullable: false));
            AlterColumn("public.Admins", "Password", c => c.String(nullable: false));
            AlterColumn("public.Admins", "Email", c => c.String(nullable: false));
            AlterColumn("public.Permissions", "Name", c => c.String(nullable: false));
            CreateIndex("public.Admins", "UserName", unique: true);
            CreateIndex("public.Permissions", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("public.Permissions", new[] { "Name" });
            DropIndex("public.Admins", new[] { "UserName" });
            AlterColumn("public.Permissions", "Name", c => c.String());
            AlterColumn("public.Admins", "Email", c => c.String());
            AlterColumn("public.Admins", "Password", c => c.String());
            AlterColumn("public.Admins", "UserName", c => c.String());
            MoveTable(name: "public.Permissions", newSchema: "dbo");
            MoveTable(name: "public.Admins", newSchema: "dbo");
        }
    }
}
