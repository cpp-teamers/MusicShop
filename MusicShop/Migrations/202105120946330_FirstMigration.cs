namespace ModelsLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Percent = c.Int(nullable: false),
                        Comment = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DiscountsPlates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlateId = c.Int(nullable: false),
                        DiscountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Discounts", t => t.DiscountId, cascadeDelete: true)
                .ForeignKey("dbo.Plates", t => t.PlateId, cascadeDelete: true)
                .Index(t => t.PlateId)
                .Index(t => t.DiscountId);
            
            CreateTable(
                "dbo.Plates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Amount = c.Int(nullable: false),
                        PublishDate = c.DateTime(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RealCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AuthorId = c.Int(nullable: false),
                        PublisherId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Publishers", t => t.PublisherId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.PublisherId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlateId = c.Int(nullable: false),
                        AmountOfSales = c.Int(nullable: false),
                        DateOfSale = c.DateTime(nullable: false),
                        FinalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Plates", t => t.PlateId, cascadeDelete: true)
                .Index(t => t.PlateId);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Duration = c.DateTime(nullable: false),
                        PlateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Plates", t => t.PlateId, cascadeDelete: true)
                .Index(t => t.PlateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tracks", "PlateId", "dbo.Plates");
            DropForeignKey("dbo.Sales", "PlateId", "dbo.Plates");
            DropForeignKey("dbo.DiscountsPlates", "PlateId", "dbo.Plates");
            DropForeignKey("dbo.Plates", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.Plates", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Plates", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.DiscountsPlates", "DiscountId", "dbo.Discounts");
            DropIndex("dbo.Tracks", new[] { "PlateId" });
            DropIndex("dbo.Sales", new[] { "PlateId" });
            DropIndex("dbo.Plates", new[] { "GenreId" });
            DropIndex("dbo.Plates", new[] { "PublisherId" });
            DropIndex("dbo.Plates", new[] { "AuthorId" });
            DropIndex("dbo.DiscountsPlates", new[] { "DiscountId" });
            DropIndex("dbo.DiscountsPlates", new[] { "PlateId" });
            DropTable("dbo.Tracks");
            DropTable("dbo.Sales");
            DropTable("dbo.Publishers");
            DropTable("dbo.Genres");
            DropTable("dbo.Plates");
            DropTable("dbo.DiscountsPlates");
            DropTable("dbo.Discounts");
            DropTable("dbo.Authors");
        }
    }
}
