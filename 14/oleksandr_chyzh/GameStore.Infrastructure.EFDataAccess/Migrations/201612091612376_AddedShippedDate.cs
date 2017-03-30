using System.Data.Entity.Migrations;

namespace GameStore.Infrastructure.EFDataAccess.Migrations
{   
    public partial class AddedShippedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PayingDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Orders", "ShippedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ShippedDate");
            DropColumn("dbo.Orders", "PayingDate");
        }
    }
}
