namespace Eventplanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreetHouseNr = c.String(),
                        PostalCode = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        SubTitle = c.String(),
                        DateTimeFrom = c.DateTime(nullable: false),
                        DateTimeTo = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        TotalTickets = c.Int(nullable: false),
                        BookedTickets = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        TelephoneNumber = c.String(),
                        IsEmploee = c.Boolean(nullable: false),
                        Address_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
            CreateTable(
                "dbo.RoomPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Event_Id = c.Int(),
                        Room_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomNumber = c.String(),
                        AdressId = c.Int(nullable: false),
                        SeatsCapacity = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role = c.Int(nullable: false),
                        DateTimeFrom = c.DateTime(nullable: false),
                        DateTimeUntil = c.DateTime(nullable: false),
                        Event_Id = c.Int(),
                        Person_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBooked = c.Boolean(nullable: false),
                        Event_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Event_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Schedules", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Schedules", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.RoomPlans", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.RoomPlans", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.People", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Tickets", new[] { "Event_Id" });
            DropIndex("dbo.Schedules", new[] { "Person_Id" });
            DropIndex("dbo.Schedules", new[] { "Event_Id" });
            DropIndex("dbo.RoomPlans", new[] { "Room_Id" });
            DropIndex("dbo.RoomPlans", new[] { "Event_Id" });
            DropIndex("dbo.People", new[] { "Address_Id" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Schedules");
            DropTable("dbo.Rooms");
            DropTable("dbo.RoomPlans");
            DropTable("dbo.People");
            DropTable("dbo.Events");
            DropTable("dbo.Addresses");
        }
    }
}
