namespace ModelsLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActorsAuthors : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Actors", newName: "Authors");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Authors", newName: "Actors");
        }
    }
}
