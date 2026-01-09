namespace Eventplanner.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlobalConfigurationOfIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoomPlans", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.RoomPlans", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.RoomPlans", new[] { "Event_Id" });
            DropIndex("dbo.RoomPlans", new[] { "Room_Id" });
            RenameColumn(table: "dbo.Appointments", name: "Event_Id", newName: "EventId");
            RenameColumn(table: "dbo.Appointments", name: "Person_Id", newName: "PersonId");
            RenameColumn(table: "dbo.Rooms", name: "Institution_Id", newName: "InstitutionId");
            RenameColumn(table: "dbo.Institutions", name: "Address_Id", newName: "AddressId");
            RenameIndex(table: "dbo.Appointments", name: "IX_Person_Id", newName: "IX_PersonId");
            RenameIndex(table: "dbo.Appointments", name: "IX_Event_Id", newName: "IX_EventId");
            RenameIndex(table: "dbo.Rooms", name: "IX_Institution_Id", newName: "IX_InstitutionId");
            RenameIndex(table: "dbo.Institutions", name: "IX_Address_Id", newName: "IX_AddressId");
            AddColumn("dbo.Appointments", "Service", c => c.Int(nullable: false));
            AddColumn("dbo.TicketPrices", "PriceCategory", c => c.Int(nullable: false));
            DropColumn("dbo.Appointments", "Role");
            DropColumn("dbo.TicketPrices", "Type");
            DropTable("dbo.RoomPlans");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoomPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Event_Id = c.Int(),
                        Room_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TicketPrices", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Appointments", "Role", c => c.Int(nullable: false));
            DropColumn("dbo.TicketPrices", "PriceCategory");
            DropColumn("dbo.Appointments", "Service");
            RenameIndex(table: "dbo.Institutions", name: "IX_AddressId", newName: "IX_Address_Id");
            RenameIndex(table: "dbo.Rooms", name: "IX_InstitutionId", newName: "IX_Institution_Id");
            RenameIndex(table: "dbo.Appointments", name: "IX_EventId", newName: "IX_Event_Id");
            RenameIndex(table: "dbo.Appointments", name: "IX_PersonId", newName: "IX_Person_Id");
            RenameColumn(table: "dbo.Institutions", name: "AddressId", newName: "Address_Id");
            RenameColumn(table: "dbo.Rooms", name: "InstitutionId", newName: "Institution_Id");
            RenameColumn(table: "dbo.Appointments", name: "PersonId", newName: "Person_Id");
            RenameColumn(table: "dbo.Appointments", name: "EventId", newName: "Event_Id");
            CreateIndex("dbo.RoomPlans", "Room_Id");
            CreateIndex("dbo.RoomPlans", "Event_Id");
            AddForeignKey("dbo.RoomPlans", "Room_Id", "dbo.Rooms", "Id");
            AddForeignKey("dbo.RoomPlans", "Event_Id", "dbo.Events", "Id");
        }
    }
}
