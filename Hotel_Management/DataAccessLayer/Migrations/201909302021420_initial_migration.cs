namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Amenity",
                c => new
                    {
                        AmenityId = c.Int(nullable: false, identity: true),
                        AmenityName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.AmenityId);
            
            CreateTable(
                "dbo.RoomAmenity",
                c => new
                    {
                        RoomAmenityId = c.Int(nullable: false, identity: true),
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
                        MaxNumberOfGuests = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId);
            
            CreateTable(
                "dbo.RoomReservation",
                c => new
                    {
                        RoomReservationId = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        ReservationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomReservationId)
                .ForeignKey("dbo.Reservation", t => t.ReservationId, cascadeDelete: true)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.ReservationId);
            
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        ReservationStatusId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.ReservationStatus", t => t.ReservationStatusId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ReservationStatusId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ReservationStatus",
                c => new
                    {
                        ReservationStatusId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ReservationStatusId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Identity = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 100),
                        Zipcode = c.String(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Identity)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.Login, unique: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Conversation",
                c => new
                    {
                        ConversationID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ConversationID);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        MessageContent = c.String(nullable: false, maxLength: 500),
                        ConversationID = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.Conversation", t => t.ConversationID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ConversationID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserConversation",
                c => new
                    {
                        UserConversationID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ConversationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserConversationID)
                .ForeignKey("dbo.Conversation", t => t.ConversationID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ConversationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserConversation", "UserID", "dbo.User");
            DropForeignKey("dbo.UserConversation", "ConversationID", "dbo.Conversation");
            DropForeignKey("dbo.Message", "UserId", "dbo.User");
            DropForeignKey("dbo.Message", "ConversationID", "dbo.Conversation");
            DropForeignKey("dbo.RoomAmenity", "RoomId", "dbo.Room");
            DropForeignKey("dbo.RoomReservation", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Reservation", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.RoomReservation", "ReservationId", "dbo.Reservation");
            DropForeignKey("dbo.Reservation", "ReservationStatusId", "dbo.ReservationStatus");
            DropForeignKey("dbo.RoomAmenity", "AmenityId", "dbo.Amenity");
            DropIndex("dbo.UserConversation", new[] { "ConversationID" });
            DropIndex("dbo.UserConversation", new[] { "UserID" });
            DropIndex("dbo.Message", new[] { "UserId" });
            DropIndex("dbo.Message", new[] { "ConversationID" });
            DropIndex("dbo.User", new[] { "RoleId" });
            DropIndex("dbo.User", new[] { "Login" });
            DropIndex("dbo.Reservation", new[] { "UserId" });
            DropIndex("dbo.Reservation", new[] { "ReservationStatusId" });
            DropIndex("dbo.RoomReservation", new[] { "ReservationId" });
            DropIndex("dbo.RoomReservation", new[] { "RoomId" });
            DropIndex("dbo.RoomAmenity", new[] { "AmenityId" });
            DropIndex("dbo.RoomAmenity", new[] { "RoomId" });
            DropTable("dbo.UserConversation");
            DropTable("dbo.Message");
            DropTable("dbo.Conversation");
            DropTable("dbo.Roles");
            DropTable("dbo.User");
            DropTable("dbo.ReservationStatus");
            DropTable("dbo.Reservation");
            DropTable("dbo.RoomReservation");
            DropTable("dbo.Room");
            DropTable("dbo.RoomAmenity");
            DropTable("dbo.Amenity");
        }
    }
}
