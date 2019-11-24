namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomSizeHotelBookingSite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelBookingSites",
                c => new
                    {
                        HotelBookingSiteId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.HotelBookingSiteId);
            
            AddColumn("dbo.Room", "RoomSize", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Reservation", "HotelBookingSiteId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservation", "HotelBookingSiteId");
            AddForeignKey("dbo.Reservation", "HotelBookingSiteId", "dbo.HotelBookingSites", "HotelBookingSiteId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation", "HotelBookingSiteId", "dbo.HotelBookingSites");
            DropIndex("dbo.Reservation", new[] { "HotelBookingSiteId" });
            DropColumn("dbo.Reservation", "HotelBookingSiteId");
            DropColumn("dbo.Room", "RoomSize");
            DropTable("dbo.HotelBookingSites");
        }
    }
}
