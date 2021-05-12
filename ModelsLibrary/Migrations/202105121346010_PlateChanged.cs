namespace ModelsLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlateChanged : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Authors", newName: "Actors");
            AddColumn("dbo.Plates", "AlbumImagePath", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Sales", "Price", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Discounts", "Comment", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Plates", "Cost", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Tracks", "Duration", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Plates", "RealCost");
            DropColumn("dbo.Sales", "FinalCost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "FinalCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Plates", "RealCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Tracks", "Duration", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Plates", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Discounts", "Comment", c => c.String(maxLength: 50));
            DropColumn("dbo.Sales", "Price");
            DropColumn("dbo.Plates", "AlbumImagePath");
            RenameTable(name: "dbo.Actors", newName: "Authors");
        }
    }
}
