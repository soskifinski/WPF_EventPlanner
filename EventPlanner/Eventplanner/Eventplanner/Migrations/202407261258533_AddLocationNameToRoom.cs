namespace Eventplanner.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocationNameToRoom : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rooms", name: "InstitutionId", newName: "LocationId");
            RenameIndex(table: "dbo.Rooms", name: "IX_InstitutionId", newName: "IX_LocationId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Rooms", name: "IX_LocationId", newName: "IX_InstitutionId");
            RenameColumn(table: "dbo.Rooms", name: "LocationId", newName: "InstitutionId");
        }
    }
}
