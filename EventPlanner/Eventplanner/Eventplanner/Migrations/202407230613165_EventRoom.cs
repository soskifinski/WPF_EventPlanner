namespace Eventplanner.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventRoom : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Events", new[] { "Address_Id" });
            AddColumn("dbo.Events", "Room_Id", c => c.Int());
            CreateIndex("dbo.Events", "Room_Id");
            AddForeignKey("dbo.Events", "Room_Id", "dbo.Rooms", "Id");
            DropColumn("dbo.Events", "Address_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Address_Id", c => c.Int());
            DropForeignKey("dbo.Events", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.Events", new[] { "Room_Id" });
            DropColumn("dbo.Events", "Room_Id");
            CreateIndex("dbo.Events", "Address_Id");
            AddForeignKey("dbo.Events", "Address_Id", "dbo.Addresses", "Id");
        }
    }
}
