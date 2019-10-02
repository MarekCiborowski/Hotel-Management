namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservation_date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservation", "AccomodationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reservation", "CheckOutDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservation", "CheckOutDate");
            DropColumn("dbo.Reservation", "AccomodationDate");
        }
    }
}
