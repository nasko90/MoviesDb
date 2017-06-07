namespace MovieDatabase.PostgreeDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissionAdmins",
                c => new
                    {
                        Permission_Id = c.Int(nullable: false),
                        Admin_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Permission_Id, t.Admin_Id })
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.Admins", t => t.Admin_Id, cascadeDelete: true)
                .Index(t => t.Permission_Id)
                .Index(t => t.Admin_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermissionAdmins", "Admin_Id", "dbo.Admins");
            DropForeignKey("dbo.PermissionAdmins", "Permission_Id", "dbo.Permissions");
            DropIndex("dbo.PermissionAdmins", new[] { "Admin_Id" });
            DropIndex("dbo.PermissionAdmins", new[] { "Permission_Id" });
            DropTable("dbo.PermissionAdmins");
            DropTable("dbo.Permissions");
            DropTable("dbo.Admins");
        }
    }
}
