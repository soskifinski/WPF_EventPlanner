namespace Eventplanner.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventRoom1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Events", name: "Room_Id", newName: "RoomId");
            RenameIndex(table: "dbo.Events", name: "IX_Room_Id", newName: "IX_RoomId");
            AddColumn("dbo.Events", "StatusId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "StatusId");
            RenameIndex(table: "dbo.Events", name: "IX_RoomId", newName: "IX_Room_Id");
            RenameColumn(table: "dbo.Events", name: "RoomId", newName: "Room_Id");
        }
    }
}
