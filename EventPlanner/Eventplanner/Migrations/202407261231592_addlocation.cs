namespace Eventplanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlocation : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Institutions", newName: "Locations");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Locations", newName: "Institutions");
        }
    }
}
