namespace Eventplanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenderToPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Gender");
        }
    }
}
