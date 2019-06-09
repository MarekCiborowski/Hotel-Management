namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Amenity",
                c => new
                    {
                        AmenityId = c.Int(nullable: false, identity: true),
                        AmenityName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AmenityId);
            
            CreateTable(
                "dbo.RoomAmenity",
                c => new
                    {
                        RoomAmenityId = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        AmenityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomAmenityId)
                .ForeignKey("dbo.Amenity", t => t.AmenityId, cascadeDelete: true)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.AmenityId);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReservationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId)
                .ForeignKey("dbo.Reservation", t => t.ReservationId, cascadeDelete: true)
                .Index(t => t.ReservationId);
            
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Identity = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Zipcode = c.String(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        Role = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Identity)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.Login, unique: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        NIP = c.String(nullable: false),
                        KRS = c.String(nullable: false),
                        REGON = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Zipcode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        NewsContent = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NewsId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false),
                        Score = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        OfferId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Offer", t => t.OfferId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.OfferId);
            
            CreateTable(
                "dbo.Offer",
                c => new
                    {
                        OfferId = c.Int(nullable: false, identity: true),
                        NumberOfGuests = c.Int(nullable: false),
                        TotalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OfferId);
            
            CreateTable(
                "dbo.RoomOffer",
                c => new
                    {
                        RoomOfferId = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        OfferId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomOfferId)
                .ForeignKey("dbo.Offer", t => t.OfferId, cascadeDelete: true)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.OfferId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomAmenity", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Room", "ReservationId", "dbo.Reservation");
            DropForeignKey("dbo.Reservation", "UserId", "dbo.User");
            DropForeignKey("dbo.Review", "UserId", "dbo.User");
            DropForeignKey("dbo.Review", "OfferId", "dbo.Offer");
            DropForeignKey("dbo.RoomOffer", "RoomId", "dbo.Room");
            DropForeignKey("dbo.RoomOffer", "OfferId", "dbo.Offer");
            DropForeignKey("dbo.News", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.RoomAmenity", "AmenityId", "dbo.Amenity");
            DropIndex("dbo.RoomOffer", new[] { "OfferId" });
            DropIndex("dbo.RoomOffer", new[] { "RoomId" });
            DropIndex("dbo.Review", new[] { "OfferId" });
            DropIndex("dbo.Review", new[] { "UserId" });
            DropIndex("dbo.News", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "CompanyId" });
            DropIndex("dbo.User", new[] { "Login" });
            DropIndex("dbo.Reservation", new[] { "UserId" });
            DropIndex("dbo.Room", new[] { "ReservationId" });
            DropIndex("dbo.RoomAmenity", new[] { "AmenityId" });
            DropIndex("dbo.RoomAmenity", new[] { "RoomId" });
            DropTable("dbo.RoomOffer");
            DropTable("dbo.Offer");
            DropTable("dbo.Review");
            DropTable("dbo.News");
            DropTable("dbo.Company");
            DropTable("dbo.User");
            DropTable("dbo.Reservation");
            DropTable("dbo.Room");
            DropTable("dbo.RoomAmenity");
            DropTable("dbo.Amenity");
        }
    }
}
