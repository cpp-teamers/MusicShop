namespace ModelsLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountRoleAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DiscountsPlates", "DiscountId", "dbo.Discounts");
            DropForeignKey("dbo.DiscountsPlates", "PlateId", "dbo.Plates");
            DropIndex("dbo.DiscountsPlates", new[] { "PlateId" });
            DropIndex("dbo.DiscountsPlates", new[] { "DiscountId" });
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 50),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Sales", "AccountId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sales", "AccountId");
            AddForeignKey("dbo.Sales", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
            DropTable("dbo.DiscountsPlates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DiscountsPlates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlateId = c.Int(nullable: false),
                        DiscountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Sales", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "RoleId", "dbo.Roles");
            DropIndex("dbo.Sales", new[] { "AccountId" });
            DropIndex("dbo.Accounts", new[] { "RoleId" });
            DropColumn("dbo.Sales", "AccountId");
            DropTable("dbo.Roles");
            DropTable("dbo.Accounts");
            CreateIndex("dbo.DiscountsPlates", "DiscountId");
            CreateIndex("dbo.DiscountsPlates", "PlateId");
            AddForeignKey("dbo.DiscountsPlates", "PlateId", "dbo.Plates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DiscountsPlates", "DiscountId", "dbo.Discounts", "Id", cascadeDelete: true);
        }
    }
}
