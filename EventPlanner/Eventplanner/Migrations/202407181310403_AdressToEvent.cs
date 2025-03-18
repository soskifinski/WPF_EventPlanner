namespace Eventplanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdressToEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Address_Id", c => c.Int());
            CreateIndex("dbo.Events", "Address_Id");
            AddForeignKey("dbo.Events", "Address_Id", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Events", new[] { "Address_Id" });
            DropColumn("dbo.Events", "Address_Id");
        }
    }
}
