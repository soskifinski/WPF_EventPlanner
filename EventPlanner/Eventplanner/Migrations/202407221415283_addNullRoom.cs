namespace Eventplanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNullRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "Discriminator");
        }
    }
}
