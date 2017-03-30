using System.Data.Entity.Migrations;

namespace GameStore.Infrastructure.EFDataAccess.Migrations
{  
    public partial class DeletedSalt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Salt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Salt", c => c.String(nullable: false));
        }
    }
}
