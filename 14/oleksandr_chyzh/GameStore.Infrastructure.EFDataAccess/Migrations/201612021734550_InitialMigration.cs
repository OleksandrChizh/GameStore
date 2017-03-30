using System.Data.Entity.Migrations;

namespace GameStore.Infrastructure.EFDataAccess.Migrations
{    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        IsQuote = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Game_Id = c.Int(),
                        ParentComment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .ForeignKey("dbo.Comments", t => t.ParentComment_Id)
                .Index(t => t.Game_Id)
                .Index(t => t.ParentComment_Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 200),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitsInStock = c.Short(nullable: false),
                        Discounted = c.Boolean(nullable: false),
                        ViewsCount = c.Int(nullable: false),
                        AddingDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PublishingDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true, name: "UniqueGameKey");
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Deleted = c.Boolean(nullable: false),
                        ParentGenre_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.ParentGenre_Id)
                .Index(t => t.Name, unique: true, name: "UniqueGenreName")
                .Index(t => t.ParentGenre_Id);
            
            CreateTable(
                "dbo.PlatformTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 200),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Type, unique: true, name: "UniquePlatformeType");
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 200),
                        Description = c.String(),
                        HomePage = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CompanyName, unique: true, name: "UniqueCompanyName");
            
            CreateTable(
                "dbo.PublisherTranslations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Language = c.String(nullable: false, maxLength: 2),
                        Deleted = c.Boolean(nullable: false),
                        Publisher_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publishers", t => t.Publisher_Id, cascadeDelete: true)
                .Index(t => t.Publisher_Id);
            
            CreateTable(
                "dbo.GameTranslations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Language = c.String(nullable: false, maxLength: 2),
                        Deleted = c.Boolean(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Short(nullable: false),
                        Discount = c.Single(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsPaid = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameGenres",
                c => new
                    {
                        Game_Id = c.Int(nullable: false),
                        Genre_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Game_Id, t.Genre_Id })
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .Index(t => t.Game_Id)
                .Index(t => t.Genre_Id);
            
            CreateTable(
                "dbo.GamePlatformTypes",
                c => new
                    {
                        Game_Id = c.Int(nullable: false),
                        PlatformType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Game_Id, t.PlatformType_Id })
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .ForeignKey("dbo.PlatformTypes", t => t.PlatformType_Id, cascadeDelete: true)
                .Index(t => t.Game_Id)
                .Index(t => t.PlatformType_Id);
            
            CreateTable(
                "dbo.PublisherGames",
                c => new
                    {
                        Publisher_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Publisher_Id, t.Game_Id })
                .ForeignKey("dbo.Publishers", t => t.Publisher_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.Publisher_Id)
                .Index(t => t.Game_Id);         
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Comments", "ParentComment_Id", "dbo.Comments");
            DropForeignKey("dbo.GameTranslations", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PublisherTranslations", "Publisher_Id", "dbo.Publishers");
            DropForeignKey("dbo.PublisherGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PublisherGames", "Publisher_Id", "dbo.Publishers");
            DropForeignKey("dbo.GamePlatformTypes", "PlatformType_Id", "dbo.PlatformTypes");
            DropForeignKey("dbo.GamePlatformTypes", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.GameGenres", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.GameGenres", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Genres", "ParentGenre_Id", "dbo.Genres");
            DropForeignKey("dbo.Comments", "Game_Id", "dbo.Games");
            DropIndex("dbo.PublisherGames", new[] { "Game_Id" });
            DropIndex("dbo.PublisherGames", new[] { "Publisher_Id" });
            DropIndex("dbo.GamePlatformTypes", new[] { "PlatformType_Id" });
            DropIndex("dbo.GamePlatformTypes", new[] { "Game_Id" });
            DropIndex("dbo.GameGenres", new[] { "Genre_Id" });
            DropIndex("dbo.GameGenres", new[] { "Game_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropIndex("dbo.GameTranslations", new[] { "Game_Id" });
            DropIndex("dbo.PublisherTranslations", new[] { "Publisher_Id" });
            DropIndex("dbo.Publishers", "UniqueCompanyName");
            DropIndex("dbo.PlatformTypes", "UniquePlatformeType");
            DropIndex("dbo.Genres", new[] { "ParentGenre_Id" });
            DropIndex("dbo.Genres", "UniqueGenreName");
            DropIndex("dbo.Games", "UniqueGameKey");
            DropIndex("dbo.Comments", new[] { "ParentComment_Id" });
            DropIndex("dbo.Comments", new[] { "Game_Id" });
            DropTable("dbo.PublisherGames");
            DropTable("dbo.GamePlatformTypes");
            DropTable("dbo.GameGenres");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.GameTranslations");
            DropTable("dbo.PublisherTranslations");
            DropTable("dbo.Publishers");
            DropTable("dbo.PlatformTypes");
            DropTable("dbo.Genres");
            DropTable("dbo.Games");
            DropTable("dbo.Comments");
        }
    }
}
