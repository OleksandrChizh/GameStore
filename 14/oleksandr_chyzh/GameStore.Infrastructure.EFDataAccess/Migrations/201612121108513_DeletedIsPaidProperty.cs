using System.Data.Entity.Migrations;

namespace GameStore.Infrastructure.EFDataAccess.Migrations
{   
    public partial class DeletedIsPaidProperty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "IsPaid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "IsPaid", c => c.Boolean(nullable: false));
        }
    }
}
