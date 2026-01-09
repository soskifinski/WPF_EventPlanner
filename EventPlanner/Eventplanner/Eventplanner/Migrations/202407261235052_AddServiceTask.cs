namespace Eventplanner.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddServiceTask : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Appointments", newName: "ServiceTasks");
            AddColumn("dbo.ServiceTasks", "ServiceRole", c => c.Int(nullable: false));
            AddColumn("dbo.ServiceTasks", "TaskStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.ServiceTasks", "TaskEnd", c => c.DateTime(nullable: false));
            DropColumn("dbo.ServiceTasks", "Service");
            DropColumn("dbo.ServiceTasks", "AppointmentStart");
            DropColumn("dbo.ServiceTasks", "AppointmentEnd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceTasks", "AppointmentEnd", c => c.DateTime(nullable: false));
            AddColumn("dbo.ServiceTasks", "AppointmentStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.ServiceTasks", "Service", c => c.Int(nullable: false));
            DropColumn("dbo.ServiceTasks", "TaskEnd");
            DropColumn("dbo.ServiceTasks", "TaskStart");
            DropColumn("dbo.ServiceTasks", "ServiceRole");
            RenameTable(name: "dbo.ServiceTasks", newName: "Appointments");
        }
    }
}
