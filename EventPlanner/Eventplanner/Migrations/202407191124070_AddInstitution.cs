namespace Eventplanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInstitution : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
            CreateTable(
                "dbo.TicketPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ticket_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tickets", t => t.Ticket_Id)
                .Index(t => t.Ticket_Id);
            
            AddColumn("dbo.People", "Email", c => c.String());
            AddColumn("dbo.Rooms", "Institution_Id", c => c.Int());
            CreateIndex("dbo.Rooms", "Institution_Id");
            AddForeignKey("dbo.Rooms", "Institution_Id", "dbo.Institutions", "Id");
            DropColumn("dbo.Rooms", "AdressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "AdressId", c => c.Int(nullable: false));
            DropForeignKey("dbo.TicketPrices", "Ticket_Id", "dbo.Tickets");
            DropForeignKey("dbo.Rooms", "Institution_Id", "dbo.Institutions");
            DropForeignKey("dbo.Institutions", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.TicketPrices", new[] { "Ticket_Id" });
            DropIndex("dbo.Rooms", new[] { "Institution_Id" });
            DropIndex("dbo.Institutions", new[] { "Address_Id" });
            DropColumn("dbo.Rooms", "Institution_Id");
            DropColumn("dbo.People", "Email");
            DropTable("dbo.TicketPrices");
            DropTable("dbo.Institutions");
        }
    }
}
