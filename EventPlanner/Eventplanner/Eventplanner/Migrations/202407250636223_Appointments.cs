namespace Eventplanner.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Appointments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedules", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Schedules", "Person_Id", "dbo.People");
            DropIndex("dbo.Schedules", new[] { "Event_Id" });
            DropIndex("dbo.Schedules", new[] { "Person_Id" });
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role = c.Int(nullable: false),
                        AppointmentStart = c.DateTime(nullable: false),
                        AppointmentEnd = c.DateTime(nullable: false),
                        Event_Id = c.Int(),
                        Person_Id = c.Int(),
                        Schedule_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .ForeignKey("dbo.Schedules", t => t.Schedule_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Person_Id)
                .Index(t => t.Schedule_Id);
            
            DropColumn("dbo.Schedules", "Role");
            DropColumn("dbo.Schedules", "DateTimeFrom");
            DropColumn("dbo.Schedules", "DateTimeUntil");
            DropColumn("dbo.Schedules", "Event_Id");
            DropColumn("dbo.Schedules", "Person_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schedules", "Person_Id", c => c.Int());
            AddColumn("dbo.Schedules", "Event_Id", c => c.Int());
            AddColumn("dbo.Schedules", "DateTimeUntil", c => c.DateTime(nullable: false));
            AddColumn("dbo.Schedules", "DateTimeFrom", c => c.DateTime(nullable: false));
            AddColumn("dbo.Schedules", "Role", c => c.Int(nullable: false));
            DropForeignKey("dbo.Appointments", "Schedule_Id", "dbo.Schedules");
            DropForeignKey("dbo.Appointments", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Appointments", "Event_Id", "dbo.Events");
            DropIndex("dbo.Appointments", new[] { "Schedule_Id" });
            DropIndex("dbo.Appointments", new[] { "Person_Id" });
            DropIndex("dbo.Appointments", new[] { "Event_Id" });
            DropTable("dbo.Appointments");
            CreateIndex("dbo.Schedules", "Person_Id");
            CreateIndex("dbo.Schedules", "Event_Id");
            AddForeignKey("dbo.Schedules", "Person_Id", "dbo.People", "Id");
            AddForeignKey("dbo.Schedules", "Event_Id", "dbo.Events", "Id");
        }
    }
}
