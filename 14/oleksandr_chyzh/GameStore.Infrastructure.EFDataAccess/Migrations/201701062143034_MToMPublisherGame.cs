using System.Data.Entity.Migrations;

namespace GameStore.Infrastructure.EFDataAccess.Migrations
{
    public partial class MToMPublisherGame : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PublisherGames", newName: "GamePublishers");
            DropPrimaryKey("dbo.GamePublishers");
            AddPrimaryKey("dbo.GamePublishers", new[] { "Game_Id", "Publisher_Id" });
        }

        public override void Down()
        {
            DropPrimaryKey("dbo.GamePublishers");
            AddPrimaryKey("dbo.GamePublishers", new[] { "Publisher_Id", "Game_Id" });
            RenameTable(name: "dbo.GamePublishers", newName: "PublisherGames");
        }
    }
}
