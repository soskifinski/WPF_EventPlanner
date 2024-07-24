namespace Eventplanner.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeePerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "IsEmployee", c => c.Boolean(nullable: false));
            DropColumn("dbo.People", "IsEmploee");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "IsEmploee", c => c.Boolean(nullable: false));
            DropColumn("dbo.People", "IsEmployee");
        }
    }
}
