using System.Data.Entity.Migrations;

namespace GameStore.Infrastructure.EFDataAccess.Migrations
{   
    public partial class AddedSalt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Salt", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Salt");
        }
    }
}
