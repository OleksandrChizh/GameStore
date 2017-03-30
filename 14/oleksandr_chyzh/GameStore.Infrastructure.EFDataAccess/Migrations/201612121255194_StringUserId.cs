using System.Data.Entity.Migrations;

namespace GameStore.Infrastructure.EFDataAccess.Migrations
{    
    public partial class StringUserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoles", "User_Id", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "User_Id" });
            RenameColumn(table: "dbo.UserRoles", name: "User_Id", newName: "User_UserId");
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.UserRoles");
            AddColumn("dbo.Users", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Orders", "CustomerId", c => c.String(nullable: false));
            AlterColumn("dbo.UserRoles", "User_UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Users", "UserId");
            AddPrimaryKey("dbo.UserRoles", new[] { "User_UserId", "Role_Id" });
            CreateIndex("dbo.UserRoles", "User_UserId");
            AddForeignKey("dbo.UserRoles", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            DropColumn("dbo.Users", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.UserRoles", "User_UserId", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "User_UserId" });
            DropPrimaryKey("dbo.UserRoles");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.UserRoles", "User_UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "UserId");
            AddPrimaryKey("dbo.UserRoles", new[] { "User_Id", "Role_Id" });
            AddPrimaryKey("dbo.Users", "Id");
            RenameColumn(table: "dbo.UserRoles", name: "User_UserId", newName: "User_Id");
            CreateIndex("dbo.UserRoles", "User_Id");
            AddForeignKey("dbo.UserRoles", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
