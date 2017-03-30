using System.Data.Entity.Migrations;

namespace GameStore.Infrastructure.EFDataAccess.Migrations
{
    public partial class AuthMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UniqueRoleName");
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        PasswordHash = c.String(nullable: false),
                        BanExpiryDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UniqueUserName");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Role_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Role_Id);         
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_Id", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "Role_Id" });
            DropIndex("dbo.UserRoles", new[] { "User_Id" });
            DropIndex("dbo.Users", "UniqueUserName");
            DropIndex("dbo.Roles", "UniqueRoleName");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
        }
    }
}
