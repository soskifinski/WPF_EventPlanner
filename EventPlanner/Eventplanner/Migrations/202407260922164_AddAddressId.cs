namespace Eventplanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.People", new[] { "Address_Id" });
            RenameColumn(table: "dbo.People", name: "Address_Id", newName: "AddressId");
            AlterColumn("dbo.People", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.People", "AddressId");
            AddForeignKey("dbo.People", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "AddressId", "dbo.Addresses");
            DropIndex("dbo.People", new[] { "AddressId" });
            AlterColumn("dbo.People", "AddressId", c => c.Int());
            RenameColumn(table: "dbo.People", name: "AddressId", newName: "Address_Id");
            CreateIndex("dbo.People", "Address_Id");
            AddForeignKey("dbo.People", "Address_Id", "dbo.Addresses", "Id");
        }
    }
}
