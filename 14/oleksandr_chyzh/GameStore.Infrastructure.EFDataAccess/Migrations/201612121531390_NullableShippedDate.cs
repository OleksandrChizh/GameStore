using System.Data.Entity.Migrations;

namespace GameStore.Infrastructure.EFDataAccess.Migrations
{   
    public partial class NullableShippedDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "ShippedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "ShippedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
